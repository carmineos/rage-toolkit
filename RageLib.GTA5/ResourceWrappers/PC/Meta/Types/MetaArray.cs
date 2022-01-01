// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaArray : IMetaValue
    {
        public StructureEntryInfo info;
        public bool IsAlwaysAtZeroOffset = false;

        public int BlockIndex { get; set; }
        public int Offset { get; set; }
        public int NumberOfEntries { get; set; }

        // Reference values
        public List<IMetaValue> Entries { get; set; }

        public void Read(DataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            this.BlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);
            this.Offset = (int)((blockIndexAndOffset & 0xFFFFF000) >> 12);
            
            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            var size1 = reader.ReadUInt16();
            var size2 = reader.ReadUInt16();
            Debug.Assert(size1 == size2);
            this.NumberOfEntries = size1;

            var unknown_Ch = reader.ReadUInt32();
            Debug.Assert(unknown_Ch == 0);
        }

        public void Write(DataWriter writer)
        {
            uint blockIndexAndOffset = (uint)BlockIndex | ((uint)Offset << 12);
            writer.Write(blockIndexAndOffset);
            writer.Write((uint)0);
            writer.Write((ushort)NumberOfEntries);
            writer.Write((ushort)NumberOfEntries);
            writer.Write((uint)0);
        }
    }
}
