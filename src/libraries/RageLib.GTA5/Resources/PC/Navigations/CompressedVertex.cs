// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    // CNavMeshCompressedVertex
    public struct CompressedVertex : IResourceStruct<CompressedVertex>
    {
        // structure data
        public short X;
        public short Y;
        public short Z;

        public CompressedVertex ReverseEndianness()
        {
            return new CompressedVertex()
            {
                X = EndiannessExtensions.ReverseEndianness(X),
                Y = EndiannessExtensions.ReverseEndianness(Y),
                Z = EndiannessExtensions.ReverseEndianness(Z),
            };
        }
    }
}
