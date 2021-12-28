// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class SkeletonData : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        private uint BonesPointer;
        private uint Unknown_04h_Pointer;
        private uint Unknown_08h_Pointer;
        private uint Unknown_0Ch_Pointer;
        private uint Unknown_10h_Pointer;
        public ushort BonesCount;
        public ushort Unknown_16h;
        public uint Unknown_18h;
        public uint Flags;
        public SimpleList32<BoneIdMapping> Unknown_20h;
        public uint Unknown_28h;
        public uint Unknown_2Ch_Hash;
        public uint Unknown_30h;
        public uint Unknown_34h;
        public uint Unknown_38h;
        public uint Unknown_3Ch;

        // reference data
        public ResourceSimpleArray<Bone>? Bones { get; set; }
        public SimpleArray<uint>? Unknown_04h_Data { get; set; }
        public SimpleArray<Matrix4x4>? Unknown_08h_Data { get; set; }
        public SimpleArray<Matrix4x4>? Unknown_0Ch_Data { get; set; }
        public SimpleArray<Matrix4x4>? Unknown_10h_Data { get; set; }

        public SkeletonData()
        {
            Unknown_20h = new SimpleList32<BoneIdMapping>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            BonesPointer = reader.ReadUInt32();
            Unknown_04h_Pointer = reader.ReadUInt32();
            Unknown_08h_Pointer = reader.ReadUInt32();
            Unknown_0Ch_Pointer = reader.ReadUInt32();
            Unknown_10h_Pointer = reader.ReadUInt32();
            BonesCount = reader.ReadUInt16();
            Unknown_16h = reader.ReadUInt16();
            Unknown_18h = reader.ReadUInt32();
            Flags = reader.ReadUInt32();
            Unknown_20h = reader.ReadBlock<SimpleList32<BoneIdMapping>>();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch_Hash = reader.ReadUInt32();
            Unknown_30h = reader.ReadUInt32();
            Unknown_34h = reader.ReadUInt32();
            Unknown_38h = reader.ReadUInt32();
            Unknown_3Ch = reader.ReadUInt32();

            // read reference data
            Bones = reader.ReadBlockAt<ResourceSimpleArray<Bone>>(BonesPointer, BonesCount);
            Unknown_04h_Data = reader.ReadBlockAt<SimpleArray<uint>>(Unknown_04h_Pointer, BonesCount);
            Unknown_08h_Data = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(Unknown_08h_Pointer, BonesCount);
            Unknown_0Ch_Data = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(Unknown_0Ch_Pointer, BonesCount);
            Unknown_10h_Data = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(Unknown_10h_Pointer, BonesCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            BonesPointer = (uint)(Bones?.BlockPosition ?? 0);
            Unknown_04h_Pointer = (uint)(Unknown_04h_Data?.BlockPosition ?? 0);
            Unknown_08h_Pointer = (uint)(Unknown_08h_Data?.BlockPosition ?? 0);
            Unknown_10h_Pointer = (uint)(Unknown_10h_Data?.BlockPosition ?? 0);
            BonesCount = (ushort)(Bones?.Count ?? 0);

            // write structure data
            writer.Write(BonesPointer);
            writer.Write(Unknown_04h_Pointer);
            writer.Write(Unknown_08h_Pointer);
            writer.Write(Unknown_0Ch_Pointer);
            writer.Write(Unknown_10h_Pointer);
            writer.Write(BonesCount);
            writer.Write(Unknown_16h);
            writer.Write(Unknown_18h);
            writer.Write(Flags);
            writer.WriteBlock(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch_Hash);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_34h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x20, Unknown_20h),
            };
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Bones is not null) list.Add(Bones);
            if (Unknown_04h_Data is not null) list.Add(Unknown_04h_Data);
            if (Unknown_08h_Data is not null) list.Add(Unknown_08h_Data);
            if (Unknown_10h_Data is not null) list.Add(Unknown_10h_Data);
            return list.ToArray();
        }
    }
}
