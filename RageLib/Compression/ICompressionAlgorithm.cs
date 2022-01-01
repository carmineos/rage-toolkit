// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Compression
{
    /// <summary>
    /// Represents a compression algorithm.
    /// </summary>
    public interface ICompressionAlgorithm
    {
        /// <summary>
        /// Compresses data.
        /// </summary>
        byte[] Compress(byte[] data);

        /// <summary>
        /// Decompresses data.
        /// </summary>
        byte[] Decompress(byte[] data, int decompressedLength);
    }
}