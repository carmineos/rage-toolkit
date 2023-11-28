// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    // TAdjPoly
    public struct AdjPoly : IResourceStruct<AdjPoly>
    {
        // structure data
        public uint Unknown_0h;
        public uint Unknown_4h;

        public AdjPoly ReverseEndianness()
        {
            return new AdjPoly()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
            };
        }
    }
}
