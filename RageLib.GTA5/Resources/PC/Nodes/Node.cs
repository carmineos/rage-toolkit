// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Nodes
{
    public struct Node : IResourceStruct<Node>
    {
        // structure data
        public uint Unknown_0h; // 0x00000000
        public uint Unknown_4h; // 0x00000000
        public uint Unknown_8h; // 0x00000000
        public uint Unknown_Ch; // 0x00000000
        public ushort Unknown_10h;
        public ushort Unknown_12h;
        public uint Unknown_14h;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public uint Unknown_20h;
        public uint Unknown_24h;

        public Node ReverseEndianness()
        {
            return new Node()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
                Unknown_12h = EndiannessExtensions.ReverseEndianness(Unknown_12h),
                Unknown_14h = EndiannessExtensions.ReverseEndianness(Unknown_14h),
                Unknown_18h = EndiannessExtensions.ReverseEndianness(Unknown_18h),
                Unknown_1Ch = EndiannessExtensions.ReverseEndianness(Unknown_1Ch),
                Unknown_20h = EndiannessExtensions.ReverseEndianness(Unknown_20h),
                Unknown_24h = EndiannessExtensions.ReverseEndianness(Unknown_24h),
            };
        }
    }
}
