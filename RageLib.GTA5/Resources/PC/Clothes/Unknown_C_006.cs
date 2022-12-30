// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Drawables;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    public class Unknown_C_006 : ResourceSystemBlock
    {
        public override long BlockLength => 0x190;

        // structure data
        public uint Unknown_0h; // 0x00000000
        public uint Unknown_4h; // 0x00000000
        public uint Unknown_8h; // 0x00000000
        public uint Unknown_Ch; // 0x00000000
        public uint Unknown_10h; // 0x00000000
        public uint Unknown_14h; // 0x00000000
        public uint Unknown_18h; // 0x00000000
        public uint Unknown_1Ch; // 0x00000000
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000
        public uint Unknown_30h; // 0x00000000
        public uint Unknown_34h; // 0x00000000
        public uint Unknown_38h; // 0x00000000
        public uint Unknown_3Ch; // 0x00000000
        public uint Unknown_40h; // 0x00000000
        public uint Unknown_44h; // 0x00000000
        public uint Unknown_48h; // 0x00000000
        public uint Unknown_4Ch; // 0x00000000
        public SimpleList64<Vector4> Unknown_50h;
        public SimpleList64<ushort> Unknown_60h;
        public SimpleList64<ushort> Unknown_70h;
        public SimpleList64<ushort> Unknown_80h;
        public SimpleList64<ushort> Unknown_90h;
        public SimpleList64<Vector4> Unknown_A0h;
        public SimpleList64<ushort> Unknown_B0h;
        public SimpleList64<ushort> Unknown_C0h;
        public SimpleList64<ushort> Unknown_D0h;
        public SimpleList64<ushort> Unknown_E0h;
        public uint Unknown_F0h; // 0x00000000
        public uint Unknown_F4h; // 0x00000000
        public uint Unknown_F8h; // 0x00000000
        public uint Unknown_FCh; // 0x00000000
        public uint Unknown_100h; // 0x00000000
        public uint Unknown_104h; // 0x00000000
        public uint Unknown_108h; // 0x00000000
        public uint Unknown_10Ch; // 0x00000000
        public uint Unknown_110h; // 0x00000000
        public uint Unknown_114h; // 0x00000000
        public uint Unknown_118h; // 0x00000000
        public uint Unknown_11Ch; // 0x00000000
        public uint Unknown_120h; // 0x00000000
        public uint Unknown_124h; // 0x00000000
        public uint Unknown_128h; // 0x00000000
        public uint Unknown_12Ch; // 0x00000000
        public uint Unknown_130h; // 0x00000000
        public uint Unknown_134h; // 0x00000000
        public uint Unknown_138h; // 0x00000000
        public uint Unknown_13Ch; // 0x00000000
        public uint Unknown_140h; // 0x00000000
        public uint Unknown_144h; // 0x00000000
        public uint Unknown_148h; // 0x00000000
        public uint Unknown_14Ch; // 0x00000000
        public SimpleList64<ushort> Unknown_150h;
        public SimpleList64<ushort> Unknown_160h;
        public uint Unknown_170h; // 0x00000000
        public uint Unknown_174h; // 0x00000000
        public uint Unknown_178h; // 0x00000000
        public uint Unknown_17Ch; // 0x00000000
        public uint Unknown_180h;
        public uint Unknown_184h; // 0x00000000
        public uint Unknown_188h; // 0x00000000
        public uint Unknown_18Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.Unknown_8h = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.Unknown_40h = reader.ReadUInt32();
            this.Unknown_44h = reader.ReadUInt32();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h = reader.ReadValueList<Vector4>();
            this.Unknown_60h = reader.ReadValueList<ushort>();
            this.Unknown_70h = reader.ReadValueList<ushort>();
            this.Unknown_80h = reader.ReadValueList<ushort>();
            this.Unknown_90h = reader.ReadValueList<ushort>();
            this.Unknown_A0h = reader.ReadValueList<Vector4>();
            this.Unknown_B0h = reader.ReadValueList<ushort>();
            this.Unknown_C0h = reader.ReadValueList<ushort>();
            this.Unknown_D0h = reader.ReadValueList<ushort>();
            this.Unknown_E0h = reader.ReadValueList<ushort>();
            this.Unknown_F0h = reader.ReadUInt32();
            this.Unknown_F4h = reader.ReadUInt32();
            this.Unknown_F8h = reader.ReadUInt32();
            this.Unknown_FCh = reader.ReadUInt32();
            this.Unknown_100h = reader.ReadUInt32();
            this.Unknown_104h = reader.ReadUInt32();
            this.Unknown_108h = reader.ReadUInt32();
            this.Unknown_10Ch = reader.ReadUInt32();
            this.Unknown_110h = reader.ReadUInt32();
            this.Unknown_114h = reader.ReadUInt32();
            this.Unknown_118h = reader.ReadUInt32();
            this.Unknown_11Ch = reader.ReadUInt32();
            this.Unknown_120h = reader.ReadUInt32();
            this.Unknown_124h = reader.ReadUInt32();
            this.Unknown_128h = reader.ReadUInt32();
            this.Unknown_12Ch = reader.ReadUInt32();
            this.Unknown_130h = reader.ReadUInt32();
            this.Unknown_134h = reader.ReadUInt32();
            this.Unknown_138h = reader.ReadUInt32();
            this.Unknown_13Ch = reader.ReadUInt32();
            this.Unknown_140h = reader.ReadUInt32();
            this.Unknown_144h = reader.ReadUInt32();
            this.Unknown_148h = reader.ReadUInt32();
            this.Unknown_14Ch = reader.ReadUInt32();
            this.Unknown_150h = reader.ReadValueList<ushort>();
            this.Unknown_160h = reader.ReadValueList<ushort>();
            this.Unknown_170h = reader.ReadUInt32();
            this.Unknown_174h = reader.ReadUInt32();
            this.Unknown_178h = reader.ReadUInt32();
            this.Unknown_17Ch = reader.ReadUInt32();
            this.Unknown_180h = reader.ReadUInt32();
            this.Unknown_184h = reader.ReadUInt32();
            this.Unknown_188h = reader.ReadUInt32();
            this.Unknown_18Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
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
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_44h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
            writer.WriteValueList(this.Unknown_50h);
            writer.WriteValueList(this.Unknown_60h);
            writer.WriteValueList(this.Unknown_70h);
            writer.WriteValueList(this.Unknown_80h);
            writer.WriteValueList(this.Unknown_90h);
            writer.WriteValueList(this.Unknown_A0h);
            writer.WriteValueList(this.Unknown_B0h);
            writer.WriteValueList(this.Unknown_C0h);
            writer.WriteValueList(this.Unknown_D0h);
            writer.WriteValueList(this.Unknown_E0h);
            writer.Write(this.Unknown_F0h);
            writer.Write(this.Unknown_F4h);
            writer.Write(this.Unknown_F8h);
            writer.Write(this.Unknown_FCh);
            writer.Write(this.Unknown_100h);
            writer.Write(this.Unknown_104h);
            writer.Write(this.Unknown_108h);
            writer.Write(this.Unknown_10Ch);
            writer.Write(this.Unknown_110h);
            writer.Write(this.Unknown_114h);
            writer.Write(this.Unknown_118h);
            writer.Write(this.Unknown_11Ch);
            writer.Write(this.Unknown_120h);
            writer.Write(this.Unknown_124h);
            writer.Write(this.Unknown_128h);
            writer.Write(this.Unknown_12Ch);
            writer.Write(this.Unknown_130h);
            writer.Write(this.Unknown_134h);
            writer.Write(this.Unknown_138h);
            writer.Write(this.Unknown_13Ch);
            writer.Write(this.Unknown_140h);
            writer.Write(this.Unknown_144h);
            writer.Write(this.Unknown_148h);
            writer.Write(this.Unknown_14Ch);
            writer.WriteValueList(this.Unknown_150h);
            writer.WriteValueList(this.Unknown_160h);
            writer.Write(this.Unknown_170h);
            writer.Write(this.Unknown_174h);
            writer.Write(this.Unknown_178h);
            writer.Write(this.Unknown_17Ch);
            writer.Write(this.Unknown_180h);
            writer.Write(this.Unknown_184h);
            writer.Write(this.Unknown_188h);
            writer.Write(this.Unknown_18Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Unknown_50h.Entries is not null) list.Add(Unknown_50h.Entries);
            if (Unknown_60h.Entries is not null) list.Add(Unknown_60h.Entries);
            if (Unknown_70h.Entries is not null) list.Add(Unknown_70h.Entries);
            if (Unknown_80h.Entries is not null) list.Add(Unknown_80h.Entries);
            if (Unknown_90h.Entries is not null) list.Add(Unknown_90h.Entries);
            if (Unknown_A0h.Entries is not null) list.Add(Unknown_A0h.Entries);
            if (Unknown_B0h.Entries is not null) list.Add(Unknown_B0h.Entries);
            if (Unknown_C0h.Entries is not null) list.Add(Unknown_C0h.Entries);
            if (Unknown_D0h.Entries is not null) list.Add(Unknown_D0h.Entries);
            if (Unknown_E0h.Entries is not null) list.Add(Unknown_E0h.Entries);
            if (Unknown_150h.Entries is not null) list.Add(Unknown_150h.Entries);
            if (Unknown_160h.Entries is not null) list.Add(Unknown_160h.Entries);
            return list.ToArray();
        }
    }
}
