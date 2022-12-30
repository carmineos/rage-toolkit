// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Clips;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public class EvolutionParameters : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        public ResourceSimpleList64<EvolutionName> EvolutionNames;
        public ResourceSimpleList64<Unknown_P_003> Unknown_10h;
        public uint Unknown_20h; // 0x00000001
        public uint Unknown_24h; // 0x00000000
        public ResourceSimpleList64<Unknown_P_007> Unknown_28h;
        public uint Unknown_38h; // 0x00000000
        public uint Unknown_3Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EvolutionNames = reader.ReadList<EvolutionName>();
            this.Unknown_10h = reader.ReadList<Unknown_P_003>();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadList<Unknown_P_007>();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteList(this.EvolutionNames);
            writer.WriteList(this.Unknown_10h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.WriteList(this.Unknown_28h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (EvolutionNames.Entries != null) list.Add(EvolutionNames.Entries);
            if (Unknown_10h.Entries != null) list.Add(Unknown_10h.Entries);
            if (Unknown_28h.Entries != null) list.Add(Unknown_28h.Entries);
            return list.ToArray();
        }
    }
}
