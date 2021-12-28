// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class ShaderFX : PgBase32
    {
        public override long BlockLength => 0x60;

        // structure data
        public ushort Unknown_08h;
        public byte Unknown_0Ah;
        public byte Unknown_0Bh;
        public ushort Unknown_0Ch;
        public ushort Unknown_0Eh;
        public uint Unknown_10h;
        private uint ParametersPointer;
        public uint Unknown_18h;
        public uint ParametersCount;
        public uint Unknown_20h;
        private uint ParametersTypesPointer;
        public uint Hash;
        public uint Unknown_2Ch;
        public uint Unknown_30h;
        private uint ParametersNamesPointer;
        public uint Unknown_38h;
        public uint Unknown_3Ch;
        public uint Unknown_40h;
        public uint NamePointer;
        private uint SPSNamePointer;
        public uint Unknown_4Ch;
        public uint Unknown_50h;
        public uint Unknown_54h;
        public uint Unknown_58h;
        public uint Unknown_5Ch; //0xCDCDCDCD

        // reference data
        public ResourcePointerArray32<ShaderParameter>? Parameters { get; set; }
        public SimpleArray<byte>? ParametersTypes { get; set; }
        public SimpleArray<uint>? ParametersNamesHashes { get; set; }
        public string_r? Name { get; set; }
        public string_r? SPSName { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_08h = reader.ReadUInt16();
            Unknown_0Ah = reader.ReadByte();
            Unknown_0Bh = reader.ReadByte();
            Unknown_0Ch = reader.ReadUInt16();
            Unknown_0Eh = reader.ReadUInt16();
            Unknown_10h = reader.ReadUInt32();
            ParametersPointer = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            ParametersCount = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            ParametersTypesPointer = reader.ReadUInt32();
            Hash = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();
            Unknown_30h = reader.ReadUInt32();
            ParametersNamesPointer = reader.ReadUInt32();
            Unknown_38h = reader.ReadUInt32();
            Unknown_3Ch = reader.ReadUInt32();
            Unknown_40h = reader.ReadUInt32();
            NamePointer = reader.ReadUInt32();
            SPSNamePointer = reader.ReadUInt32();
            Unknown_4Ch = reader.ReadUInt32();
            Unknown_50h = reader.ReadUInt32();
            Unknown_54h = reader.ReadUInt32();
            Unknown_58h = reader.ReadUInt32();
            Unknown_5Ch = reader.ReadUInt32();

            // read reference data
            Parameters = reader.ReadBlockAt<ResourcePointerArray32<ShaderParameter>>(ParametersPointer, ParametersCount);
            ParametersTypes = reader.ReadBlockAt<SimpleArray<byte>>(ParametersTypesPointer, ParametersCount);
            ParametersNamesHashes = reader.ReadBlockAt<SimpleArray<uint>>(ParametersNamesPointer, ParametersCount);
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            SPSName = reader.ReadBlockAt<string_r>(SPSNamePointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            ParametersPointer = (uint)(Parameters?.BlockPosition ?? 0);
            ParametersTypesPointer = (uint)(ParametersTypes?.BlockPosition ?? 0);
            ParametersNamesPointer = (uint)(ParametersNamesHashes?.BlockPosition ?? 0);
            NamePointer = (uint)(Name?.BlockPosition ?? 0);
            SPSNamePointer = (uint)(SPSName?.BlockPosition ?? 0);
            ParametersCount = (uint)(Parameters?.Count ?? 0);

            // write structure data
            writer.Write(Unknown_08h);
            writer.Write(Unknown_0Ah);
            writer.Write(Unknown_0Bh);
            writer.Write(Unknown_0Ch);
            writer.Write(Unknown_0Eh);
            writer.Write(Unknown_10h);
            writer.Write(ParametersPointer);
            writer.Write(Unknown_18h);
            writer.Write(ParametersCount);
            writer.Write(Unknown_20h);
            writer.Write(ParametersTypesPointer);
            writer.Write(Hash);
            writer.Write(Unknown_2Ch);
            writer.Write(Unknown_30h);
            writer.Write(ParametersNamesPointer);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
            writer.Write(Unknown_40h);
            writer.Write(NamePointer);
            writer.Write(SPSNamePointer);
            writer.Write(Unknown_4Ch);
            writer.Write(Unknown_50h);
            writer.Write(Unknown_54h);
            writer.Write(Unknown_58h);
            writer.Write(Unknown_5Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Parameters is not null) list.Add(Parameters);
            if (ParametersTypes is not null) list.Add(ParametersTypes);
            if (ParametersNamesHashes is not null) list.Add(ParametersNamesHashes);
            if (Name is not null) list.Add(Name);
            if (SPSName is not null) list.Add(SPSName);
            return list.ToArray();
        }
    }

    public class ShaderParameter : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public uint Unknown_00h;
        public uint Unknown_04h;
        public uint Unknown_08h;
        public uint Unknown_0Ch;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Unknown_00h = reader.ReadUInt32();
            Unknown_04h = reader.ReadUInt32();
            Unknown_08h = reader.ReadUInt32();
            Unknown_0Ch = reader.ReadUInt32();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(Unknown_00h);
            writer.Write(Unknown_04h);
            writer.Write(Unknown_08h);
            writer.Write(Unknown_0Ch);
        }
    }
}
