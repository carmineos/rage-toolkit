// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // datBase
    // ptxKeyframeProp
    public class KeyframeProp : DatBase64
    {
        public override long BlockLength => 0x90;

        // structure data
        private ulong Unknown_8h; // 0x0000000000000000
        private ulong Unknown_10h; // 0x0000000000000000
        private ulong Unknown_18h; // 0x0000000000000000
        private ulong Unknown_20h; // 0x0000000000000000
        private ulong Unknown_28h; // 0x0000000000000000
        private ulong Unknown_30h; // 0x0000000000000000
        private ulong Unknown_38h; // 0x0000000000000000
        private ulong Unknown_40h; // 0x0000000000000000
        private ulong Unknown_48h; // 0x0000000000000000
        private ulong Unknown_50h; // 0x0000000000000000
        private ulong Unknown_58h; // 0x0000000000000000
        private ulong Unknown_60h; // 0x0000000000000000
        private uint Unknown_68h;
        private uint Unknown_6Ch;
        private SimpleList64<Unknown_P_009> Unknown_70h;
        private ulong Unknown_80h; // 0x0000000000000000
        private ulong Unknown_88h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_8h = reader.ReadUInt64();
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.Unknown_58h = reader.ReadUInt64();
            this.Unknown_60h = reader.ReadUInt64();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();
            this.Unknown_70h = reader.ReadBlock<SimpleList64<Unknown_P_009>>();
            this.Unknown_80h = reader.ReadUInt64();
            this.Unknown_88h = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
            writer.WriteBlock(this.Unknown_70h);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_88h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x70, Unknown_70h)
            };
        }
    }
}
