// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    public struct BVHTreeInfo : IResourceStruct<BVHTreeInfo>
    {
        public short MinX;
        public short MinY;
        public short MinZ;
        public short MaxX;
        public short MaxY;
        public short MaxZ;
        public short NodeIndex1;
        public short NodeIndex2;

        public BVHTreeInfo ReverseEndianness()
        {
            return new BVHTreeInfo()
            {
                MinX = EndiannessExtensions.ReverseEndianness(MinX),
                MinY = EndiannessExtensions.ReverseEndianness(MinY),
                MinZ = EndiannessExtensions.ReverseEndianness(MinZ),
                MaxX = EndiannessExtensions.ReverseEndianness(MaxX),
                MaxY = EndiannessExtensions.ReverseEndianness(MaxY),
                MaxZ = EndiannessExtensions.ReverseEndianness(MaxZ),
                NodeIndex1 = EndiannessExtensions.ReverseEndianness(NodeIndex1),
                NodeIndex2 = EndiannessExtensions.ReverseEndianness(NodeIndex2),
            };
        }
    }
}
