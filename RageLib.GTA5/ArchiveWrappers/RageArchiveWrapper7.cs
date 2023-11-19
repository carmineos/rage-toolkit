// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;
using RageLib.Compression;
using RageLib.Cryptography;
using RageLib.Data;
using RageLib.GTA5.Archives;
using RageLib.GTA5.Cryptography;
using RageLib.Resources;
using RageLib.Resources.GTA5;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RageLib.GTA5.ArchiveWrappers
{
    // TODO: Check input stream based on mode
    // Use leaveOpen param to determine if the stream owner is the archive (in that case it should take care of disposing it, otherwise shouldn't)
    public enum RageArchiveMode
    {
        Read = 0,
        Create = 1,
        Update = 2,
    }

    /// <summary>
    /// Represents a wrapper for an RPFv7 archive.
    /// </summary>
    public class RageArchiveWrapper7 : IArchive
    {
        public const int BLOCK_SIZE = 0x200;

        /// <summary>
        /// The RPF7 archive
        /// </summary>
        private RageArchive7 archive;

        /// <summary>
        /// The encryption of the archive
        /// </summary>
        public RageArchiveEncryption7 Encryption
        {
            get => archive.Encryption;
            set => archive.Encryption = value;
        }

        /// <summary>
        /// The filename of the archive
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the length of the archive
        /// </summary>
        public long Size => archive.BaseStream.Length;  // TODO: Is this true for new/edited archives?

        /// <summary>
        /// Gets the root directory of the archive.
        /// </summary>
        public IArchiveDirectory Root => new RageArchiveDirectoryWrapper7(this, archive.Root);

        /// <summary>
        /// A flag to indicate if the archive should be left open 
        /// </summary>
        public bool LeaveOpen { get; set; }

        private RageArchiveWrapper7(Stream stream, string fileName, bool leaveOpen = false)
        {
            archive = new RageArchive7(stream);
            this.Name = fileName;
            this.LeaveOpen = leaveOpen;
            //  archive_.ReadHeader();
        }

        /// <summary>
        /// Clears all buffers for this archive and causes any buffered data to be 
        /// written to the underlying device.
        /// </summary>
        public void Flush()
        {



            long headerSize = GetHeaderSize();
            do
            {
                var blocks = GetBlocks();
                long maxheaderlength = ArchiveHelpers.FindSpace(blocks, blocks[0]);
                if (maxheaderlength < headerSize)
                {
                    long newpos = ArchiveHelpers.FindOffset(blocks, blocks[1].Length, BLOCK_SIZE);
                    ArchiveHelpers.MoveBytes(archive.BaseStream, blocks[1].Offset, newpos, blocks[1].Length);
                    ((IRageArchiveFileEntry7)blocks[1].Tag).FileOffset = (uint)(newpos / BLOCK_SIZE);

                    blocks = GetBlocks();
                    maxheaderlength = ArchiveHelpers.FindSpace(blocks, blocks[0]);
                }
                else
                    break;

            } while (true);




            // calculate key...
            var indexKey = GTA5Crypto.GetKeyIndex(Name, (uint)archive.BaseStream.Length);

            //  archive_.key_ = GTA5Crypto.key_gta5;
            archive.WriteHeader(GTA5Constants.PC_AES_KEY, GTA5Constants.PC_NG_KEYS[indexKey]);
            archive.BaseStream.Flush();

        }

        /// <summary>
        /// Releases all resources used by the archive.
        /// </summary>
        public void Dispose()
        {
            if (archive != null)
                archive.Dispose();

            archive = null;
        }

        /////////////////////////////////////////////////////////////////////////////
        // helper functions
        /////////////////////////////////////////////////////////////////////////////

        internal Stream GetStream(RageArchiveBinaryFile7 file)
        {

            return new PartialStream(
                    archive.BaseStream,
                    delegate () // offset
                    {
                        return BLOCK_SIZE * file.FileOffset;
                    },
                    delegate () // size
                    {
                        if (file.FileSize != 0)
                            return file.FileSize; // compressed
                        else
                            return file.FileUncompressedSize; // uncompressed
                    },
                    delegate (long newLength)
                    {
                        RequestBytes(file, newLength);
                    }
                );

        }

        /// <summary>
        /// Gets the stream to a resource file
        /// </summary>
        /// <param name="file">The wrapper to the resource file</param>
        /// <returns>The stream</returns>
        internal Stream GetStream(RageArchiveResourceFile7 file)
        {

            return new PartialStream(
                    archive.BaseStream,
                   delegate () // offset
                   {
                       return file.FileOffset * BLOCK_SIZE;
                   },
                   delegate () // size
                   {
                       return file.FileSize;
                   },
                   delegate (long newLength)
                   {
                       RequestBytesRES(file, newLength);
                   }
               );
        }








        private long GetHeaderSize()
        {
            long len = 16;

            var st = new Stack<RageArchiveDirectory7>();
            st.Push(archive.Root);
            while (st.Count > 0)
            {
                var x = st.Pop();
                len += 16; // entry
                len += x.Name.Length + 1; // name

                foreach (var q in x.Directories)
                    st.Push(q);
                foreach (var q in x.Files)
                    len += 16 + q.Name.Length + 1;
            }


            return len;
        }


        internal long FindSpace(long length)
        {
            // determine header size...
            long x = GetHeaderSize();

            List<DataBlock> blocks = GetBlocks();

            long offset = ArchiveHelpers.FindOffset(blocks, length, BLOCK_SIZE);
            return offset;
        }

        private List<DataBlock> GetBlocks()
        {
            var blocks = new List<DataBlock>();

            // header
            blocks.Add(
                new DataBlock(0, GetHeaderSize())
            );

            var st = new Stack<RageArchiveDirectory7>();
            st.Push(archive.Root);
            while (st.Count > 0)
            {
                var x = st.Pop();

                foreach (var q in x.Directories)
                    st.Push(q);
                foreach (IRageArchiveFileEntry7 q in x.Files)
                {
                    if (q is RageArchiveBinaryFile7)
                    {
                        // if(q.FileSize != 0)
                        RageArchiveBinaryFile7 fff = (RageArchiveBinaryFile7)q;

                        long l = 0;
                        if (q.FileSize == 0)
                            l = fff.FileUncompressedSize;
                        else
                            l = fff.FileSize;

                        blocks.Add(new DataBlock(q.FileOffset * BLOCK_SIZE, l, q));
                    }
                    else
                    {
                        // if(q.FileSize != 0)
                        RageArchiveResourceFile7 fff = (RageArchiveResourceFile7)q;

                        long l = fff.FileSize;

                        blocks.Add(new DataBlock(q.FileOffset * BLOCK_SIZE, l, q));
                    }
                }
            }

            blocks.Sort(
                        delegate (DataBlock a, DataBlock b)
                        {
                            if (a.Offset != b.Offset)
                                return a.Offset.CompareTo(b.Offset);
                            else
                                return a.Offset.CompareTo(b.Offset);
                        }
                    );

            return blocks;
        }



        public void RequestBytes(RageArchiveBinaryFile7 file_, long newLength)
        {
            // determine header size...
            long x = GetHeaderSize();

            DataBlock thisBlock = null;
            var blocks = GetBlocks();
            foreach (var q in blocks)
                if (q.Tag == file_)
                    thisBlock = q;

            long maxlength = ArchiveHelpers.FindSpace(blocks, thisBlock);
            if (maxlength < newLength)
            {
                // move...

                long offset = ArchiveHelpers.FindOffset(blocks, newLength, BLOCK_SIZE);
                ArchiveHelpers.MoveBytes(archive.BaseStream, thisBlock.Offset, offset, thisBlock.Length);
                ((IRageArchiveFileEntry7)thisBlock.Tag).FileOffset = (uint)offset / BLOCK_SIZE;

            }

            if (file_.FileSize != 0)
                file_.FileSize = (uint)newLength;
            else
                file_.FileUncompressedSize = (uint)newLength;
        }

        public void RequestBytesRES(RageArchiveResourceFile7 file_, long newLength)
        {
            // determine header size...
            long x = GetHeaderSize();

            DataBlock thisBlock = null;
            List<DataBlock> blocks = GetBlocks();
            foreach (var q in blocks)
                if (q.Tag == file_)
                    thisBlock = q;

            long maxlength = ArchiveHelpers.FindSpace(blocks, thisBlock);
            if (maxlength < newLength)
            {
                // move...

                long offset = ArchiveHelpers.FindOffset(blocks, newLength, BLOCK_SIZE);
                ArchiveHelpers.MoveBytes(archive.BaseStream, thisBlock.Offset, offset, thisBlock.Length);
                ((IRageArchiveFileEntry7)thisBlock.Tag).FileOffset = (uint)offset / BLOCK_SIZE;

            }

            file_.FileSize = (uint)newLength;
        }




        /////////////////////////////////////////////////////////////////////////////
        // static functions
        /////////////////////////////////////////////////////////////////////////////

        public static RageArchiveWrapper7 Create(string fileName)
        {
            var finfo = new FileInfo(fileName);
            var fs = new FileStream(fileName, FileMode.Create);
            return Create(fs, finfo.Name, false);
        }

        public static RageArchiveWrapper7 Create(Stream stream, string fileName, bool leaveOpen = false)
        {
            var arch = new RageArchiveWrapper7(stream, fileName, leaveOpen);
            
            var rootD = new RageArchiveDirectory7();
            rootD.Name = "";
            arch.archive.Root = rootD;
            // arch.archive_.WriteHeader(); // write...
            return arch;
        }

        public static RageArchiveWrapper7 Open(string fileName)
        {
            var finfo = new FileInfo(fileName);
            var fs = new FileStream(fileName, FileMode.Open);
            var arch = new RageArchiveWrapper7(fs, finfo.Name, false);
            try
            {

                if (GTA5Constants.PC_LUT != null && GTA5Constants.PC_NG_KEYS != null)
                {
                    // calculate key...
                    var indexKey = GTA5Crypto.GetKeyIndex(arch.Name, (uint)finfo.Length);

                    arch.archive.ReadHeader(GTA5Constants.PC_AES_KEY, GTA5Constants.PC_NG_KEYS[indexKey]); // read...
                }
                else
                {
                    arch.archive.ReadHeader(GTA5Constants.PC_AES_KEY, null); // read...
                }

                
                return arch;
            }
            catch
            {
                fs.Dispose();
                arch.Dispose();
                return InternalOpenWithUnknownNGKey(fileName);
                throw;
            }
        }

        public static RageArchiveWrapper7 Open(Stream stream, string fileName, bool leaveOpen = false)
        {
            var arch = new RageArchiveWrapper7(stream, fileName, leaveOpen);
            try
            {
                if (GTA5Constants.PC_LUT != null && GTA5Constants.PC_NG_KEYS != null)
                {
                    // calculate key...
                    var indexKey = GTA5Crypto.GetKeyIndex(arch.Name, (uint)stream.Length);

                    arch.archive.ReadHeader(GTA5Constants.PC_AES_KEY, GTA5Constants.PC_NG_KEYS[indexKey]); // read...
                }
                else
                {
                    arch.archive.ReadHeader(GTA5Constants.PC_AES_KEY, null); // read...
                }

                return arch;
            }
            catch
            {
                arch.Dispose();
                return InternalOpenWithUnknownNGKey(stream, fileName, leaveOpen);
                throw;
            }
        }

        // If we haven't the required key with just try with any of them
        // We could make it faster to detect a wrong key by just checking if the root entry of a rpf is a folder
        private static RageArchiveWrapper7 InternalOpenWithUnknownNGKey(string fileName)
        {
            for (int i = 0; i < GTA5Constants.PC_NG_KEYS.Length; i++)
            {
                var finfo = new FileInfo(fileName);
                var fs = new FileStream(fileName, FileMode.Open);
                var arch = new RageArchiveWrapper7(fs, finfo.Name, false);
                try
                {
                    arch.archive.ReadHeader(GTA5Constants.PC_AES_KEY, GTA5Constants.PC_NG_KEYS[i]);
                    return arch;
                }
                catch
                {
                    fs.Dispose();
                    arch.Dispose();
                }
            }

            throw new Exception();
        }

        // If we haven't the required key with just try with any of them
        // We could make it faster to detect a wrong key by just checking if the root entry of a rpf is a folder
        private static RageArchiveWrapper7 InternalOpenWithUnknownNGKey(Stream stream, string fileName, bool leaveOpen = false)
        {
            for (int i = 0; i < GTA5Constants.PC_NG_KEYS.Length; i++)
            {
                var arch = new RageArchiveWrapper7(stream, fileName, leaveOpen);
                try
                {
                    arch.archive.ReadHeader(GTA5Constants.PC_AES_KEY, GTA5Constants.PC_NG_KEYS[i]);
                    return arch;
                }
                catch
                {
                    arch.Dispose();
                }
            }

            throw new Exception();
        }

        public static bool IsArchive(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return IsArchive(fileStream);
        }

        public static bool IsArchive(Stream stream)
        {
            return RageArchive7.IsRPF7(stream);
        }
    }

    /// <summary>
    /// Represents a wrapper for a directory in an RPFv7 archive.
    /// </summary>
    public class RageArchiveDirectoryWrapper7 : IArchiveDirectory
    {
        private readonly RageArchiveWrapper7 _archiveWrapper;
        private readonly RageArchiveDirectory7 _directory;

        /// <summary>
        /// Gets or sets the name of the directory.
        /// </summary>
        public string Name
        {
            get => _directory.Name;
            set => _directory.Name = value;
        }

        internal RageArchiveDirectoryWrapper7(RageArchiveWrapper7 archiveWrapper, RageArchiveDirectory7 directory)
        {
            _archiveWrapper = archiveWrapper;
            _directory = directory;
        }

        /// <summary>
        /// Returns a directory list from the current directory. 
        /// </summary>
        public IEnumerable<IArchiveDirectory> GetDirectories()
        {
            foreach (var directory in _directory.Directories)
                yield return new RageArchiveDirectoryWrapper7(_archiveWrapper, directory);
        }

        /// <summary>
        /// Returns a directory from the current directory. 
        /// </summary>
        public IArchiveDirectory? GetDirectory(string name)
        {
            foreach (var directory in _directory.Directories)
            {
                if (directory.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return new RageArchiveDirectoryWrapper7(_archiveWrapper, directory);
            }
            return null;
        }

        /// <summary>
        /// Creates a new directory inside this directory.
        /// </summary>
        public IArchiveDirectory CreateDirectory()
        {
            var newDirectory = new RageArchiveDirectory7();
            var newDirectoryWrapper = new RageArchiveDirectoryWrapper7(_archiveWrapper, newDirectory);

            this._directory.Directories.Add(newDirectory);

            return newDirectoryWrapper;
        }

        /// <summary>
        /// Deletes an existing directory inside this directory.
        /// </summary>
        public void DeleteDirectory(IArchiveDirectory directory)
        {
            this._directory.Directories.Remove(((RageArchiveDirectoryWrapper7)directory)._directory);
        }

        /// <summary>
        /// Returns a file list from the current directory. 
        /// </summary>
        public IEnumerable<IArchiveFile> GetFiles()
        {
            foreach (var file in _directory.Files)
            {
                if (file is RageArchiveResourceFile7 rsc7)
                    yield return new RageArchiveResourceFileWrapper7(_archiveWrapper, rsc7);
                else
                    yield return new RageArchiveBinaryFileWrapper7(_archiveWrapper, (RageArchiveBinaryFile7)file);
            }
        }

        /// <summary>
        /// Returns a file from the current directory. 
        /// </summary>
        public IArchiveFile? GetFile(string name)
        {
            foreach (var f in _directory.Files)
            {
                if (f.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    if (f is RageArchiveResourceFile7 rsc7)
                        return new RageArchiveResourceFileWrapper7(_archiveWrapper, rsc7);
                    else
                        return new RageArchiveBinaryFileWrapper7(_archiveWrapper, (RageArchiveBinaryFile7)f);
            }
            return null;
        }

        /// <summary>
        /// Creates a new binary file inside this directory.
        /// </summary>
        public IArchiveBinaryFile CreateBinaryFile()
        {
            RageArchiveBinaryFile7 realF = new RageArchiveBinaryFile7();
            RageArchiveBinaryFileWrapper7 wrD = new RageArchiveBinaryFileWrapper7(_archiveWrapper, realF);


            realF.Name = "";
            var offset = _archiveWrapper.FindSpace(64);
            realF.FileOffset = (uint)(offset / RageArchiveWrapper7.BLOCK_SIZE);

            _directory.Files.Add(realF);

            return wrD;
        }

        /// <summary>
        /// Creates a new resource file inside this directory.
        /// </summary>
        public IArchiveResourceFile CreateResourceFile()
        {
            RageArchiveResourceFile7 realF = new RageArchiveResourceFile7();
            RageArchiveResourceFileWrapper7 wrD = new RageArchiveResourceFileWrapper7(_archiveWrapper, realF);


            realF.Name = "";
            var offset = _archiveWrapper.FindSpace(64);
            realF.FileOffset = (uint)(offset / RageArchiveWrapper7.BLOCK_SIZE);

            _directory.Files.Add(realF);

            return wrD;
        }

        /// <summary>
        /// Deletes an existing file inside this directory.
        /// </summary>
        public void DeleteFile(IArchiveFile file)
        {
            if (file is RageArchiveResourceFileWrapper7 rsc7)
                _directory.Files.Remove(rsc7.GetFile());
            else if (file is RageArchiveBinaryFileWrapper7 binaryFile)
                _directory.Files.Remove(binaryFile.GetFile());
            else
                throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Represents a wrapper for a binary file in an RPFv7 archive.
    /// </summary>
    public class RageArchiveBinaryFileWrapper7 : IArchiveBinaryFile
    {
        private readonly RageArchiveWrapper7 _archiveWrapper;
        private readonly RageArchiveBinaryFile7 _binaryFile;

        internal RageArchiveBinaryFile7 GetFile() => _binaryFile;

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Name
        {
            get => _binaryFile.Name;
            set => _binaryFile.Name = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the file is encrypted.
        /// </summary>
        public bool IsEncrypted
        {
            get => _binaryFile.IsEncrypted;
            set => _binaryFile.IsEncrypted = value;
        }

        /// <summary>
        /// Gets the encryption type.
        /// </summary>
        public RageArchiveEncryption7 Encryption => _archiveWrapper.Encryption;

        /// <summary>
        /// Gets or sets a value indicating whether the file is compressed.
        /// </summary>
        public bool IsCompressed
        {
            get => _binaryFile.FileSize != 0;
            set
            {
                if (value)
                {
                    _binaryFile.FileSize = _binaryFile.FileUncompressedSize;
                }
                else
                {
                    _binaryFile.FileUncompressedSize = _binaryFile.FileSize;
                    _binaryFile.FileSize = 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the uncompressed size of the file. This 
        /// property can only be set if the file is compressed.
        /// </summary>
        public long UncompressedSize
        {
            get => _binaryFile.FileUncompressedSize;
            set => _binaryFile.FileUncompressedSize = (uint)value;
        }

        /// <summary>
        /// Gets the compressed size of the file or 0 if not compressed.
        /// </summary>
        public long CompressedSize => _binaryFile.FileSize;

        public long Size => _binaryFile.FileSize != 0 ? _binaryFile.FileSize : _binaryFile.FileUncompressedSize;

        internal RageArchiveBinaryFileWrapper7(RageArchiveWrapper7 archiveWrapper, RageArchiveBinaryFile7 file)
        {
            _archiveWrapper = archiveWrapper;
            _binaryFile = file;
        }

        /// <summary>
        /// Gets the stream that respresents the possibly compressed content of the file.
        /// </summary>
        public Stream GetStream()
        {
            return _archiveWrapper.GetStream(_binaryFile);
        }





        /// <summary>
        /// Imports a binary file.
        /// </summary>
        public void Import(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                Import(stream);
        }

        /// <summary>
        /// Imports a binary file.
        /// </summary>
        public void Import(Stream stream)
        {
            var binaryStream = GetStream();
            binaryStream.SetLength(stream.Length);
            stream.CopyTo(binaryStream, (int)stream.Length);
        }

        /// <summary>
        /// Exports a binary file.
        /// </summary>
        public void Export(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                Export(stream);
        }

        /// <summary>
        /// Exports a binary file.
        /// </summary>
        public void Export(Stream stream)
        {
            var binaryStream = GetStream();
            binaryStream.CopyTo(stream);
        }

        public void ExportUncompressed(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                ExportUncompressed(stream);
        }

        public void ExportUncompressed(Stream stream)
        {
            // export
            var length = (int)Size;
            using var ms = new MemoryStream(length);
            Export(ms);
            ms.Position = 0;

            var buf = ms.GetBuffer();
            var span = buf.AsSpan(0, length);

            //var buf = ArrayPool<byte>.Shared.Rent(length);
            //var span = buf.AsSpan(0, length);
            //ms.Position = 0;
            //ms.Read(span);

            var uncompressedSize = (uint)UncompressedSize;

            // decrypt...
            if (IsEncrypted)
            {
                if (Encryption == RageArchiveEncryption7.AES)
                {
                    AesEncryption.DecryptData(buf, GTA5Constants.PC_AES_KEY);
                }
                else if (Encryption == RageArchiveEncryption7.NG)
                {
                    var indexKey = GTA5Crypto.GetKeyIndex(_binaryFile.Name, uncompressedSize);
                    GTA5Crypto.DecryptData(span, GTA5Constants.PC_NG_KEYS[indexKey]);
                }
            }

            byte[] bufnew = null;

            // decompress...
            if (IsCompressed)
            {
                var def = new DeflateStream(new MemoryStream(buf, 0, length), CompressionMode.Decompress);
                bufnew = ArrayPool<byte>.Shared.Rent((int)uncompressedSize);
                def.ReadExactly(bufnew, 0, (int)uncompressedSize);
                buf = bufnew;
            }

            stream.Write(buf.AsSpan(0, (int)uncompressedSize));

            if (bufnew != null)
                ArrayPool<byte>.Shared.Return(bufnew);
        }

        public void ImportCompressed(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                ImportCompressed(stream);
        }

        public void ImportCompressed(Stream stream)
        {
            using MemoryStream compressedStream = new MemoryStream();

            // Compress the data to memory
            using var compressor = new DeflateStream(compressedStream, CompressionMode.Compress);
            compressedStream.Position = 0;
            stream.Position = 0;
            stream.CopyTo(compressor);
            compressor.Flush();

            // Set the binary file as compressed
            UncompressedSize = stream.Length;
            IsCompressed = true;

            // Get the buffer for the compressed file and copy the content
            compressedStream.Position = 0;
            
            // Warning: Encryption whole binary file is overkill
            //          Specially with many nested RPF
            //          Don't use this unless you know what you are doing
            //var buf = compressedStream.GetBuffer();
            //var span = buf.AsSpan(0, (int)compressedStream.Length);
            //if (Encryption == RageArchiveEncryption7.AES)
            //{
            //    IsEncrypted = true;
            //    AesEncryption.EncryptData(buf, GTA5Constants.PC_AES_KEY);
            //}
            //else if (Encryption == RageArchiveEncryption7.NG)
            //{
            //    IsEncrypted = true;
            //    var indexKey = GTA5Crypto.GetKeyIndex(file.Name, (uint)UncompressedSize);
            //    GTA5Crypto.EncryptData(span, GTA5Constants.PC_NG_KEYS[indexKey]);
            //}

            Import(compressedStream);
        }
    }

    /// <summary>
    /// Represents a wrapper for a resource file in an RPFv7 archive.
    /// </summary>
    public class RageArchiveResourceFileWrapper7 : IArchiveResourceFile
    {
        private readonly RageArchiveWrapper7 _archiveWrapper;
        private readonly RageArchiveResourceFile7 _resourceFile;

        internal RageArchiveResourceFile7 GetFile() => _resourceFile;

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Name
        {
            get => _resourceFile.Name;
            set => _resourceFile.Name = value;
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        public long Size => _resourceFile.FileSize;

        internal RageArchiveResourceFileWrapper7(RageArchiveWrapper7 archiveWrapper, RageArchiveResourceFile7 file)
        {
            _archiveWrapper = archiveWrapper;
            _resourceFile = file;
        }

        /// <summary>
        /// Imports a resource file.
        /// </summary>
        public void Import(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                Import(stream);
        }

        /// <summary>
        /// Imports a resource file.
        /// </summary>
        public void Import(Stream stream)
        {
            var resourceStream = _archiveWrapper.GetStream(_resourceFile);
            resourceStream.SetLength(stream.Length);

            // read resource
            var reader = new DataReader(stream);
            reader.Position = 0;
            var ident = reader.ReadUInt32();
            var version = reader.ReadUInt32();
            var systemFlags = reader.ReadUInt32();
            var graphicsFlags = reader.ReadUInt32();

            reader.Position = 0;
            _resourceFile.ResourceInfo = new DatResourceInfo(systemFlags, graphicsFlags);

            stream.CopyTo(resourceStream);
        }

        /// <summary>
        /// Exports a resource file.
        /// </summary>
        public void Export(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                Export(stream);
        }

        /// <summary>
        /// Exports a resource file.
        /// </summary>
        public void Export(Stream stream)
        {
            var writer = new DataWriter(stream);
            writer.Write((uint)0x37435352);
            writer.Write((uint)_resourceFile.ResourceInfo.Version);
            writer.Write(_resourceFile.ResourceInfo.VirtualFlags);
            writer.Write(_resourceFile.ResourceInfo.PhysicalFlags);

            var resourceStream = _archiveWrapper.GetStream(_resourceFile);
            resourceStream.Position = 16;

            resourceStream.CopyTo(stream, (int)resourceStream.Length - 16);
        }

        /// <summary>
        /// Gets the stream that respresents the possibly compressed content of the file.
        /// </summary>
        public Stream GetStream()
        {
            return _archiveWrapper.GetStream(_resourceFile);
        }

        public void ExportResourceContent(string directoryPath)
        {
            var ext = Path.GetExtension(Name);
            var name = Name.Replace(ext, ext.Replace('.', '_'));

            var directory = Directory.CreateDirectory(Path.Combine(directoryPath, name));

            // export
            using var ms = new MemoryStream((int)Size);
            Export(ms);
            ms.Position = 0;

            var rsc7 = new Resource7();
            rsc7.Load(ms);

            // TODO: refactor to write the deflated stream directly
            File.WriteAllBytes(Path.Combine(directory.FullName, Name + ".virtual"), rsc7.VirtualData);
            File.WriteAllBytes(Path.Combine(directory.FullName, Name + ".physical"), rsc7.PhysicalData);
        }
    }
}