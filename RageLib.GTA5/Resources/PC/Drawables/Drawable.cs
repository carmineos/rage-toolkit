// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

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
        public ulong SkeletonPointer;
        public LodGroup LodGroup;
        public ulong JointsPointer;
        public ushort Unknown_98h;
        public ushort Unknown_9Ah;
        public uint Unknown_9Ch; // 0x00000000
        public ulong PrimaryLodPointer;

        // reference data
        public SkeletonData Skeleton;
        public Joints Joints;
        public Lod PrimaryLod;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.SkeletonPointer = reader.ReadUInt64();
            this.LodGroup = reader.ReadBlock<LodGroup>();
            this.JointsPointer = reader.ReadUInt64();
            this.Unknown_98h = reader.ReadUInt16();
            this.Unknown_9Ah = reader.ReadUInt16();
            this.Unknown_9Ch = reader.ReadUInt32();
            this.PrimaryLodPointer = reader.ReadUInt64();

            // read reference data
            this.Skeleton = reader.ReadBlockAt<SkeletonData>(
                this.SkeletonPointer // offset
            );
            this.Joints = reader.ReadBlockAt<Joints>(
                this.JointsPointer // offset
            );
            this.PrimaryLod = reader.ReadBlockAt<Lod>(
                this.PrimaryLodPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.SkeletonPointer = (ulong)(this.Skeleton != null ? this.Skeleton.BlockPosition : 0);
            this.JointsPointer = (ulong)(this.Joints != null ? this.Joints.BlockPosition : 0);
            this.PrimaryLodPointer = (ulong)(this.PrimaryLod != null ? this.PrimaryLod.BlockPosition : 0);

            // write structure data
            writer.Write(this.SkeletonPointer);
            writer.WriteBlock(this.LodGroup);
            writer.Write(this.JointsPointer);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_9Ah);
            writer.Write(this.Unknown_9Ch);
            writer.Write(this.PrimaryLodPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Skeleton != null) list.Add(Skeleton);
            if (Joints != null) list.Add(Joints);
            if (PrimaryLod != null) list.Add(PrimaryLod);
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
