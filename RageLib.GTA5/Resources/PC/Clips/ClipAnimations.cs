// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crClipAnimations
    public class ClipAnimations : Clip
    {
        public override long BlockLength => 0x70;

        // structure data
        public ResourceSimpleList64<ClipAnimationsEntry> Animations;
        public float Duration;
        public uint Unknown_64h; // 0x00000001
        public uint Unknown_68h; // 0x00000000
        public uint Unknown_6Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            this.Animations = reader.ReadBlock<ResourceSimpleList64<ClipAnimationsEntry>>();
            this.Duration = reader.ReadSingle();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            writer.WriteBlock(this.Animations);
            writer.Write(this.Duration);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x50, Animations),
            };
        }
    }
}
