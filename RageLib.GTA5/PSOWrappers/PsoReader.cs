// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;
using RageLib.GTA5.PSOWrappers.Types;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.PSOWrappers
{
    public class PsoReader
    {
        public IPsoValue Read(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return Read(fileStream);
            }
        }

        public IPsoValue Read(Stream fileStream)
        {
            var resource = new PsoFile();
            resource.Load(fileStream);
            return Parse(resource);
        }

        public IPsoValue Parse(PsoFile meta)
        {
            var blockKeys = new List<int>();
            var blocks = new List<List<IPsoValue>>();

            var t1 = (PsoStructureInfo)null;
            var t2 = (PsoElementIndexInfo)null;
            var rootHash = meta.DataMappingSection.Entries[meta.DataMappingSection.RootIndex - 1].NameHash;
            for (int i = 0; i < meta.DefinitionSection.Count; i++)
            {
                if (meta.DefinitionSection.EntriesIdx[i].NameHash == rootHash)
                {
                    t1 = (PsoStructureInfo)meta.DefinitionSection.Entries[i];
                    t2 = meta.DefinitionSection.EntriesIdx[i];
                }
            }

            var resultStructure = new PsoStructure(meta, t1, t2, null);

            var reader = new PsoDataReader(meta);
            reader.CurrentSectionIndex = meta.DataMappingSection.RootIndex - 1;
            reader.Position = 0;
            resultStructure.Read(reader);
            return resultStructure;
        }
    }
}
