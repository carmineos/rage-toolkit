// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.Common.Simple;
using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public class Unknown_P_013 : ResourceSystemBlock
    {
        public override long BlockLength => 0x58;

        // structure data
        public string32_r Unknown_0h;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Unknown_38h; // 0x0000000000000000
        public SimpleList64<uint> Unknown_40h;
        public uint Unknown_50h;
        public uint Unknown_54h; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadBlock<string32_r>();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadBlock<SimpleList64<uint>>();
            this.Unknown_50h = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(this.Unknown_0h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
            writer.WriteBlock(this.Unknown_40h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x0, Unknown_0h),
                new Tuple<long, IResourceBlock>(0x40, Unknown_40h)
            };
        }
    }
}
