// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crClipAnimation
    public class ClipAnimation : Clip
    {
        public override long BlockLength => 0x70;

        // structure data
        public ulong AnimationPointer;
        public float StartTime;
        public float EndTime;
        public float Rate;
        private uint Unknown_64h; // 0x00000000
        private uint Unknown_68h; // 0x00000000
        private uint Unknown_6Ch; // 0x00000000

        // reference data
        public Animation? Animation { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);
            this.AnimationPointer = reader.ReadUInt64();
            this.StartTime = reader.ReadSingle();
            this.EndTime = reader.ReadSingle();
            this.Rate = reader.ReadSingle();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();

            this.Animation = reader.ReadBlockAt<Animation>(
                this.AnimationPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            this.AnimationPointer = (ulong)(this.Animation?.BlockPosition ?? 0);

            writer.Write(this.AnimationPointer);
            writer.Write(this.StartTime);
            writer.Write(this.EndTime);
            writer.Write(this.Rate);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Animation != null) list.Add(Animation);
            return list.ToArray();
        }
    }
}
