// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources;

namespace RageLib.Numerics
{
    public struct BVHTreeInfo : IResourceStruct<BVHTreeInfo>
    {
        public short MinX;
        public short MinY;
        public short MinZ;
        public short MaxX;
        public short MaxY;
        public short MaxZ;
        public ushort FirstNodeIndex;
        public ushort LastNodeIndex;

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
                FirstNodeIndex = EndiannessExtensions.ReverseEndianness(FirstNodeIndex),
                LastNodeIndex = EndiannessExtensions.ReverseEndianness(LastNodeIndex),
            };
        }
    }
}
