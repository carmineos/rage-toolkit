// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // characterClothController
    public class CharacterClothController : ClothController
    {
        public override long BlockLength => 0xF0;

        // structure data      
        public SimpleList64<ushort> TriIndices;
        public SimpleList64<Vector4> OriginalPos;
        public float Unknown_A0h; // 0x3D23D70A
        public uint Unknown_A4h; // 0x00000000
        public uint Unknown_A8h; // 0x00000000
        public uint Unknown_ACh; // 0x00000000
        public SimpleList64<uint> BoneIndexMap;
        public SimpleList64<BindingInfo> BindingInfo;
        public uint Unknown_D0h; // 0x00000000
        public uint Unknown_D4h; // 0x00000000
        public uint Unknown_D8h; // 0x00000000
        public float Unknown_DCh; // 0x3F800000
        public SimpleList64<uint> BoneIDMap;
        
        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data         
            this.TriIndices = reader.ReadValueList<ushort>();
            this.OriginalPos = reader.ReadValueList<Vector4>();
            this.Unknown_A0h = reader.ReadSingle();
            this.Unknown_A4h = reader.ReadUInt32();
            this.Unknown_A8h = reader.ReadUInt32();
            this.Unknown_ACh = reader.ReadUInt32();
            this.BoneIndexMap = reader.ReadValueList<uint>();
            this.BindingInfo = reader.ReadValueList<BindingInfo>();
            this.Unknown_D0h = reader.ReadUInt32();
            this.Unknown_D4h = reader.ReadUInt32();
            this.Unknown_D8h = reader.ReadUInt32();
            this.Unknown_DCh = reader.ReadSingle();
            this.BoneIDMap = reader.ReadValueList<uint>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data           
            writer.WriteValueList(this.TriIndices);
            writer.WriteValueList(this.OriginalPos);
            writer.Write(this.Unknown_A0h);
            writer.Write(this.Unknown_A4h);
            writer.Write(this.Unknown_A8h);
            writer.Write(this.Unknown_ACh);
            writer.WriteValueList(this.BoneIndexMap);
            writer.WriteValueList(this.BindingInfo);
            writer.Write(this.Unknown_D0h);
            writer.Write(this.Unknown_D4h);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.Unknown_DCh);
            writer.WriteValueList(this.BoneIDMap);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (TriIndices.Entries != null) list.Add(TriIndices.Entries);
            if (OriginalPos.Entries != null) list.Add(OriginalPos.Entries);
            if (BoneIndexMap.Entries != null) list.Add(BoneIndexMap.Entries);
            if (BindingInfo.Entries != null) list.Add(BindingInfo.Entries);
            if (BoneIDMap.Entries != null) list.Add(BoneIDMap.Entries);
            return list.ToArray();
        }
    }
}
