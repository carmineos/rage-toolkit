// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoMap : IPsoValue
    {
        public readonly PsoFile pso;
        public readonly PsoStructureInfo structureInfo;
        public readonly PsoStructureEntryInfo keyEntryInfo;
        public readonly PsoStructureEntryInfo valueEntryInfo;
        public List<PsoStructure> Entries { get; set; }

        public PsoMap(
            PsoFile pso,
            PsoStructureInfo structureInfo,
          PsoStructureEntryInfo keyEntryInfo,
          PsoStructureEntryInfo valueEntryInfo)
        {
            this.pso = pso;
            this.structureInfo = structureInfo;
            this.keyEntryInfo = keyEntryInfo;
            this.valueEntryInfo = valueEntryInfo;
        }

        public void Read(PsoDataReader reader)
        {
            var unknown_0h = reader.ReadUInt32();
            Debug.Assert(unknown_0h == 0x01000000);

            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            var blockIndexAndOffset = reader.ReadUInt32();
            var Offset = (int)((blockIndexAndOffset >> 12) & 0x000FFFFF);
            var BlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);

            var unknown_Ch = reader.ReadUInt32();
            Debug.Assert(unknown_Ch == 0);

            var keysCount = reader.ReadUInt16();
            var itemsCount = reader.ReadUInt16();
            Debug.Assert(keysCount == itemsCount);

            var unknown_14h = reader.ReadUInt32();
            Debug.Assert(unknown_14h == 0);

            // read reference data...
            var backupOfSection = reader.CurrentSectionIndex;
            var backupOfPosition = reader.Position;

            reader.CurrentSectionIndex = BlockIndex - 1;
            reader.Position = Offset;

            int nameOfDataSection = pso.DataMappingSection.Entries[BlockIndex - 1].NameHash;
            var sectionInfo = PsoTypeBuilder.GetStructureInfo(pso, nameOfDataSection);

            Entries = new List<PsoStructure>();
            for (int i = 0; i < keysCount; i++)
            {
                var entryStr = new PsoStructure(pso, sectionInfo, null, null);
                entryStr.Read(reader);
                Entries.Add(entryStr);
            }

            reader.CurrentSectionIndex = backupOfSection;
            reader.Position = backupOfPosition;
        }

        public void Write(DataWriter writer)
        {
        }
    }
}
