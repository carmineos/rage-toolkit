// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Runtime.CompilerServices;

namespace RageLib.Helpers.Xml
{
    public static class HexConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHex(int value) => $"0x{value:X8}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt32(string value) => Convert.ToInt32(value, 16);
    }
}
