// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Nodes
{
    public struct Unknown_ND_002 : IResourceStruct<Unknown_ND_002>
    {
        // structure data
        private uint Unknown_0h;
        private uint Unknown_4h;

        public Unknown_ND_002 ReverseEndianness()
        {
            return new Unknown_ND_002()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
            };
        }
    }
}
