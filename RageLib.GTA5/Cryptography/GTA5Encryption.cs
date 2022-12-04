// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Cryptography;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
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

        public byte[] Encrypt(byte[] data)
        {
            return Encrypt(data, Key);
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
        public static void DecryptData(Span<byte> data, ReadOnlySpan<byte> key)
        {
            var keyuints = MemoryMarshal.Cast<byte,uint>(key);

            for (int blockIndex = 0; blockIndex < data.Length / 16; blockIndex++)
            {
                DecryptBlock(data.Slice(16 * blockIndex, 16), keyuints);
            }

            // Just do nothing
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
            for (int k = 0; k <= 16; k++)
            {
                ReadOnlySpan<uint> subkey = key.Slice(4 * k, 4);

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

        public static byte[] Encrypt(ReadOnlySpan<byte> data, ReadOnlySpan<byte> key)
        {
            var encryptedData = new byte[data.Length];

            Span<byte> block = stackalloc byte[16];

            for (int blockIndex = 0; blockIndex < data.Length / 16; blockIndex++)
            {
                data.Slice(16 * blockIndex, 16).CopyTo(block);
                EncryptBlock(block, key);
                block.CopyTo(encryptedData.AsSpan(16 * blockIndex, 16));
            }

            if (data.Length % 16 != 0)
            {
                var left = data.Length % 16;
                data.Slice(data.Length - left, left).CopyTo(encryptedData.AsSpan(data.Length - left, left));
            }

            return encryptedData;
        }

        public static void EncryptData(Span<byte> data, ReadOnlySpan<byte> key)
        {
            for (int blockIndex = 0; blockIndex < data.Length / 16; blockIndex++)
            {
                EncryptBlock(data.Slice(16 * blockIndex, 16), key);
            }

            // Just do nothing
            //if (data.Length % 16 != 0)
            //{
            //    var left = data.Length % 16;
            //}
        }

        private static void EncryptBlock(Span<byte> data, ReadOnlySpan<byte> key)
        {
            for (var k = 16; k >= 0; k--)
            {
                ReadOnlySpan<byte> subkey = key.Slice(16 * k, 16);

                if (k == 0 || k == 1 || k == 16)
                    EncryptRoundA(data, subkey, GTA5Constants.PC_NG_ENCRYPT_TABLES[k]);
                else
                    EncryptRoundB_LUT(data, subkey, GTA5Constants.PC_NG_ENCRYPT_LUTs[k]);
            }
        }

        private static void EncryptRoundA(Span<byte> data, ReadOnlySpan<byte> key, uint[][] table)
        {
            // Here we could edit data directly
            Span<byte> x = stackalloc byte[16];

            if (Vector.IsHardwareAccelerated)
            {
                Vector<byte> x1 = new Vector<byte>(data);
                Vector<byte> x2 = new Vector<byte>(key);
                Vector.Xor(x1, x2).CopyTo(x);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                    x[i] = (byte)(data[i] ^ key[i]);
            }

            Span<uint> encrypted = MemoryMarshal.Cast<byte, uint>(x);

            encrypted[0] =
                table[0][x[0]] ^
                table[1][x[1]] ^
                table[2][x[2]] ^
                table[3][x[3]];
            encrypted[1] =
                table[4][x[4]] ^
                table[5][x[5]] ^
                table[6][x[6]] ^
                table[7][x[7]];
            encrypted[2] =
                table[8][x[8]] ^
                table[9][x[9]] ^
                table[10][x[10]] ^
                table[11][x[11]];
            encrypted[3] =
                table[12][x[12]] ^
                table[13][x[13]] ^
                table[14][x[14]] ^
                table[15][x[15]];

            MemoryMarshal.AsBytes(encrypted).CopyTo(data);
        }

        private static void EncryptRoundB_LUT(Span<byte> data, ReadOnlySpan<byte> key, GTA5NGLUT[] lut)
        {
            Span<byte> x = stackalloc byte[16];

            if (Vector.IsHardwareAccelerated)
            {
                Vector<byte> x1 = new Vector<byte>(data);
                Vector<byte> x2 = new Vector<byte>(key);
                Vector.Xor(x1, x2).CopyTo(x);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                    x[i] = (byte)(data[i] ^ key[i]);
            }

            Span<uint> y = MemoryMarshal.Cast<byte, uint>(x);

            data[0] = lut[0].LookUp(y[0]);
            data[1] = lut[1].LookUp(y[1]);
            data[2] = lut[2].LookUp(y[2]);
            data[3] = lut[3].LookUp(y[3]);
            data[4] = lut[4].LookUp(y[1]);
            data[5] = lut[5].LookUp(y[2]);
            data[6] = lut[6].LookUp(y[3]);
            data[7] = lut[7].LookUp(y[0]);
            data[8] = lut[8].LookUp(y[2]);
            data[9] = lut[9].LookUp(y[3]);
            data[10] = lut[10].LookUp(y[0]);
            data[11] = lut[11].LookUp(y[1]);
            data[12] = lut[12].LookUp(y[3]);
            data[13] = lut[13].LookUp(y[0]);
            data[14] = lut[14].LookUp(y[1]);
            data[15] = lut[15].LookUp(y[2]);
        }
    }

    public class GTA5NGLUT
    {

        public byte[][] LUT0;
        public byte[][] LUT1;

        public byte[] Indices;

        public GTA5NGLUT()
        {
            LUT0 = new byte[256][];
            for (var i = 0; i < 256; i++)
                LUT0[i] = new byte[256];

            LUT1 = new byte[256][];
            for (var i = 0; i < 256; i++)
                LUT1[i] = new byte[256];

            Indices = new byte[65536];
        }

        public byte LookUp(uint value)
        {
            var num = (value & 0xFFFF0000u) >> 16;
            var num2 = (value & 0xFF00u) >> 8;
            var num3 = value & 0xFFu;
            return LUT0[LUT1[Indices[(int)num]][(int)num2]][(int)num3];
        }
    }
}