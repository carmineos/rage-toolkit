// Copyright � Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources;

namespace RageLib.Numerics
{
    public struct BVHNode : IResourceStruct<BVHNode>
    {
        public short MinX;
        public short MinY;
        public short MinZ;
        public short MaxX;
        public short MaxY;
        public short MaxZ;
        public short NodeId;
        public ushort ChildrenCount;

        public BVHNode ReverseEndianness()
        {
            return new BVHNode()
            {

                MinX = EndiannessExtensions.ReverseEndianness(MinX),
                MinY = EndiannessExtensions.ReverseEndianness(MinY),
                MinZ = EndiannessExtensions.ReverseEndianness(MinZ),
                MaxX = EndiannessExtensions.ReverseEndianness(MaxX),
                MaxY = EndiannessExtensions.ReverseEndianness(MaxY),
                MaxZ = EndiannessExtensions.ReverseEndianness(MaxZ),
                NodeId = EndiannessExtensions.ReverseEndianness(NodeId),
                ChildrenCount = EndiannessExtensions.ReverseEndianness(ChildrenCount),
            };
        }
    }
}
