using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics.Primitives
{
    public readonly struct Triangle : IEquatable<Triangle>, IPrimitive
    {
        public readonly Vector3 Vertex1;
        public readonly Vector3 Vertex2;
        public readonly Vector3 Vertex3;

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
        }

        public bool Equals(Triangle other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => (obj is Triangle other) && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Triangle left, Triangle right) =>
            (left.Vertex1 == right.Vertex1) &&
            (left.Vertex2 == right.Vertex2) &&
            (left.Vertex3 == right.Vertex3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Triangle left, Triangle right) =>
            (left.Vertex1 != right.Vertex1) ||
            (left.Vertex2 != right.Vertex2) ||
            (left.Vertex3 != right.Vertex3);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Vertex1);
                hashCode.Add(Vertex2);
                hashCode.Add(Vertex3);
            }
            return hashCode.ToHashCode();
        }

        public float GetArea()
        {
            return Vector3.Cross(Vertex2 - Vertex1, Vertex3 - Vertex1).Length() * 0.5f;
        }

        public BoundingBox GetBoundingBox()
        {
            var min = Vector3.Min(Vector3.Min(Vertex1, Vertex2), Vertex3);
            var max = Vector3.Max(Vector3.Max(Vertex1, Vertex2), Vertex3);
            return new BoundingBox(min, max);
        }
    }
}
