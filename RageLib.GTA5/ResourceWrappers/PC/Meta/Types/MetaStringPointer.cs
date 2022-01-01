// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System;
using System.Diagnostics;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaStringPointer : IMetaValue
    {
        public int DataBlockIndex { get; set; }
        public int DataOffset { get; set; }
        public int StringLength { get; set; }
        public int StringCapacity { get; set; }

        // Reference values
        public string Value { get; set; }

        public MetaStringPointer()
        { }

        public MetaStringPointer(string value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            this.DataBlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);
            this.DataOffset = (int)((blockIndexAndOffset & 0xFFFFF000) >> 12);
            
            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            var count1 = reader.ReadUInt16();
            var count2 = reader.ReadUInt16();
            
            // one is the length with null terminator, but they are often inverted
            var length = Math.Min(count1, count2);
            var length_null = Math.Max(count1, count2);

            // check they are either equal or differ of 1
            Debug.Assert(length_null - length <= 1);

            var unknown_Ch = reader.ReadUInt32();
            Debug.Assert(unknown_Ch == 0);

            this.StringLength = count1;
            this.StringCapacity = count2;
        }

        public void Write(DataWriter writer)
        {
            uint blockIndexAndOffset = (uint)DataBlockIndex | ((uint)DataOffset << 12);
            writer.Write(blockIndexAndOffset);
            writer.Write((uint)0);
            writer.Write((ushort)StringLength);
            writer.Write((ushort)(StringCapacity));
            writer.Write((uint)0);
        }
    }
}
