﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Cryptography;
using RageLib.Data;
using RageLib.GTA5.Cryptography;
using RageLib.Resources;
using System;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Archives
{
    public interface IRageArchiveEntry7
    {
        uint NameOffset { get; set; }
        string Name { get; set; }

        void Read(DataReader reader);
        void Write(DataWriter writer);
    }

    public interface IRageArchiveFileEntry7 : IRageArchiveEntry7
    {
        uint FileOffset { get; set; }
        uint FileSize { get; set; }
    }

    public enum RageArchiveEncryption7 : uint
    {
        None = 0,
        OPEN = 0x4E45504F,
        AES = 0x0FFFFFF9,
        NG = 0x0FEFFFFF,
    }

    /// <summary>
    /// Represents an RPFv7 archive.
    /// </summary>
    public class RageArchive7 : IDisposable
    {
        private const uint IDENT = 0x52504637;

        public RageArchiveEncryption7 Encryption { get; set; }

        public Stream BaseStream { get; private set; }

        public RageArchiveDirectory7 Root { get; set; }

        /// <summary>
        /// Creates an RPFv7 archive.
        /// </summary>
        public RageArchive7(Stream fileStream)
        {
            BaseStream = fileStream;
        }

        public static bool IsRPF7(Stream stream)
        {
            var reader = new DataReader(stream);
            var ident = reader.ReadInt32();
            stream.Position -= 4;
            return ident == IDENT;
        }

        /// <summary>
        /// Reads the archive header.
        /// </summary>
        public void ReadHeader(byte[] aesKey = null, byte[] ngKey = null)
        {
            var reader = new DataReader(BaseStream);
            var posbak = reader.Position;
            reader.Position = 0;

            uint header_identifier = reader.ReadUInt32(); // 0x52504637
            if (header_identifier != IDENT)
                throw new Exception("The identifier " + header_identifier.ToString("X8") + " did not match the RPF7 one");

            uint header_entriesCount = reader.ReadUInt32();
            uint header_namesLength = reader.ReadUInt32();
            var header_encryption = (RageArchiveEncryption7)reader.ReadUInt32();

            byte[] entries_data_dec = reader.ReadBytes(16 * (int)header_entriesCount);
            byte[] names_data_dec = reader.ReadBytes((int)header_namesLength);

            switch (header_encryption)
            {
                case RageArchiveEncryption7.None:
                case RageArchiveEncryption7.OPEN:   // for OpenIV compatibility
                    // no encryption...
                    Encryption = RageArchiveEncryption7.None;
                    break;
                case RageArchiveEncryption7.AES:
                    // AES encryption...                
                    Encryption = RageArchiveEncryption7.AES;
                    AesEncryption.DecryptData(entries_data_dec, aesKey);
                    AesEncryption.DecryptData(names_data_dec, aesKey);
                    break;
                case RageArchiveEncryption7.NG:
                    // NG encryption...
                    Encryption = RageArchiveEncryption7.NG;
                    GTA5Crypto.DecryptData(entries_data_dec, ngKey);
                    GTA5Crypto.DecryptData(names_data_dec, ngKey);
                    break;
                default:
                    throw new Exception($"Unknown RPF7 encryption type: {header_encryption.ToString("X8")}");
            }

            var entries_reader = new DataReader(new MemoryStream(entries_data_dec));
            var names_reader = new DataReader(new MemoryStream(names_data_dec));

            // Ensure root is a directory, if not then probably decryption failed
            uint tmp = entries_reader.ReadUInt32();
            tmp = entries_reader.ReadUInt32();
            if (tmp != 0x7fffff00)
                throw new Exception("Root of RPF7 isn't a directory");
            entries_reader.Position -= 8;

            var entries = new List<IRageArchiveEntry7>();
            for (var index = 0; index < header_entriesCount; index++)
            {
                uint y = entries_reader.ReadUInt32();
                uint x = entries_reader.ReadUInt32();
                entries_reader.Position -= 8;

                if (x == 0x7fffff00)
                {
                    // directory
                    var e = new RageArchiveDirectory7();
                    e.Read(entries_reader);

                    entries.Add(e);
                }
                else
                {
                    if ((x & 0x80000000) == 0)
                    {
                        // binary file
                        var e = new RageArchiveBinaryFile7();
                        e.Read(entries_reader);

                        entries.Add(e);
                    }
                    else
                    {
                        // resource file
                        var e = new RageArchiveResourceFile7();
                        e.Read(entries_reader);
                        PatchLargeResourceFile(e, reader);

                        entries.Add(e);
                    }
                }
            }

            // Read the names of the entries
            foreach(var e in entries)
            {
                names_reader.Position = e.NameOffset;
                e.Name = names_reader.ReadString();
            }

            BuildEntriesTree(entries);

            reader.Position = posbak;
        }

        private void BuildEntriesTree(List<IRageArchiveEntry7> entries)
        {
            var stack = new Stack<RageArchiveDirectory7>();
            stack.Push((RageArchiveDirectory7)entries[0]);
            Root = (RageArchiveDirectory7)entries[0];
            
            while (stack.Count > 0)
            {
                var item = stack.Pop();

                for (int index = (int)item.EntriesIndex; index < (item.EntriesIndex + item.EntriesCount); index++)
                {
                    if (entries[index] is RageArchiveDirectory7 directoryEntry)
                    {
                        item.Directories.Add(directoryEntry);
                        stack.Push(directoryEntry);
                    }
                    else
                    {
                        item.Files.Add(entries[index]);
                    }
                }
            }
        }

        /// <summary>
        /// Writes the archive header.
        /// </summary>
        public void WriteHeader(byte[] aesKey = null, byte[] ngKey = null)
        {
            // backup position
            var positionBackup = BaseStream.Position;

            var writer = new DataWriter(BaseStream);


            var entries = new List<IRageArchiveEntry7>();
            var stack = new Stack<RageArchiveDirectory7>();
            var nameOffset = 1;


            entries.Add(Root);
            stack.Push(Root);

            var nameDict = new Dictionary<string, uint>();
            nameDict.Add("", 0);

            while (stack.Count > 0)
            {
                var directory = stack.Pop();

                directory.EntriesIndex = (uint)entries.Count;
                directory.EntriesCount = (uint)directory.Directories.Count + (uint)directory.Files.Count;

                var theList = new List<IRageArchiveEntry7>((int)directory.EntriesCount);

                theList.AddRange(directory.Directories);
                theList.AddRange(directory.Files);
                theList.Sort(static (a, b) => string.CompareOrdinal(a.Name, b.Name));

                foreach (var xd in theList)
                {
                    if (!nameDict.TryGetValue(xd.Name, out uint value))
                    {
                        value = (uint)nameOffset;
                        nameDict.Add(xd.Name, value);
                        nameOffset += xd.Name.Length + 1;
                    }
                    xd.NameOffset = value;
                }

                entries.AddRange(theList);
                theList.Reverse(); // Required?
                foreach (var entry in theList)
                    if (entry is RageArchiveDirectory7 dir)
                        stack.Push(dir);
            }


            // there are sometimes resources with length>=0xffffff which actually
            // means length=0xffffff
            // -> we therefore just cut the file size
            foreach (var entry in entries)
                if (entry is RageArchiveResourceFile7 rsc7)
                    PatchLargeResourceFile(rsc7, writer);


            // entries...
            var ent_str = new MemoryStream();
            var ent_wr = new DataWriter(ent_str);
            foreach (var entry in entries)
                entry.Write(ent_wr);
            ent_str.Flush();

            var ent_buf = ent_str.GetBuffer().AsSpan(0, (int)ent_str.Length);

            if (Encryption == RageArchiveEncryption7.AES)
            {
                AesEncryption.EncryptData(ent_buf, aesKey);
            }
            else if (Encryption == RageArchiveEncryption7.NG)
            {
                GTA5Crypto.EncryptData(ent_buf, ngKey);
            }


            // names...
            var n_str = new MemoryStream();
            var n_wr = new DataWriter(n_str);
            //foreach (var entry in entries)
            //    n_wr.Write(entry.Name);
            foreach (var entry in nameDict)
                n_wr.Write(entry.Key);

            n_wr.Write(new byte[16 - (n_wr.Length % 16)]);
            n_str.Flush();

            var n_buf = n_str.GetBuffer().AsSpan(0, (int)n_str.Length);

            if (Encryption == RageArchiveEncryption7.AES)
            {
                AesEncryption.EncryptData(n_buf, aesKey);
            }
            else if (Encryption == RageArchiveEncryption7.NG)
            {
                GTA5Crypto.EncryptData(n_buf, ngKey);
            }

            writer.Position = 0;
            writer.Write((uint)IDENT);
            writer.Write((uint)entries.Count);
            writer.Write((uint)n_buf.Length);

            if (Encryption == RageArchiveEncryption7.None)
            {
                // For OpenIV compatibility
                Encryption = RageArchiveEncryption7.OPEN;
            }

            writer.Write((uint)Encryption);
            writer.Write(ent_buf);
            writer.Write(n_buf);

            // restore position
            BaseStream.Position = positionBackup;
        }

        /// <summary>
        /// Releases all resources used by the archive.
        /// </summary>
        public void Dispose()
        {
            if (BaseStream != null)
                BaseStream.Dispose();

            BaseStream = null;
            Root = null;
        }

        private static void PatchLargeResourceFile(RageArchiveResourceFile7 resource, DataReader reader)
        {
            // there are sometimes resources with length=0xffffff which actually
            // means length>=0xffffff
            if (resource.FileSize == 0xFFFFFF)
            {
                Span<byte> buffer = stackalloc byte[16];
                reader.Position = 512 * resource.FileOffset;
                reader.ReadBytes(buffer);
                resource.FileSize = ((uint)buffer[7] << 0) | ((uint)buffer[14] << 8) | ((uint)buffer[5] << 16) | ((uint)buffer[2] << 24);
            }
        }

        private static void PatchLargeResourceFile(RageArchiveResourceFile7 resource, DataWriter writer)
        {
            // there are sometimes resources with length>=0xffffff which actually
            // means length=0xffffff
            // -> we therefore just cut the file size
            if (resource.FileSize > 0xFFFFFF)
            {
                Span<byte> buffer = stackalloc byte[16];
                buffer[7] = (byte)((resource.FileSize >> 0) & 0xFF);
                buffer[14] = (byte)((resource.FileSize >> 8) & 0xFF);
                buffer[5] = (byte)((resource.FileSize >> 16) & 0xFF);
                buffer[2] = (byte)((resource.FileSize >> 24) & 0xFF);

                if (writer.Length > 512 * resource.FileOffset)
                {
                    writer.Position = 512 * resource.FileOffset;
                    writer.Write(buffer);
                }

                resource.FileSize = 0xFFFFFF;
            }
        }
    }

    /// <summary>
    /// Represents a directory in an RPFv7 archive.
    /// </summary>
    public class RageArchiveDirectory7 : IRageArchiveEntry7
    {
        public uint NameOffset { get; set; }
        public uint EntriesIndex { get; set; }
        public uint EntriesCount { get; set; }

        public string Name { get; set; }
        public List<RageArchiveDirectory7> Directories = new List<RageArchiveDirectory7>();
        public List<IRageArchiveEntry7> Files = new List<IRageArchiveEntry7>();

        /// <summary>
        /// Reads the directory entry.
        /// </summary>
        public void Read(DataReader reader)
        {
            this.NameOffset = reader.ReadUInt32();

            uint ident = reader.ReadUInt32();
            if (ident != 0x7FFFFF00)
                throw new Exception("Error in RPF7 directory entry.");

            this.EntriesIndex = reader.ReadUInt32();
            this.EntriesCount = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the directory entry.
        /// </summary>
        public void Write(DataWriter writer)
        {
            writer.Write(this.NameOffset);
            writer.Write((uint)0x7FFFFF00);
            writer.Write(this.EntriesIndex);
            writer.Write(this.EntriesCount);
        }
    }

    /// <summary>
    /// Represents a binary file in an RPFv7 archive.
    /// </summary>
    public class RageArchiveBinaryFile7 : IRageArchiveFileEntry7
    {
        public uint NameOffset { get; set; }
        public uint FileSize { get; set; }
        public uint FileOffset { get; set; }
        public uint FileUncompressedSize { get; set; }
        public uint Flags { get; set; }
        
        public bool IsEncrypted
        {
            get => (Flags & 0x1u) == 0x1u;
            set
            {
                if (value)
                    Flags |= 0x1u;
                else
                    Flags &= ~0x1u;
            }
        }

        public string Name { get; set; }

        /// <summary>
        /// Reads the binary file entry.
        /// </summary>
        public void Read(DataReader reader)
        {
            NameOffset = reader.ReadUInt16();

            Span<byte> buf1 = stackalloc byte[3];
            reader.ReadBytes(buf1);
            FileSize = (uint)buf1[0] + (uint)(buf1[1] << 8) + (uint)(buf1[2] << 16);

            Span<byte> buf2 = stackalloc byte[3];
            reader.ReadBytes(buf2);
            FileOffset = (uint)buf2[0] + (uint)(buf2[1] << 8) + (uint)(buf2[2] << 16);

            FileUncompressedSize = reader.ReadUInt32();
            Flags = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the binary file entry.
        /// </summary>
        public void Write(DataWriter writer)
        {
            writer.Write((ushort)NameOffset);

            Span<byte> buf1 = stackalloc byte[3] 
            {
                (byte)((FileSize >> 0) & 0xFF),
                (byte)((FileSize >> 8) & 0xFF),
                (byte)((FileSize >> 16) & 0xFF)
            };
            writer.Write(buf1);

            Span<byte> buf2 = stackalloc byte[3] 
            {
                (byte)((FileOffset >> 0) & 0xFF),
                (byte)((FileOffset >> 8) & 0xFF),
                (byte)((FileOffset >> 16) & 0xFF)
            };
            writer.Write(buf2);

            writer.Write(FileUncompressedSize);

            writer.Write(Flags);
        }
    }

    /// <summary>
    /// Represents a resource file in an RPFv7 archive.
    /// </summary>
    public class RageArchiveResourceFile7 : IRageArchiveFileEntry7
    {
        public uint NameOffset { get; set; }
        public uint FileSize { get; set; }
        public uint FileOffset { get; set; }
        public DatResourceInfo ResourceInfo { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Reads the resource file entry.
        /// </summary>
        public void Read(DataReader reader)
        {
            NameOffset = reader.ReadUInt16();

            Span<byte> buf1 = stackalloc byte[3];
            reader.ReadBytes(buf1);
            FileSize = (uint)buf1[0] + (uint)(buf1[1] << 8) + (uint)(buf1[2] << 16);

            Span<byte> buf2 = stackalloc byte[3];
            reader.ReadBytes(buf2);
            FileOffset = ((uint)buf2[0] + (uint)(buf2[1] << 8) + (uint)(buf2[2] << 16)) & 0x7FFFFF;

            var VirtualFlags = reader.ReadUInt32();
            var PhysicalFlags = reader.ReadUInt32();

            ResourceInfo = new DatResourceInfo(VirtualFlags, PhysicalFlags);
        }

        /// <summary>
        /// Writes the resource file entry.
        /// </summary>
        public void Write(DataWriter writer)
        {
            writer.Write((ushort)NameOffset);

            Span<byte> buf1 = stackalloc byte[3] 
            { 
                (byte)((FileSize >> 0) & 0xFF),
                (byte)((FileSize >> 8) & 0xFF),
                (byte)((FileSize >> 16) & 0xFF)
            };

            writer.Write(buf1);

            Span<byte> buf2 = stackalloc byte[3]  
            {
                (byte)((FileOffset >> 0) & 0xFF),
                (byte)((FileOffset >> 8) & 0xFF),
                (byte)(((FileOffset >> 16) & 0xFF) | 0x80)
            };

            writer.Write(buf2);

            writer.Write(ResourceInfo.VirtualFlags);
            writer.Write(ResourceInfo.PhysicalFlags);
        }
    }
}