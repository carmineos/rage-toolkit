// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    // TNavMeshPoly
    public struct Poly : IResourceStruct<Poly>
    {
        // structure data
        private uint Unknown_0h;
        private uint Unknown_4h;
        private uint Unknown_8h; // 0x00000000
        private uint Unknown_Ch; // 0x00000000
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        private uint Unknown_18h;
        private uint Unknown_1Ch;
        private uint Unknown_20h;
        private uint Unknown_24h;
        private uint Unknown_28h;
        private uint Unknown_2Ch;

        public Poly ReverseEndianness()
        {
            return new Poly()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
                Unknown_14h = EndiannessExtensions.ReverseEndianness(Unknown_14h),
                Unknown_18h = EndiannessExtensions.ReverseEndianness(Unknown_18h),
                Unknown_1Ch = EndiannessExtensions.ReverseEndianness(Unknown_1Ch),
                Unknown_20h = EndiannessExtensions.ReverseEndianness(Unknown_20h),
                Unknown_24h = EndiannessExtensions.ReverseEndianness(Unknown_24h),
                Unknown_28h = EndiannessExtensions.ReverseEndianness(Unknown_28h),
                Unknown_2Ch = EndiannessExtensions.ReverseEndianness(Unknown_2Ch),
            };
        }
    }
}
