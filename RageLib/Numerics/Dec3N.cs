using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace RageLib.Numerics
{
    public readonly struct Dec3N : IEquatable<Dec3N>
    {
        public readonly uint packedData;

        public Dec3N(uint value)
        {
            packedData = value;
        }

        public Dec3N(float fx, float fy, float fz, float fw)
        {
            ushort x = SetComponentTwoComplement10Bits(fx);
            ushort y = SetComponentTwoComplement10Bits(fy);
            ushort z = SetComponentTwoComplement10Bits(fz);
            byte w = SetComponentTwoComplement2Bits(fw);
            packedData = (uint)((w << 30) | (z << 20) | (y << 10) | x);
        }

        public readonly float X => GetComponentTwoComplement10Bits(packedData);

        public readonly float Y => GetComponentTwoComplement10Bits(packedData >> 10);

        public readonly float Z => GetComponentTwoComplement10Bits(packedData >> 20);

        public readonly float W => GetComponentTwoComplement2Bits(packedData >> 30);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetComponentTwoComplement10Bits(uint value)
        {
            uint masked = value & 0x3FF;
            return (masked & 0x200) == 0x200
                ? (((~masked + 1) & 0x3FF) / -511.0f)
                : ((masked & 0x1FF) / 511.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float GetComponentTwoComplement2Bits(uint value)
        {
            uint masked = value & 0x3;
            return (masked & 0x2) == 0x2
                ? (((~masked + 1) & 0x3) / -1.0f)
                : ((masked & 0x1) / 1.0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ushort SetComponentTwoComplement10Bits(float value)
        {
            uint c = (uint)MathF.Round(Math.Abs(value) * 511.0f);
            return (ushort)(value < 0.0f
                ? ((~c + 1) & 0x1FF) | 0x200
                : (c & 0x1FF));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte SetComponentTwoComplement2Bits(float value)
        {
            return (byte)MathF.Round(Math.Clamp(value, -2.0f, 1.0f));
        }

        public bool Equals(Dec3N other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is Dec3N other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Dec3N left, Dec3N right) => left.packedData == right.packedData;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Dec3N left, Dec3N right) => left.packedData != right.packedData;

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(packedData);
            }
            return hashCode.ToHashCode();
        }

        public override string ToString() => ToString(format: null, formatProvider: null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder(9 + (separator.Length * 3))
                .Append('<')
                .Append(X.ToString(format, formatProvider))
                .Append(separator)
                .Append(' ')
                .Append(Y.ToString(format, formatProvider))
                .Append(separator)
                .Append(' ')
                .Append(Z.ToString(format, formatProvider))
                .Append(separator)
                .Append(' ')
                .Append(W.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
