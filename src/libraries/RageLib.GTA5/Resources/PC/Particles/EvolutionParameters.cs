// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;

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
            this.EvolutionNames = reader.ReadBlock<ResourceSimpleList64<EvolutionName>>();
            this.Unknown_10h = reader.ReadBlock<ResourceSimpleList64<Unknown_P_003>>();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadBlock<ResourceSimpleList64<Unknown_P_007>>();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(this.EvolutionNames);
            writer.WriteBlock(this.Unknown_10h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.WriteBlock(this.Unknown_28h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0, EvolutionNames),
                new Tuple<long, IResourceBlock>(0x10, Unknown_10h),
                new Tuple<long, IResourceBlock>(0x28, Unknown_28h)
            };
        }
    }
}
