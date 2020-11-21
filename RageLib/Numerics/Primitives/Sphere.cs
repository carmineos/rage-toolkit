using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics.Primitives
{
    public readonly struct Sphere : IEquatable<Sphere>, IPrimitive
    {
        public readonly Vector3 Vertex;
        public readonly float Radius;

        public Sphere(Vector3 center, float radius)
        {
            Vertex = center;
            Radius = radius;
        }

        public bool Equals(Sphere other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is Sphere other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Sphere left, Sphere right) =>
            (left.Vertex == right.Vertex) &&
            (left.Radius == right.Radius);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Sphere left, Sphere right) =>
            (left.Vertex != right.Vertex) ||
            (left.Radius != right.Radius);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Vertex);
                hashCode.Add(Radius);
            }
            return hashCode.ToHashCode();
        }

        public BoundingBox GetBoundingBox()
        {
            var min = new Vector3(Vertex.X - Radius, Vertex.Y - Radius, Vertex.Z - Radius);
            var max = new Vector3(Vertex.X + Radius, Vertex.Y + Radius, Vertex.Z + Radius);
            return new BoundingBox(min, max);
        }
    }
}
