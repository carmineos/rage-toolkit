// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.WaypointRecords
{
    public struct WaypointRecordEntry : IResourceStruct<WaypointRecordEntry>
    {
        // structure data
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        private ushort Unknown_Ch;
        private ushort Unknown_Eh;
        private ushort Unknown_10h;
        private ushort Unknown_12h;

        public WaypointRecordEntry ReverseEndianness()
        {
            return new WaypointRecordEntry()
            {
                PositionX = EndiannessExtensions.ReverseEndianness(PositionX),
                PositionY = EndiannessExtensions.ReverseEndianness(PositionY),
                PositionZ = EndiannessExtensions.ReverseEndianness(PositionZ),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
                Unknown_Eh = EndiannessExtensions.ReverseEndianness(Unknown_Eh),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
                Unknown_12h = EndiannessExtensions.ReverseEndianness(Unknown_12h)
            };
        }
    }
}
