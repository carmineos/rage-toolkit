// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoArray0 : IPsoValue
    {
        public readonly PsoFile pso;
        public readonly PsoStructureInfo structureInfo;
        public readonly PsoStructureEntryInfo entryInfo;
        public List<IPsoValue> Entries { get; set; }

        public PsoArray0(PsoFile pso, PsoStructureInfo structureInfo, PsoStructureEntryInfo entryInfo)
        {
            this.pso = pso;
            this.structureInfo = structureInfo;
            this.entryInfo = entryInfo;
        }

        public void Read(PsoDataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            var BlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);
            var Offset = (int)((blockIndexAndOffset & 0xFFFFF000) >> 12);
            
            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            var count = reader.ReadUInt16();
            var capacity = reader.ReadUInt16();
            Debug.Assert(count <= capacity);

            var NumberOfEntries = count;
            var unknown_Ch = reader.ReadUInt32();
            Debug.Assert(unknown_Ch == 0);

            if (BlockIndex > 0)
            {
                // read reference data...
                var backupOfSection = reader.CurrentSectionIndex;
                var backupOfPosition = reader.Position;

                reader.CurrentSectionIndex = BlockIndex - 1;
                reader.Position = Offset;

                Entries = new List<IPsoValue>();
                for (int i = 0; i < NumberOfEntries; i++)
                {
                    var entry = PsoTypeBuilder.Make(pso, structureInfo, entryInfo);
                    entry.Read(reader);
                    Entries.Add(entry);
                }

                reader.CurrentSectionIndex = backupOfSection;
                reader.Position = backupOfPosition;
            }
            else
            {
                Entries = null;
            }
        }

        public void Write(DataWriter writer)
        {
            //uint blockIndexAndOffset = (uint)BlockIndex | ((uint)Offset << 12);
            //writer.Write(blockIndexAndOffset);
            //writer.Write((uint)0);
            //writer.Write((ushort)NumberOfEntries);
            //writer.Write((ushort)NumberOfEntries);
            //writer.Write((uint)0);
        }
    }
}
