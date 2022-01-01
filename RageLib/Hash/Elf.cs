// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Hash
{
    public class Elf
    {
        // source: https://en.wikipedia.org/wiki/PJW_hash_function
        public static uint Hash(ReadOnlySpan<char> str)
        {
            uint hash = 0;
            uint high;

            for (int i = 0; i < str.Length; i++)
            {
                var c = (byte)str[i];

                hash = (hash << 4) + c;

                if ((high = hash & 0xF0000000) != 0)
                {
                    hash ^= (high >> 24);
                }

                hash &= ~high;
            }

            return hash;
        }

        public static uint HashUppercased(ReadOnlySpan<char> str)
        {
            uint hash = 0;
            uint high;

            for (int i = 0; i < str.Length; i++)
            {
                var c = (byte)str[i];
                if ((byte)(c - 'a') <= 25u) // to uppercase
                    c -= 32;

                hash = (hash << 4) + c;

                if ((high = hash & 0xF0000000) != 0)
                {
                    hash ^= (high >> 24);
                }

                hash &= ~high;
            }

            return hash;
        }

        public static uint Hash(string str)
        {
            return Hash(str.AsSpan());
        }

        public static uint HashUppercased(string str)
        {
            return HashUppercased(str.AsSpan());
        }
    }
}
