/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Cryptography;
using System;
using System.Runtime.InteropServices;

namespace RageLib.GTA5.Cryptography
{





    /// <summary>
    /// Represents a GTA5 encryption algorithm.
    /// </summary>
    public class GTA5Crypto : IEncryptionAlgorithm
    {
        public byte[] Key { get; set; }

        public static uint GetKeyIndex(string name, uint length)
        {
            var hash = GTA5Hash.CalculateHash(name);
            return (hash + length + (101 - 40)) % 0x65;
        }

        ////////////////////////////////////////////////////////////////////////////
        // decryption
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Decrypts data.
        /// </summary>
        public byte[] Decrypt(byte[] data)
        {
            return Decrypt(data, Key);
        }

        /// <summary>
        /// Decrypts data.
        /// </summary>
        public static void DecryptUnsafe(Span<byte> data, ReadOnlySpan<byte> key)
        {
            var keyuints = MemoryMarshal.Cast<byte,uint>(key);

            for (int blockIndex = 0; blockIndex < data.Length / 16; blockIndex++)
            {
                DecryptBlock(data.Slice(16 * blockIndex, 16), keyuints);
            }

            //    // Just do nothing
            //if (data.Length % 16 != 0)
            //{
            //    var left = data.Length % 16;
            //}
        }

        /// <summary>
        /// Decrypts data.
        /// </summary>
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            // TODO: Actually it should be safe to decrypt data directly without copy
            var decryptedData = new byte[data.Length];

            var keyuints = MemoryMarshal.Cast<byte, uint>(key);

            Span<byte> block = stackalloc byte[16];

            for (int blockIndex = 0; blockIndex < data.Length / 16; blockIndex++)
            {
                data.AsSpan(16 * blockIndex, 16).CopyTo(block);
                DecryptBlock(block, keyuints);
                block.CopyTo(decryptedData.AsSpan(16 * blockIndex, 16));
            }

            if (data.Length % 16 != 0)
            {
                var left = data.Length % 16;
                data.AsSpan(data.Length - left, left).CopyTo(decryptedData.AsSpan(data.Length - left, left));
            }

            return decryptedData;
        }

        private static void DecryptBlock(Span<byte> data, ReadOnlySpan<uint> key)
        {
            Span<uint> subkey = stackalloc uint[4];

            for (int k = 0; k <= 16; k++)
            {
                key.Slice(4 * k, 4).CopyTo(subkey);

                if (k == 0 || k == 1 || k == 16)
                    DecryptRoundA(data, subkey, GTA5Constants.PC_NG_DECRYPT_TABLES[k]);
                else
                    DecryptRoundB(data, subkey, GTA5Constants.PC_NG_DECRYPT_TABLES[k]);
            }
        }

        // round 1,2,16
        private static void DecryptRoundA(Span<byte> data, ReadOnlySpan<uint> key, uint[][] table)
        {
            Span<uint> decrypted = stackalloc uint[4];
            
            decrypted[0] =
                table[0][data[0]] ^
                table[1][data[1]] ^
                table[2][data[2]] ^
                table[3][data[3]] ^
                key[0];
            decrypted[1] =
                table[4][data[4]] ^
                table[5][data[5]] ^
                table[6][data[6]] ^
                table[7][data[7]] ^
                key[1];
            decrypted[2] =
                table[8][data[8]] ^
                table[9][data[9]] ^
                table[10][data[10]] ^
                table[11][data[11]] ^
                key[2];
            decrypted[3] =
                table[12][data[12]] ^
                table[13][data[13]] ^
                table[14][data[14]] ^
                table[15][data[15]] ^
                key[3];

            MemoryMarshal.AsBytes(decrypted).CopyTo(data);
        }

        // round 3-15
        private static void DecryptRoundB(Span<byte> data, ReadOnlySpan<uint> key, uint[][] table)
        {
            Span<uint> decrypted = stackalloc uint[4];

            decrypted[0] =
                table[0][data[0]] ^
                table[7][data[7]] ^
                table[10][data[10]] ^
                table[13][data[13]] ^
                key[0];
            decrypted[1] =
                table[1][data[1]] ^
                table[4][data[4]] ^
                table[11][data[11]] ^
                table[14][data[14]] ^
                key[1];
            decrypted[2] =
                table[2][data[2]] ^
                table[5][data[5]] ^
                table[8][data[8]] ^
                table[15][data[15]] ^
                key[2];
            decrypted[3] =
                table[3][data[3]] ^
                table[6][data[6]] ^
                table[9][data[9]] ^
                table[12][data[12]] ^
                key[3];

            MemoryMarshal.AsBytes(decrypted).CopyTo(data);
        }
        
    }
}