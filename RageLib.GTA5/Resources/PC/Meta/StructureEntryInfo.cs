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

namespace RageLib.Resources.GTA5.PC.Meta
{
    public enum StructureEntryDataType : byte
    {
        Bool = 0x01, 
        Structure = 0x05, // has structure name hash in info, OCCURS IN ARRAY
        StructurePointer = 0x07, // OCCURS IN ARRAY
        Int8 = 0x10,
        UInt8 = 0x11, // OCCURS IN ARRAY
        Int16 = 0x12,
        UInt16 = 0x13, // OCCURS IN ARRAY
        Int32 = 0x14,
        UInt32 = 0x15, // OCCURS IN ARRAY
        Float = 0x21, // OCCURS IN ARRAY
        Vector3 = 0x33, // OCCURS IN ARRAY
        Vector4 = 0x34, 
        StringLocal = 0x40, // has length in info
        StringPointer = 0x44, 
        StringHash = 0x4A, // OCCURS IN ARRAY
        ArrayLocal = 0x50, // has length in info
        Array = 0x52,
        DataBlockPointer = 0x59,
        EnumInt8 = 0x60, // has enum name hash in info
        EnumInt16 = 0x61, // has enum name hash in info
        EnumInt32 = 0x62, // has enum name hash in info
        FlagsInt8 = 0x63, // has enum name hash in info
        FlagsInt16 = 0x64, // has enum name hash in info     
        FlagsInt32 = 0x65, // has enum name hash in info (optional?)
    }

    public class StructureEntryInfo : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public int EntryNameHash { get; set; }
        public int DataOffset { get; set; }
        public StructureEntryDataType DataType { get; set; }
        public byte Unknown_9h { get; set; }
        public short ReferenceTypeIndex { get; set; }
        public int ReferenceKey { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EntryNameHash = reader.ReadInt32();
            this.DataOffset = reader.ReadInt32();
            this.DataType = (StructureEntryDataType)reader.ReadByte();
            this.Unknown_9h = reader.ReadByte();
            this.ReferenceTypeIndex = reader.ReadInt16();
            this.ReferenceKey = reader.ReadInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.EntryNameHash);
            writer.Write(this.DataOffset);
            writer.Write((byte)this.DataType);
            writer.Write(this.Unknown_9h);
            writer.Write(this.ReferenceTypeIndex);
            writer.Write(this.ReferenceKey);
        }
    }
}
