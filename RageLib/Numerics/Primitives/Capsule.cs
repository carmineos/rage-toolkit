using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics.Primitives
{
    public readonly struct Capsule : IEquatable<Capsule>, IPrimitive
    {
        public readonly Vector3 Vertex1;
        public readonly Vector3 Vertex2;
        public readonly float Radius;

        public Capsule(Vector3 center1, Vector3 center2, float radius)
        {
            Vertex1 = center1;
            Vertex2 = center2;
            Radius = radius;
        }

        public bool Equals(Capsule other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is Capsule other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Capsule left, Capsule right) =>
            (left.Vertex1 == right.Vertex1) &&
            (left.Vertex2 == right.Vertex2) &&
            (left.Radius == right.Radius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Capsule left, Capsule right) =>
            (left.Vertex1 != right.Vertex1) ||
            (left.Vertex2 != right.Vertex2) ||
            (left.Radius != right.Radius);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Vertex1);
                hashCode.Add(Vertex2);
                hashCode.Add(Radius);
            }
            return hashCode.ToHashCode();
        }

        public BoundingBox GetBoundingBox()
        {
            var radius = new Vector3(Radius);
            var min = Vector3.Min(Vertex1, Vertex2) - radius;
            var max = Vector3.Max(Vertex1, Vertex2) + radius;
            return new BoundingBox(min, max);
        }
    }
}
