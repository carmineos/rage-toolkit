// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    // occurrences: 13969
    public struct Portal : IResourceStruct<Portal>
    {
        // structure data
        public uint Unknown_0h;
        public uint Unknown_4h;
        public uint Unknown_8h;
        public uint Unknown_Ch;
        public ushort Unknown_10h;
        public ushort Unknown_12h;
        public ushort Unknown_14h;
        public ushort Unknown_16h;
        public uint Unknown_18h;

        public Portal ReverseEndianness()
        {
            return new Portal()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
                Unknown_12h = EndiannessExtensions.ReverseEndianness(Unknown_12h),
                Unknown_14h = EndiannessExtensions.ReverseEndianness(Unknown_14h),
                Unknown_16h = EndiannessExtensions.ReverseEndianness(Unknown_16h),
                Unknown_18h = EndiannessExtensions.ReverseEndianness(Unknown_18h),
            };
        }
    }
}
