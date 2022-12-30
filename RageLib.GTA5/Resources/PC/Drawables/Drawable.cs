// Copyright � Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // rmcDrawable
    public class Drawable : DrawableBase
    {
        public override long BlockLength => 0xA8;

        // structure data
        public PgRef64<SkeletonData> Skeleton;
        public LodGroup LodGroup;
        public PgRef64<Joints> Joints;
        public ushort Unknown_98h;
        public ushort Unknown_9Ah;
        public uint Unknown_9Ch; // 0x00000000
        public PgRef64<Lod> PrimaryLod;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Skeleton = reader.ReadPointer<SkeletonData>();
            this.LodGroup = reader.ReadBlock<LodGroup>();
            this.Joints = reader.ReadPointer<Joints>();
            this.Unknown_98h = reader.ReadUInt16();
            this.Unknown_9Ah = reader.ReadUInt16();
            this.Unknown_9Ch = reader.ReadUInt32();
            this.PrimaryLod = reader.ReadPointer<Lod>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Skeleton);
            writer.WriteBlock(this.LodGroup);
            writer.Write(this.Joints);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_9Ah);
            writer.Write(this.Unknown_9Ch);
            writer.Write(this.PrimaryLod);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Skeleton.Data != null) list.Add(Skeleton.Data);
            if (Joints.Data != null) list.Add(Joints.Data);
            if (PrimaryLod.Data != null) list.Add(PrimaryLod.Data);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, LodGroup)
            };
        }
    }
}
