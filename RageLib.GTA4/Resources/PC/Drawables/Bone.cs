// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class Bone : ResourceSystemBlock
    {
        public override long BlockLength => 0xE0;

        // structure data
        private uint NamePointer;
        public ushort Unknown_04h;
        public ushort Flags;
        private uint NextSiblingPointer;
        private uint FirstChildPointer;
        private uint ParentPointer;
        public ushort BoneIndex;
        public ushort BoneId;
        public ushort Mirror;
        public ushort Unknown_1Ah;
        public uint Unknown_1Ch;
        public Vector4 LocalOffset;
        public Vector4 RotationEuler;
        public Quaternion RotationQuaternion;
        public Vector4 Scale;
        public Vector4 WorldOffset;
        public Vector4 Orient;
        public Vector4 Sorient;
        public Vector4 TranslationMin;
        public Vector4 TranslationMax;
        public Vector4 RotationMin;
        public Vector4 RotationMax;
        public Vector4 Unknown_D0h;

        // reference data
        public string_r? Name { get; set; }
        public Bone? NextSibling { get; set; }
        public Bone? FirstChild { get; set; }
        public Bone? Parent { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            NamePointer = reader.ReadUInt32();
            Unknown_04h = reader.ReadUInt16();
            Flags = reader.ReadUInt16();
            NextSiblingPointer = reader.ReadUInt32();
            FirstChildPointer = reader.ReadUInt32();
            ParentPointer = reader.ReadUInt32();
            BoneIndex = reader.ReadUInt16();
            BoneId = reader.ReadUInt16();
            Mirror = reader.ReadUInt16();
            Unknown_1Ah = reader.ReadUInt16();
            Unknown_1Ch = reader.ReadUInt32();
            LocalOffset = reader.ReadVector4();
            RotationEuler = reader.ReadVector4();
            RotationQuaternion = reader.ReadQuaternion();
            Scale = reader.ReadVector4();
            WorldOffset = reader.ReadVector4();
            Orient = reader.ReadVector4();
            Sorient = reader.ReadVector4();
            TranslationMin = reader.ReadVector4();
            TranslationMax = reader.ReadVector4();
            RotationMin = reader.ReadVector4();
            RotationMax = reader.ReadVector4();
            Unknown_D0h = reader.ReadVector4();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            NextSibling = reader.ReadBlockAt<Bone>(NextSiblingPointer);
            FirstChild = reader.ReadBlockAt<Bone>(FirstChildPointer);
            Parent = reader.ReadBlockAt<Bone>(ParentPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            NamePointer = (uint)(Name?.BlockPosition ?? 0);
            NextSiblingPointer = (uint)(NextSibling?.BlockPosition ?? 0);
            FirstChildPointer = (uint)(FirstChild?.BlockPosition ?? 0);
            ParentPointer = (uint)(Parent?.BlockPosition ?? 0);

            // write structure data
            writer.Write(NamePointer);
            writer.Write(Unknown_04h);
            writer.Write(Flags);
            writer.Write(NextSiblingPointer);
            writer.Write(FirstChildPointer);
            writer.Write(ParentPointer);
            writer.Write(BoneIndex);
            writer.Write(BoneId);
            writer.Write(Mirror);
            writer.Write(Unknown_1Ah);
            writer.Write(Unknown_1Ch);
            writer.Write(LocalOffset);
            writer.Write(RotationEuler);
            writer.Write(RotationQuaternion);
            writer.Write(Scale);
            writer.Write(WorldOffset);
            writer.Write(Orient);
            writer.Write(Sorient);
            writer.Write(TranslationMin);
            writer.Write(TranslationMax);
            writer.Write(RotationMin);
            writer.Write(RotationMax);
            writer.Write(Unknown_D0h);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name is not null) list.Add(Name);
            if (NextSibling is not null) list.Add(NextSibling);
            if (FirstChild is not null) list.Add(FirstChild);
            if (Parent is not null) list.Add(Parent);
            return list.ToArray();
        }
    }
}
