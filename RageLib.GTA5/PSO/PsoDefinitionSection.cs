/*
    Copyright(c) 2016 Neodymium

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
using System;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.PSO
{
    public class PsoDefinitionSection
    {
        public int Ident { get; private set; } = 0x50534348;
        public uint Count;

        public List<PsoElementIndexInfo> EntriesIdx;
        public List<PsoElementInfo> Entries;

        public void Read(DataReader reader)
        {
            Ident = reader.ReadInt32();
            var Length = reader.ReadInt32();
            this.Count = reader.ReadUInt32();

            this.EntriesIdx = new List<PsoElementIndexInfo>();
            for (int i = 0; i < Count; i++)
            {
                var entry = new PsoElementIndexInfo();
                entry.Read(reader);
                EntriesIdx.Add(entry);
            }

            this.Entries = new List<PsoElementInfo>();
            for (int i = 0; i < Count; i++)
            {
                reader.Position = EntriesIdx[i].Offset;
                var type = reader.ReadByte();

                reader.Position = EntriesIdx[i].Offset;
                if (type == 0)
                {
                    var entry = new PsoStructureInfo();
                    entry.Read(reader);
                    Entries.Add(entry);
                }
                else if (type == 1)
                {
                    var entry = new PsoEnumInfo();
                    entry.Read(reader);
                    Entries.Add(entry);
                }
                else
                    throw new Exception("unknown type!");
            }
        }

        public void Write(DataWriter writer)
        {

            var entriesStream = new MemoryStream();
            var entriesWriter = new DataWriter(entriesStream, Endianess.BigEndian);
            for (int i = 0; i < Entries.Count; i++)
            {
                EntriesIdx[i].Offset = 12 + 8 * Entries.Count + (int)entriesWriter.Position;
                Entries[i].Write(entriesWriter);
            }



            var indexStream = new MemoryStream();
            var indexWriter = new DataWriter(indexStream, Endianess.BigEndian);
            foreach (var entry in EntriesIdx)
                entry.Write(indexWriter);




            writer.Write(Ident);
            writer.Write((int)(12 + entriesStream.Length + indexStream.Length));
            writer.Write((int)(Entries.Count));

            // write entries index data
            var buf1 = new byte[indexStream.Length];
            indexStream.Position = 0;
            indexStream.Read(buf1, 0, buf1.Length);
            writer.Write(buf1);

            // write entries data
            var buf2 = new byte[entriesStream.Length];
            entriesStream.Position = 0;
            entriesStream.Read(buf2, 0, buf2.Length);
            writer.Write(buf2);


        }
    }

    public class PsoElementIndexInfo
    {
        public int NameHash;
        public int Offset;

        public void Read(DataReader reader)
        {
            this.NameHash = reader.ReadInt32();
            this.Offset = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(NameHash);
            writer.Write(Offset);
        }
    }

    public abstract class PsoElementInfo
    {
        public abstract void Read(DataReader reader);

        public abstract void Write(DataWriter writer);
    }

    public class PsoStructureInfo : PsoElementInfo
    {
        public byte Type { get; set; } = 0;
        public short EntriesCount { get; private set; }
        public byte Unk { get; set; }
        public int StructureLength { get; set; }
        public uint Unk_Ch { get; set; } = 0x00000000;
        public List<PsoStructureEntryInfo> Entries { get; set; } = new List<PsoStructureEntryInfo>();

        public override void Read(DataReader reader)
        {
            uint x = reader.ReadUInt32();
            this.Type = (byte)((x & 0xFF000000) >> 24);
            this.EntriesCount = (short)(x & 0xFFFF);
            this.Unk = (byte)((x & 0x00FF0000) >> 16);
            this.StructureLength = reader.ReadInt32();
            this.Unk_Ch = reader.ReadUInt32();

            Entries = new List<PsoStructureEntryInfo>();
            for (int i = 0; i < EntriesCount; i++)
            {
                var entry = new PsoStructureEntryInfo();
                entry.Read(reader);
                Entries.Add(entry);
            }
        }

        public override void Write(DataWriter writer)
        {
            Type = 0;
            EntriesCount = (short)Entries.Count;

            uint typeAndEntriesCount = (uint)(Type << 24) | (uint)(Unk << 16) | (ushort)EntriesCount;
            writer.Write(typeAndEntriesCount);
            writer.Write(StructureLength);
            writer.Write(Unk_Ch);

            foreach (var entry in Entries)
            {
                entry.Write(writer);
            }
        }
    }
    
    public enum ParMemberType : byte    // 0x1CA39C3D
    {
        BOOL = 0,
        CHAR = 1,
        UCHAR = 2,
        SHORT = 3,
        USHORT = 4,
        INT = 5,
        UINT = 6,
        FLOAT = 7,
        VECTOR2 = 8,
        VECTOR3 = 9,
        VECTOR4 = 10,
        STRING = 11,
        STRUCT = 12,
        ARRAY = 13,
        ENUM = 14,
        BITSET = 15,
        MAP = 16,
        MATRIX34 = 17,
        MATRIX44 = 18,
        VEC2V = 19,
        VEC3V = 20,
        VEC4V = 21,
        MAT33V = 22,
        MAT34V = 23,
        MAT44V = 24,
        SCALARV = 25,
        BOOLV = 26,
        VECBOOLV = 27,
        PTRDIFFT = 28,
        SIZET = 29,
        FLOAT16 = 30,
        INT64 = 31,
        UINT64 = 32,
        DOUBLE = 33
    }

    public enum ParMemberArraySubtype : byte    // 0xADE25B1B
    {
        ATARRAY = 0,                        // 0xABE40192
        ATFIXEDARRAY = 1,                   // 0x3A523E81
        ATRANGEARRAY = 2,                   // 0x18A25B6B
        POINTER = 3,                        // 0x47073D6E
        MEMBER = 4,                         // 0x6CC11BB4
        _0x2087BB00 = 5,                    // 0x2087BB00
        POINTER_WITH_COUNT = 6,             // 0xE2980EB5
        POINTER_WITH_COUNT_8BIT_IDX = 7,    // 0x254D33B1
        POINTER_WITH_COUNT_16BIT_IDX = 8,   // 0xB66B6752
        VIRTUAL = 9,                        // 0xAC01A1DC
    };

    public enum ParMemberEnumSubtype : byte // 0x2721C60A
    {
        _32BIT = 0,         // 0xAF085554
        _16BIT = 1,         // 0x0D502D8E
        _8BIT = 2,          // 0xF2AAF53D
    };

    public enum ParMemberBitsetSubtype : byte
    {
        _32BIT = 0,         // 0xAF085554
        _16BIT = 1,         // 0x0D502D8E
        _8BIT = 2,          // 0xF2AAF53D
        ATBITSET = 3,       // 0xB46B5F65
    };

    public enum ParMemberMapSubtype : byte  // 0x9C9F1983
    {
        ATMAP = 0,          // 0xD8C10171
        ATBINARYMAP = 1,    // 0x6560BA79
    };

    public enum ParMemberStringSubtype : byte   // 0xA5CF41A9
    {
        MEMBER = 0,                 // 0x6CC11BB4
        POINTER = 1,                // 0x47073D6E
        CONST_STRING = 2,           // 0x757C1B9B
        ATSTRING = 3,               // 0x5CDCA61E
        WIDE_MEMBER = 4,            // 0xAC508104
        WIDE_POINTER = 5,           // 0x99D4A8CD
        ATWIDESTRING = 6,           // 0x3DED5509
        ATNONFINALHASHSTRING = 7,   // 0xDFE6E4AF
        ATFINALHASHSTRING = 8,      // 0x945E5945
        ATHASHVALUE = 9,            // 0xBD3CD157
        ATPARTIALHASHVALUE = 10,    // 0xD552B3C8
        ATNSHASHSTRING = 11,        // 0x893F9F69
        ATNSHASHVALUE = 12,         // 0x3767C917
    };

    public enum ParMemberStructSubtype : byte   // 0x76214E40
    {
        STRUCTURE = 0,                  // 0x3AC3050F
        EXTERNAL_NAMED = 1,             // 0xA53F8BA9
        EXTERNAL_NAMED_USERNULL = 2,    // 0x2DED4C19
        POINTER = 3,                    // 0x47073D6E
        SIMPLE_POINTER = 4,             // 0x67466543
    };

    public class PsoStructureEntryInfo
    {
        public int EntryNameHash;
        public ParMemberType Type;
        public byte SubType;
        public short DataOffset;
        public int ReferenceKey; // when array -> entry index with type

        public PsoStructureEntryInfo()
        { }

        public PsoStructureEntryInfo(int nameHash, ParMemberType type, byte subType, short dataOffset, int referenceKey)
        {
            this.EntryNameHash = nameHash;
            this.Type = type;
            this.SubType = subType;
            this.DataOffset = dataOffset;
            this.ReferenceKey = referenceKey;
        }

        public void Read(DataReader reader)
        {
            this.EntryNameHash = reader.ReadInt32();
            this.Type = (ParMemberType)reader.ReadByte();
            this.SubType = reader.ReadByte();
            this.DataOffset = reader.ReadInt16();
            this.ReferenceKey = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(EntryNameHash);
            writer.Write((byte)Type);
            writer.Write(SubType);
            writer.Write(DataOffset);
            writer.Write(ReferenceKey);
        }
    }

    public class PsoEnumInfo : PsoElementInfo
    {
        public byte Type { get; private set; } = 1;
        public int EntriesCount { get; private set; }
        public List<PsoEnumEntryInfo> Entries { get; set; }

        public override void Read(DataReader reader)
        {
            uint x = reader.ReadUInt32();
            this.Type = (byte)((x & 0xFF000000) >> 24);
            this.EntriesCount = (int)(x & 0x00FFFFFF);

            Entries = new List<PsoEnumEntryInfo>();
            for (int i = 0; i < EntriesCount; i++)
            {
                var entry = new PsoEnumEntryInfo();
                entry.Read(reader);
                Entries.Add(entry);
            }
        }

        public override void Write(DataWriter writer)
        {
            // update...
            Type = 1;
            EntriesCount = Entries.Count;

            uint typeAndEntriesCount = (uint)(Type << 24) | (uint)EntriesCount;
            writer.Write(typeAndEntriesCount);

            foreach (var entry in Entries)
            {
                entry.Write(writer);
            }
        }
    }

    public class PsoEnumEntryInfo
    {
        public int EntryNameHash { get; set; }
        public int EntryKey { get; set; }

        public PsoEnumEntryInfo()
        { }

        public PsoEnumEntryInfo(int nameHash, int key)
        {
            this.EntryNameHash = nameHash;
            this.EntryKey = key;
        }

        public void Read(DataReader reader)
        {
            this.EntryNameHash = reader.ReadInt32();
            this.EntryKey = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(EntryNameHash);
            writer.Write(EntryKey);
        }
    }
}
