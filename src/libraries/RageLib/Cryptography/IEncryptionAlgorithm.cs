// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Cryptography
{
    /// <summary>
    /// Represents an encryption algorithm.
    /// </summary>
    public interface IEncryptionAlgorithm
    {
        /// <summary>
        /// Encrypts data.
        /// </summary>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decrypts data.
        /// </summary>
        byte[] Decrypt(byte[] data);
    }
}