using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics
{
    /// <summary>Defines an axis-aligned bounding box in three dimensional space.</summary>
    public readonly struct BoundingBox : IEquatable<BoundingBox>
    {
        public readonly Vector3 Min;
        public readonly Vector3 Max;

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public Vector3 Center => (Max + Min) * 0.5f;
        
        public Vector3 Size => Max - Min;

        public bool Equals(BoundingBox other) => this == other;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is BoundingBox other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(BoundingBox left, BoundingBox right) =>
            (left.Min == right.Min) &&
            (left.Max == right.Max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BoundingBox left, BoundingBox right) =>
            (left.Min != right.Min) ||
            (left.Max != right.Max);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Min);
                hashCode.Add(Max);
            }
            return hashCode.ToHashCode();
        }
    }
}
