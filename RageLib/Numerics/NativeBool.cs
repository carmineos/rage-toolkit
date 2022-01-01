// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics
{
    public struct NativeBool
    {
        private readonly byte value;

        public NativeBool(bool boolean)
        {
            value = (byte)(boolean ? 1 : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(NativeBool value) => value.value != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(NativeBool value) => value.value == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(NativeBool nativeBool) => nativeBool.value != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NativeBool(bool boolean) => new NativeBool(boolean);
    }
}
