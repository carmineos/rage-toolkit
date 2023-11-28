// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;
using System.Collections.Generic;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoArray129 : IPsoValue
    {
        public readonly PsoFile pso;
        public readonly PsoStructureInfo structureInfo;
        public readonly PsoStructureEntryInfo entryInfo;
        public readonly int numberOfEntries;
        public List<IPsoValue> Entries { get; set; }

        public PsoArray129(PsoFile pso, PsoStructureInfo structureInfo, PsoStructureEntryInfo entryInfo, int numberOfEntries)
        {
            this.pso = pso;
            this.structureInfo = structureInfo;
            this.entryInfo = entryInfo;
            this.numberOfEntries = numberOfEntries;
        }

        public void Read(PsoDataReader reader)
        {
            Entries = new List<IPsoValue>();
            for (int i = 0; i < numberOfEntries; i++)
            {
                var entry = PsoTypeBuilder.Make(pso, structureInfo, entryInfo);
                entry.Read(reader);
                Entries.Add(entry);
            }
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
