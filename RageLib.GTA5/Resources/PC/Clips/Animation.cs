// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // pgBase
    // crAnimation
    public class Animation : PgBase64
    {
        public override long BlockLength => 0x60;

        // structure data
        private ushort Unknown_10h;
        private ushort Unknown_12h;
        private ushort Unknown_14h;
        private ushort Unknown_16h;
        private float Unknown_18h;
        private uint Unknown_1Ch; // Signature?
        private uint Unknown_20h; // 0x00000000
        private uint Unknown_24h; // 0x00000000
        private uint Unknown_28h; // 0x00000000
        private uint Unknown_2Ch; // 0x00000000
        private uint Unknown_30h; // 0x00000000
        private uint Unknown_34h; // 0x00000000
        private uint Unknown_38h;
        private uint Unknown_3Ch;
        public ResourcePointerList64<Sequence> Sequences;
        public SimpleList64<AnimTrack> Tracks;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt16();
            this.Unknown_12h = reader.ReadUInt16();
            this.Unknown_14h = reader.ReadUInt16();
            this.Unknown_16h = reader.ReadUInt16();
            this.Unknown_18h = reader.ReadSingle();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.Sequences = reader.ReadBlock<ResourcePointerList64<Sequence>>();
            this.Tracks = reader.ReadBlock<SimpleList64<AnimTrack>>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_12h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_16h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
            writer.WriteBlock(this.Sequences);
            writer.WriteBlock(this.Tracks);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x40, Sequences),
                new Tuple<long, IResourceBlock>(0x50, Tracks)
            };
        }
    }
}
