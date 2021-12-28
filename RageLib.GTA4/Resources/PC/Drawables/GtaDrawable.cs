// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class GtaDrawable : PgBase32
    {
        public override long BlockLength => 0x88;

        // structure data
        private uint ShaderGroupPointer;
        private uint SkeletonDataPointer;
        public GrmLodGroup LodGroup;
        public float Radius;
        private uint Unknown_74h; // 0x00000000
        private uint Unknown_78h; // 0x00000000
        private uint Unknown_7Ch; // 0x00000000
        public ResourceSimpleList32<CLightAttr> LightAttributes;

        // reference data
        public ShaderGroup? ShaderGroup { get; set; }
        public SkeletonData? SkeletonData { get; set; }

        public GtaDrawable()
        {
            LodGroup = new GrmLodGroup();
            LightAttributes = new ResourceSimpleList32<CLightAttr>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            ShaderGroupPointer = reader.ReadUInt32();
            SkeletonDataPointer = reader.ReadUInt32();
            LodGroup = reader.ReadBlock<GrmLodGroup>();
            Radius = reader.ReadSingle();
            Unknown_74h = reader.ReadUInt32();
            Unknown_78h = reader.ReadUInt32();
            Unknown_7Ch = reader.ReadUInt32();
            LightAttributes = reader.ReadBlock<ResourceSimpleList32<CLightAttr>>();

            // read reference data
            ShaderGroup = reader.ReadBlockAt<ShaderGroup>(ShaderGroupPointer);
            SkeletonData = reader.ReadBlockAt<SkeletonData>(SkeletonDataPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            ShaderGroupPointer = (uint)(ShaderGroup?.BlockPosition ?? 0);
            SkeletonDataPointer = (uint)(SkeletonData?.BlockPosition ?? 0);

            // write structure data
            writer.Write(ShaderGroupPointer);
            writer.Write(SkeletonDataPointer);
            writer.WriteBlock(LodGroup);
            writer.Write(Radius);
            writer.Write(Unknown_74h);
            writer.Write(Unknown_78h);
            writer.Write(Unknown_7Ch);
            writer.WriteBlock(LightAttributes);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (ShaderGroup is not null) list.Add(ShaderGroup);
            if (SkeletonData is not null) list.Add(SkeletonData);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x10, LodGroup),
                new Tuple<long, IResourceBlock>(0x80, LightAttributes),
            };
        }
    }
}
