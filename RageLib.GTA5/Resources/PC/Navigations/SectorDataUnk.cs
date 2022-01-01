// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    public struct SectorDataUnk : IResourceStruct<SectorDataUnk>
    {
        // structure data
        public ushort Unknown_0h;
        public ushort Unknown_2h;
        public ushort Unknown_4h;
        public ushort Unknown_6h;

        public SectorDataUnk ReverseEndianness()
        {
            return new SectorDataUnk()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_2h = EndiannessExtensions.ReverseEndianness(Unknown_2h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_6h = EndiannessExtensions.ReverseEndianness(Unknown_6h),
            };
        }
    }
}
