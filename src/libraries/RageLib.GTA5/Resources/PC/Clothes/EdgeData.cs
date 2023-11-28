// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // rage__phEdgeData
    public struct EdgeData : IResourceStruct<EdgeData>
    {
        // structure data
        public ushort vertIndices0;
        public ushort vertIndices1;
        public float EdgeLength2;
        public float Weight0;
        public float CompressionWeight;

        public EdgeData ReverseEndianness()
        {
            return new EdgeData()
            {
                vertIndices0 = EndiannessExtensions.ReverseEndianness(vertIndices0),
                vertIndices1 = EndiannessExtensions.ReverseEndianness(vertIndices1),
                EdgeLength2 = EndiannessExtensions.ReverseEndianness(EdgeLength2),
                Weight0 = EndiannessExtensions.ReverseEndianness(Weight0),
                CompressionWeight = EndiannessExtensions.ReverseEndianness(CompressionWeight),
            };
        }
    }
}
