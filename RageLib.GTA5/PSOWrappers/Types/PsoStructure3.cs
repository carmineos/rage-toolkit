// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;
using System;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoStructure3 : IPsoValue
    {
        public readonly PsoFile pso;
        public readonly PsoStructureInfo structureInfo;
        public readonly PsoStructureEntryInfo entryInfo;
        public PsoStructure Value { get; set; }

        public PsoStructure3(PsoFile pso, PsoStructureInfo structureInfo, PsoStructureEntryInfo entryInfo)
        {
            this.pso = pso;
            this.structureInfo = structureInfo;
            this.entryInfo = entryInfo;
        }

        public void Read(PsoDataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            var Offset = (int)(blockIndexAndOffset >> 12) & 0x000FFFFF;
            var BlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);

            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            if (BlockIndex > 0)
            {
                var nameHash = pso.DataMappingSection.Entries[BlockIndex - 1].NameHash;
                var infos = PsoTypeBuilder.GetElementInfoAndElementIndexInfo(pso, nameHash);
                var strInfo = (PsoStructureInfo)infos.Item1;
                var sectionIdxInfo = infos.Item2;

                // read reference data...
                var backupOfSection = reader.CurrentSectionIndex;
                var backupOfPosition = reader.Position;

                reader.CurrentSectionIndex = BlockIndex - 1;
                reader.Position = Offset;

                Value = new PsoStructure(pso, strInfo, sectionIdxInfo, null);
                Value.Read(reader);

                reader.CurrentSectionIndex = backupOfSection;
                reader.Position = backupOfPosition;
            }
            else
            {
                Value = null;
            }

        }

        public void Write(DataWriter writer)
        {
        }
    }
}
