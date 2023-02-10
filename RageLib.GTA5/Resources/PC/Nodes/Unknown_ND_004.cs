// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Nodes
{
    public struct Unknown_ND_004 : IResourceStruct<Unknown_ND_004>
    {
        // structure data
        private ushort Unknown_0h;
        private ushort Unknown_2h;
        private uint Unknown_4h;

        public Unknown_ND_004 ReverseEndianness()
        {
            return new Unknown_ND_004()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_2h = EndiannessExtensions.ReverseEndianness(Unknown_2h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
            };
        }
    }
}
