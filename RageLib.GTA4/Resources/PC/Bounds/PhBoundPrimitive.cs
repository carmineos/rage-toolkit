// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public struct PhBoundPrimitive : IResourceStruct<PhBoundPrimitive>
    {
        public Vector3 Normal;
        private uint MaterialAndArea;
        public short VertexIndex1;
        public short VertexIndex2;
        public short VertexIndex3;
        public short VertexIndex4;
        public short NeighborIndex1;
        public short NeighborIndex2;
        public short NeighborIndex3;
        public short NeighborIndex4;

        public byte Material
        {
            get => (byte)(MaterialAndArea & 0xFF);
            set => MaterialAndArea &= value;
        }

        public float Area
        {
            get => BitConverter.UInt32BitsToSingle(MaterialAndArea & 0xFFFFFF00);
            set => MaterialAndArea = (BitConverter.SingleToUInt32Bits(value) & 0xFFFFFF00);
        }

        public PhBoundPrimitive ReverseEndianness()
        {
            return new PhBoundPrimitive()
            {
                Normal = EndiannessExtensions.ReverseEndianness(Normal),
                MaterialAndArea = EndiannessExtensions.ReverseEndianness(MaterialAndArea),
                VertexIndex1 = EndiannessExtensions.ReverseEndianness(VertexIndex1),
                VertexIndex2 = EndiannessExtensions.ReverseEndianness(VertexIndex2),
                VertexIndex3 = EndiannessExtensions.ReverseEndianness(VertexIndex3),
                VertexIndex4 = EndiannessExtensions.ReverseEndianness(VertexIndex4),
                NeighborIndex1 = EndiannessExtensions.ReverseEndianness(NeighborIndex1),
                NeighborIndex2 = EndiannessExtensions.ReverseEndianness(NeighborIndex2),
                NeighborIndex3 = EndiannessExtensions.ReverseEndianness(NeighborIndex3),
                NeighborIndex4 = EndiannessExtensions.ReverseEndianness(NeighborIndex4),
            };
        }
    }
}
