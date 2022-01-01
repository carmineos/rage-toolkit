// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Data
{
    public enum Endianness
    {
        LittleEndian,
        BigEndian
    }

    public static class EndiannessExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsHostArchitecture(this Endianness endianness) =>
            BitConverter.IsLittleEndian
            ? endianness == Endianness.LittleEndian
            : endianness == Endianness.BigEndian;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReverseEndianness(byte value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ReverseEndianness(sbyte value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReverseEndianness(short value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReverseEndianness(ushort value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReverseEndianness(int value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseEndianness(uint value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReverseEndianness(long value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReverseEndianness(ulong value) => BinaryPrimitives.ReverseEndianness(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReverseEndianness(double value)
        {
            var int64 = BitConverter.DoubleToUInt64Bits(value);
            var reversed = BinaryPrimitives.ReverseEndianness(int64);
            return BitConverter.UInt64BitsToDouble(reversed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReverseEndianness(float value)
        {
            var int32 = BitConverter.SingleToUInt32Bits(value);
            var reversed = BinaryPrimitives.ReverseEndianness(int32);
            return BitConverter.UInt32BitsToSingle(reversed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Half ReverseEndianness(Half value)
        {
            var int16 = BitConverter.HalfToUInt16Bits(value);
            var reversed = BinaryPrimitives.ReverseEndianness(int16);
            return BitConverter.UInt16BitsToHalf(reversed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ReverseEndianness(Vector2 value)
        {
            return new Vector2(
                ReverseEndianness(value.X),
                ReverseEndianness(value.Y)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReverseEndianness(Vector3 value)
        {
            return new Vector3(
                ReverseEndianness(value.X),
                ReverseEndianness(value.Y),
                ReverseEndianness(value.Z)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReverseEndianness(Vector4 value)
        {
            return new Vector4(
                ReverseEndianness(value.X),
                ReverseEndianness(value.Y),
                ReverseEndianness(value.Z),
                ReverseEndianness(value.W)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ReverseEndianness(Quaternion value)
        {
            return new Quaternion(
                ReverseEndianness(value.X),
                ReverseEndianness(value.Y),
                ReverseEndianness(value.Z),
                ReverseEndianness(value.W)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 ReverseEndianness(Matrix4x4 value)
        {
            Matrix4x4 mat;

            mat.M11 = ReverseEndianness(value.M11);
            mat.M12 = ReverseEndianness(value.M12);
            mat.M13 = ReverseEndianness(value.M13);
            mat.M14 = ReverseEndianness(value.M14);
            mat.M21 = ReverseEndianness(value.M21);
            mat.M22 = ReverseEndianness(value.M22);
            mat.M23 = ReverseEndianness(value.M23);
            mat.M24 = ReverseEndianness(value.M24);
            mat.M31 = ReverseEndianness(value.M31);
            mat.M32 = ReverseEndianness(value.M32);
            mat.M33 = ReverseEndianness(value.M33);
            mat.M34 = ReverseEndianness(value.M34);
            mat.M41 = ReverseEndianness(value.M41);
            mat.M42 = ReverseEndianness(value.M42);
            mat.M43 = ReverseEndianness(value.M43);
            mat.M44 = ReverseEndianness(value.M44);

            return mat;
        }
    }
}