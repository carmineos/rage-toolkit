// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.VehicleRecords
{
    // this looks exactly like an rrr entry:
    // -> http://www.gtamodding.com/wiki/Carrec
    public struct VehicleRecordsEntry : IResourceStruct<VehicleRecordsEntry>
    {
        // structure data
        public uint Time;
        public ushort VelocityX;
        public ushort VelocityY;
        public ushort VelocityZ;
        public byte RightX;
        public byte RightY;
        public byte RightZ;
        public byte TopX;
        public byte TopY;
        public byte TopZ;
        public byte SteeringAngle;
        public byte GasPedalPower;
        public byte BrakePedalPower;
        public byte HandbrakeUsed;
        public float PositionX;
        public float PositionY;
        public float PositionZ;

        public VehicleRecordsEntry ReverseEndianness()
        {
            return new VehicleRecordsEntry()
            {
                Time = EndiannessExtensions.ReverseEndianness(Time),
                VelocityX = EndiannessExtensions.ReverseEndianness(VelocityX),
                VelocityY = EndiannessExtensions.ReverseEndianness(VelocityY),
                VelocityZ = EndiannessExtensions.ReverseEndianness(VelocityZ),
                RightX = EndiannessExtensions.ReverseEndianness(RightX),
                RightY = EndiannessExtensions.ReverseEndianness(RightY),
                RightZ = EndiannessExtensions.ReverseEndianness(RightZ),
                TopX = EndiannessExtensions.ReverseEndianness(TopX),
                TopY = EndiannessExtensions.ReverseEndianness(TopY),
                TopZ = EndiannessExtensions.ReverseEndianness(TopZ),
                SteeringAngle = EndiannessExtensions.ReverseEndianness(SteeringAngle),
                GasPedalPower = EndiannessExtensions.ReverseEndianness(GasPedalPower),
                HandbrakeUsed = EndiannessExtensions.ReverseEndianness(HandbrakeUsed),
                PositionX = EndiannessExtensions.ReverseEndianness(PositionX),
                PositionY = EndiannessExtensions.ReverseEndianness(PositionY),
                PositionZ = EndiannessExtensions.ReverseEndianness(PositionZ),
            };
        }
    }
}
