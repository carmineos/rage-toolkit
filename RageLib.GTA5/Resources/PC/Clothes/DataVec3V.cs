// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // rage__dataVec3V
    public struct DataVec3V : IResourceStruct<DataVec3V>
    {
        // structure data
        private uint Unknown_0h;
        private uint Unknown_4h;
        private uint Unknown_8h;
        private uint Unknown_Ch;

        public DataVec3V ReverseEndianness()
        {
            return new DataVec3V()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
            };
        }
    }
}
