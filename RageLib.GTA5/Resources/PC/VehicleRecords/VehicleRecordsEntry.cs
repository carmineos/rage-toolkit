/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

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
