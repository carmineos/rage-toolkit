// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Textures
{
    public class GrcTexturePC : DatBase32
    {
        public override long BlockLength => 0x50;

        // structure data
        public uint Unknown_04h;
        public ushort Unknown_08h;
        public ushort Unknown_0Ah;
        public uint Unknown_0Ch;
        public uint Unknown_10h;
        private uint NamePointer;
        public uint Unknown_18h;
        public ushort Width;
        public ushort Height;
        public uint Format;
        public ushort Stride;
        public byte TextureType;
        public byte Levels;
        public float Unknown_28h;
        public float Unknown_2Ch;
        public float Unknown_30h;
        public float Unknown_34h;
        public float Unknown_38h;
        public float Unknown_3Ch;
        private uint NextTexturePointer;
        private uint PreviousTexturePointer;
        private uint TextureDataPointer;
        public uint Unknown_4Ch;

        // reference data
        public string_r? Name { get; set; }
        //public GrcTexturePC? NextTexture { get; set; }
        //public GrcTexturePC? PreviousTexture { get; set; }
        public TextureData_GTA4_pc? TextureData { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_04h = reader.ReadUInt32();
            Unknown_08h = reader.ReadUInt16();
            Unknown_0Ah = reader.ReadUInt16();
            Unknown_0Ch = reader.ReadUInt32();
            Unknown_10h = reader.ReadUInt32();
            NamePointer = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            Width = reader.ReadUInt16();
            Height = reader.ReadUInt16();
            Format = reader.ReadUInt32();
            Stride = reader.ReadUInt16();
            TextureType = reader.ReadByte();
            Levels = reader.ReadByte();
            Unknown_28h = reader.ReadSingle();
            Unknown_2Ch = reader.ReadSingle();
            Unknown_30h = reader.ReadSingle();
            Unknown_34h = reader.ReadSingle();
            Unknown_38h = reader.ReadSingle();
            Unknown_3Ch = reader.ReadSingle();
            NextTexturePointer = reader.ReadUInt32();
            PreviousTexturePointer = reader.ReadUInt32();
            TextureDataPointer = reader.ReadUInt32();
            Unknown_4Ch = reader.ReadUInt32();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            //NextTexture = reader.ReadBlockAt<GrcTexturePC>(NextTexuturePointer);
            //PreviousTexture = reader.ReadBlockAt<GrcTexturePC>(PreviousTexturePointer);
            TextureData = reader.ReadBlockAt<TextureData_GTA4_pc>(TextureDataPointer, Format, Width, Height, Stride, Levels);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            NamePointer = (uint)(Name?.BlockPosition ?? 0);
            //NextTexuturePointer = (uint)(NextTexture?.BlockPosition ?? 0);
            //PreviousTexturePointer = (uint)(PreviousTexture?.BlockPosition ?? 0);
            TextureDataPointer = (uint)(TextureData?.BlockPosition ?? 0);

            // write structure data
            writer.Write(Unknown_04h);
            writer.Write(Unknown_08h);
            writer.Write(Unknown_0Ah);
            writer.Write(Unknown_0Ch);
            writer.Write(Unknown_10h);
            writer.Write(NamePointer);
            writer.Write(Unknown_18h);
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(Format);
            writer.Write(Stride);
            writer.Write(TextureType);
            writer.Write(Levels);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_34h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
            writer.Write(NextTexturePointer);
            writer.Write(PreviousTexturePointer);
            writer.Write(TextureDataPointer);
            writer.Write(Unknown_4Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name is not null) list.Add(Name);
            //if (NextTexture is not null) list.Add(NextTexture);
            //if (PreviousTexture is not null) list.Add(PreviousTexture);
            if (TextureData is not null) list.Add(TextureData);
            return list.ToArray();
        }
    }

    public class TextureData_GTA4_pc : ResourceGraphicsBlock
    {
        public override long BlockLength => Data?.Length ?? 0;

        // structure data
        public byte[] Data;

        public TextureData_GTA4_pc()
        {
            Data = Array.Empty<byte>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            uint format = Convert.ToUInt32(parameters[0]);
            int Width = Convert.ToInt32(parameters[1]);
            int Height = Convert.ToInt32(parameters[2]);
            int Stride = Convert.ToInt32(parameters[3]);
            int Levels = Convert.ToInt32(parameters[4]);

            int fullLength = 0;
            int length = Stride * Height;
            for (int i = 0; i < Levels; i++)
            {
                fullLength += length;
                length /= 4;
            }

            Data = reader.ReadBytes(fullLength);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Data);
        }
    }
}
