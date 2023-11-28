// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.IO;
using System.IO.Compression;

namespace RageLib.Compression
{
    /// <summary>
    /// Represents a deflate compression algorithm.
    /// </summary>
    public class DeflateCompression : ICompressionAlgorithm
    {
        /// <summary>
        /// Compresses data.
        /// </summary>
        public byte[] Compress(byte[] data)
        {
            return CompressData(data);
        }

        /// <summary>
        /// Decompresses data.
        /// </summary>
        public byte[] Decompress(byte[] data, int decompressedLength)
        {
            return DecompressData(data, decompressedLength);
        }

        /// <summary>
        /// Compresses data.
        /// </summary>
        public static byte[] CompressData(byte[] data)
        {
            var dataStream = new MemoryStream(data);
            var deflateStream = new DeflateStream(dataStream, CompressionMode.Compress);

            deflateStream.Write(data, 0, data.Length);

            var buffer = new byte[dataStream.Length];
            dataStream.Position = 0;
            dataStream.Read(buffer, 0, (int)dataStream.Length);

            deflateStream.Close();

            return buffer;
        }

        /// <summary>
        /// Decompresses data.
        /// </summary>
        public static byte[] DecompressData(byte[] data, int decompressedLength)
        {
            var dataStream = new MemoryStream(data);
            var deflateStream = new DeflateStream(dataStream, CompressionMode.Decompress);

            var buffer = new byte[decompressedLength];
            deflateStream.ReadExactly(buffer, 0, decompressedLength);
            deflateStream.Close();

            return buffer;
        }
    }
}