// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;
using System.Collections.Generic;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoStructure : IPsoValue
    {
        public readonly PsoFile pso;
        public readonly PsoStructureInfo structureInfo;
        public readonly PsoElementIndexInfo entryIndexInfo;
        public readonly PsoStructureEntryInfo entryInfo;
        public Dictionary<int, IPsoValue> Values { get; set; }

        public PsoStructure(PsoFile pso, PsoStructureInfo structureInfo, PsoElementIndexInfo entryIndexInfo, PsoStructureEntryInfo entryInfo)
        {
            this.pso = pso;
            this.structureInfo = structureInfo;
            this.entryIndexInfo = entryIndexInfo;
            this.entryInfo = entryInfo;
            this.Values = new Dictionary<int, IPsoValue>();
        }
        
        public void Read(PsoDataReader reader)
        {
            long backupOfPosition = reader.Position;

            this.Values = new Dictionary<int, IPsoValue>();
            for (int i = 0; i < structureInfo.Entries.Count; i++)
            {
                // skip unnamed entries...
                var x1 = structureInfo.Entries[i];
                if (x1.EntryNameHash == 0x100)
                    continue;


                reader.Position = backupOfPosition + x1.DataOffset;
                var value = PsoTypeBuilder.Make(pso, structureInfo, x1);
                value.Read(reader);
                Values.Add(x1.EntryNameHash, value);
            }

            reader.Position = backupOfPosition + structureInfo.StructureLength;

        }

        public void Write(DataWriter writer)
        {
            //long position = writer.Position;

            //writer.Write(new byte[psoSection.StructureLength]);
            //writer.Position = position;

            //foreach (var entry in psoSection.Entries)
            //{
            //    if (entry.EntryNameHash != 0x100)
            //    {
            //        writer.Position = position + entry.DataOffset;
            //        this.Values[entry.EntryNameHash].Write(writer);
            //    }
            //}
            //writer.Position = position + psoSection.StructureLength;
        }
    }
}
