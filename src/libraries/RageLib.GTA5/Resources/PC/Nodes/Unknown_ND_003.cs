// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Nodes
{
    public struct Unknown_ND_003 : IResourceStruct<Unknown_ND_003>
    {
        // structure data
        public uint Unknown_0h;
        public uint Unknown_4h;
        public uint Unknown_8h;

        public Unknown_ND_003 ReverseEndianness()
        {
            return new Unknown_ND_003()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
            };
        }
    }
}
