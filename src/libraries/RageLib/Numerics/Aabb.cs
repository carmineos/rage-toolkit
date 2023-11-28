﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Numerics
{
    // spdAABB
    /// <summary>Defines an axis-aligned bounding box in three dimensional space.</summary>
    public readonly struct Aabb : IEquatable<Aabb>, IResourceStruct<Aabb>
    {
        public readonly Vector4 Min;
        public readonly Vector4 Max;

        public Aabb(Vector4 min, Vector4 max)
        {
            Min = min;
            Max = max;
        }

        public Vector4 Center => (Max + Min) * 0.5f;

        public Vector4 Size => Max - Min;

        public bool Equals(Aabb other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Aabb other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Aabb left, Aabb right) =>
            left.Min == right.Min &&
            left.Max == right.Max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Aabb left, Aabb right) =>
            left.Min != right.Min ||
            left.Max != right.Max;

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Min);
                hashCode.Add(Max);
            }
            return hashCode.ToHashCode();
        }

        public Aabb ReverseEndianness()
        {
            return new Aabb(
                EndiannessExtensions.ReverseEndianness(Min),
                EndiannessExtensions.ReverseEndianness(Max));
        }
    }
}
