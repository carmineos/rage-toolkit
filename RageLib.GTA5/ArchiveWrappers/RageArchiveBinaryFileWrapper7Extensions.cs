using RageLib.Compression;
using RageLib.Cryptography;
using RageLib.GTA5.Archives;
using RageLib.GTA5.Cryptography;
using System;
using System.Buffers;
using System.IO;
using System.IO.Compression;

namespace RageLib.GTA5.ArchiveWrappers
{
    public static class RageArchiveBinaryFileWrapper7Extensions
    {
        public static void ExportUncompressed(this RageArchiveBinaryFileWrapper7 file, string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                ExportUncompressed(file, stream);
        }

        public static void ExportUncompressed(this RageArchiveBinaryFileWrapper7 file, Stream stream)
        {
            // export
            var length = (int)file.Size;
            using var ms = new MemoryStream(length);
            file.Export(ms);
            ms.Position = 0;

            var buf = ms.GetBuffer();
            var span = buf.AsSpan(0, length);

            //var buf = ArrayPool<byte>.Shared.Rent(length);
            //var span = buf.AsSpan(0, length);
            //ms.Position = 0;
            //ms.Read(span);

            var uncompressedSize = (uint)file.UncompressedSize;

            // decrypt...
            if (file.IsEncrypted)
            {
                if (file.Encryption == RageArchiveEncryption7.AES)
                {
                    // TODO: edit this to reduce heap allocations
                    buf = AesEncryption.DecryptData(buf, GTA5Constants.PC_AES_KEY);
                }
                else if (file.Encryption == RageArchiveEncryption7.NG)
                {
                    var indexKey = GTA5Crypto.GetKeyIndex(file.Name, uncompressedSize);
                    GTA5Crypto.DecryptData(span, GTA5Constants.PC_NG_KEYS[indexKey]);
                }
            }

            byte[] bufnew = null;
            
            // decompress...
            if (file.IsCompressed)
            {
                var def = new DeflateStream(new MemoryStream(buf, 0, length), CompressionMode.Decompress);
                bufnew = ArrayPool<byte>.Shared.Rent((int)uncompressedSize);
                def.ReadAll(bufnew, 0, (int)uncompressedSize);
                buf = bufnew;
            }

            stream.Write(buf.AsSpan(0, (int)uncompressedSize));
            
            if(bufnew != null)
                ArrayPool<byte>.Shared.Return(bufnew);
        }
    }
}
