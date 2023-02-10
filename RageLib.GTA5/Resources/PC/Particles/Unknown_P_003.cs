// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public class Unknown_P_003 : ResourceSystemBlock
    {
        public override long BlockLength => 24;

        // structure data
        private ResourceSimpleList64<Unknown_P_006> Unknown_0h;
        public uint Hash;
        private uint Unknown_14h;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadBlock<ResourceSimpleList64<Unknown_P_006>>();
            this.Hash = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(this.Unknown_0h);
            writer.Write(this.Hash);
            writer.Write(this.Unknown_14h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0, Unknown_0h)
            };
        }
    }
}
