using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics.Primitives
{
    public readonly struct Box : IEquatable<Box>, IPrimitive
    {
        public readonly Vector3 Vertex1;
        public readonly Vector3 Vertex2;
        public readonly Vector3 Vertex3;
        public readonly Vector3 Vertex4;

        public Box(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
            Vertex4 = v4;
        }

        public bool Equals(Box other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is Box other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Box left, Box right) =>
            (left.Vertex1 == right.Vertex1) &&
            (left.Vertex2 == right.Vertex2) &&
            (left.Vertex3 == right.Vertex3) &&
            (left.Vertex4 == right.Vertex4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Box left, Box right) =>
            (left.Vertex1 != right.Vertex1) ||
            (left.Vertex2 != right.Vertex2) ||
            (left.Vertex3 != right.Vertex3) ||
            (left.Vertex4 != right.Vertex4);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Vertex1);
                hashCode.Add(Vertex2);
                hashCode.Add(Vertex3);
                hashCode.Add(Vertex4);
            }
            return hashCode.ToHashCode();
        }

        public BoundingBox GetBoundingBox()
        {
            var min = Vector3.Min(Vector3.Min(Vector3.Min(Vertex1, Vertex2), Vertex3), Vertex4);
            var max = Vector3.Max(Vector3.Max(Vector3.Max(Vertex1, Vertex2), Vertex3), Vertex4);
            return new BoundingBox(min, max);
        }
    }
}
