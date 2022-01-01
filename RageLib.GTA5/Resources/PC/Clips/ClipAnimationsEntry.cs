// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.GTA5.PC.Clips
{
    public class ClipAnimationsEntry : ResourceSystemBlock
    {
        public override long BlockLength => 24;

        // structure data
        public float StartTime;
        public float EndTime;
        public float Rate;
        public uint Unknown_Ch; // 0x00000000
        public ulong AnimationPointer;

        // reference data
        public Animation Animation;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.StartTime = reader.ReadSingle();
            this.EndTime = reader.ReadSingle();
            this.Rate = reader.ReadSingle();
            this.Unknown_Ch = reader.ReadUInt32();
            this.AnimationPointer = reader.ReadUInt64();

            // read reference data
            this.Animation = reader.ReadBlockAt<Animation>(
                this.AnimationPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.AnimationPointer = (ulong)(this.Animation != null ? this.Animation.BlockPosition : 0);

            // write structure data
            writer.Write(this.StartTime);
            writer.Write(this.EndTime);
            writer.Write(this.Rate);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.AnimationPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return Animation == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Animation };
        }
    }
}
