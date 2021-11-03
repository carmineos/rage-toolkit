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

namespace RageLib.Resources.GTA5.PC.WaypointRecords
{
    public struct WaypointRecordEntry : IResourceStruct<WaypointRecordEntry>
    {
        // structure data
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public ushort Unknown_Ch;
        public ushort Unknown_Eh;
        public ushort Unknown_10h;
        public ushort Unknown_12h;

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
