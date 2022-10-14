// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Security.Cryptography;

namespace RageLib.Cryptography
{
    /// <summary>
    /// Represents an AES encryption algorithm.
    /// </summary>
    public class AesEncryption : IEncryptionAlgorithm
    {
        public byte[] Key { get; set; }
        public int Rounds { get; set; }

        public AesEncryption(byte[] key, int rounds = 1)
        {
            this.Key = key;
            this.Rounds = rounds;
        }

        /// <summary>
        /// Encrypts data.
        /// </summary>
        public byte[] Encrypt(byte[] data)
        {
            return Encrypt(data, Key, Rounds);
        }

        /// <summary>
        /// Decrypts data.
        /// </summary>
        public byte[] Decrypt(byte[] data)
        {
            return Decrypt(data, Key, Rounds);
        }

        /// <summary>
        /// Encrypts data.
        /// </summary>
        public static byte[] Encrypt(byte[] data, byte[] key, int rounds = 1)
        {
            var rijndael = Aes.Create();
            rijndael.KeySize = 256;
            rijndael.Key = key;
            rijndael.BlockSize = 128;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.None;

            var buffer = (byte[])data.Clone();
            var length = data.Length - data.Length % 16;

            // encrypt...
            if (length > 0)
            {
                var encryptor = rijndael.CreateEncryptor();
                for (var roundIndex = 0; roundIndex < rounds; roundIndex++)
                    encryptor.TransformBlock(buffer, 0, length, buffer, 0);
            }

            return buffer;
        }

        /// <summary>
        /// Decrypts data.
        /// </summary>
        public static byte[] Decrypt(byte[] data, byte[] key, int rounds = 1)
        {
            var rijndael = Aes.Create();
            rijndael.KeySize = 256;
            rijndael.Key = key;
            rijndael.BlockSize = 128;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.None;

            var buffer = (byte[])data.Clone();
            var length = data.Length - data.Length % 16;

            // decrypt...
            if (length > 0)
            {
                var decryptor = rijndael.CreateDecryptor();
                for (var roundIndex = 0; roundIndex < rounds; roundIndex++)
                    decryptor.TransformBlock(buffer, 0, length, buffer, 0);
            }

            return buffer;
        }


        public static void EncryptData(byte[] data, byte[] key, int rounds = 1)
        {
            var rijndael = Aes.Create();
            rijndael.KeySize = 256;
            rijndael.Key = key;
            rijndael.BlockSize = 128;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.None;

            var length = data.Length - data.Length % 16;

            // encrypt...
            if (length > 0)
            {
                var encryptor = rijndael.CreateEncryptor();
                for (var roundIndex = 0; roundIndex < rounds; roundIndex++)
                    encryptor.TransformBlock(data, 0, length, data, 0);
            }
        }

        public static void DecryptData(byte[] data, byte[] key, int rounds = 1)
        {
            var rijndael = Aes.Create();
            rijndael.KeySize = 256;
            rijndael.Key = key;
            rijndael.BlockSize = 128;
            rijndael.Mode = CipherMode.ECB;
            rijndael.Padding = PaddingMode.None;

            var length = data.Length - data.Length % 16;

            // decrypt...
            if (length > 0)
            {
                var decryptor = rijndael.CreateDecryptor();
                for (var roundIndex = 0; roundIndex < rounds; roundIndex++)
                    decryptor.TransformBlock(data, 0, length, data, 0);
            }

        }
    }
}