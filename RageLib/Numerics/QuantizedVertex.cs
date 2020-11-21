using System;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics
{
    /// <summary>Defines a quantized vertex in three dimensional space.</summary>
    public readonly struct QuantizedVertex : IEquatable<QuantizedVertex>
    {
        public readonly short X;
        public readonly short Y;
        public readonly short Z;

        public QuantizedVertex(short x, short y, short z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(QuantizedVertex other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is QuantizedVertex other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(QuantizedVertex left, QuantizedVertex right) => 
            (left.X == right.X) &&
            (left.Y == right.Y) &&
            (left.Z == right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(QuantizedVertex left, QuantizedVertex right) =>
            (left.X != right.X) ||
            (left.Y != right.Y) ||
            (left.Z != right.Z);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(X);
                hashCode.Add(Y);
                hashCode.Add(Z);
            }
            return hashCode.ToHashCode();
        }
    }
}
