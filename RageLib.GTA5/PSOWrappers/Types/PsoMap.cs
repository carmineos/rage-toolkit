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
            var sectionInfo = (PsoStructureInfo)null;
            //var sectionIdxInfo = (PsoElementIndexInfo)null;
            for (int k = 0; k < pso.DefinitionSection.EntriesIdx.Count; k++)
            {
                if (pso.DefinitionSection.EntriesIdx[k].NameHash == nameOfDataSection)
                {
                    sectionInfo = (PsoStructureInfo)pso.DefinitionSection.Entries[k];
                    //sectionIdxInfo = pso.DefinitionSection.EntriesIdx[k];
                }
            }

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
