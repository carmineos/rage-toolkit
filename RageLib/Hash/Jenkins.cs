// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Hash
{
    public class Jenkins
    {
        // source: http://en.wikipedia.org/wiki/Jenkins_hash_function
        public static uint Hash(string key)
        {
            return Hash(key.AsSpan());
        }

        public static uint Hash(ReadOnlySpan<char> key)
        {
            uint hash = 0;
            for (int i = 0; i < key.Length; ++i)
            {
                hash += (byte)key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }
    }
}
