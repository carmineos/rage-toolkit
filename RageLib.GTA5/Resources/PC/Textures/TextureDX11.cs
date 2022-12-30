// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Textures
{
    // grcTexturePC
    // grcTextureDX11
    public class TextureDX11 : Texture
    {
        public override long BlockLength => 0x90;

        // structure data
        public ushort Width;
        public ushort Height;
        public ushort Depth; // 0x0001
        public ushort Stride;
        public uint Format;
        public byte Unknown_5Ch; // 0x00
        public byte Levels;
        public ushort Unknown_5Eh; // 0x0000
        public uint Unknown_60h; // 0x00000000
        public uint Unknown_64h; // 0x00000000
        public uint Unknown_68h; // 0x00000000
        public uint Unknown_6Ch; // 0x00000000
        public PgRef64<TextureData_GTA5_pc> Data;
        public uint Unknown_78h; // 0x00000000
        public uint Unknown_7Ch; // 0x00000000
        public uint Unknown_80h; // 0x00000000
        public uint Unknown_84h; // 0x00000000
        public uint Unknown_88h; // 0x00000000
        public uint Unknown_8Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Width = reader.ReadUInt16();
            this.Height = reader.ReadUInt16();
            this.Depth = reader.ReadUInt16();
            this.Stride = reader.ReadUInt16();
            this.Format = reader.ReadUInt32();
            this.Unknown_5Ch = reader.ReadByte();
            this.Levels = reader.ReadByte();
            this.Unknown_5Eh = reader.ReadUInt16();
            this.Unknown_60h = reader.ReadUInt32();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();
            this.Data = reader.ReadPointer<TextureData_GTA5_pc>(false);
            this.Unknown_78h = reader.ReadUInt32();
            this.Unknown_7Ch = reader.ReadUInt32();
            this.Unknown_80h = reader.ReadUInt32();
            this.Unknown_84h = reader.ReadUInt32();
            this.Unknown_88h = reader.ReadUInt32();
            this.Unknown_8Ch = reader.ReadUInt32();

            // read reference data
            this.Data.ReadReference(reader, this.Format, this.Width, this.Height, this.Levels, this.Stride);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Width);
            writer.Write(this.Height);
            writer.Write(this.Depth);
            writer.Write(this.Stride);
            writer.Write(this.Format);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Levels);
            writer.Write(this.Unknown_5Eh);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
            writer.Write(this.Data);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_7Ch);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_84h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_8Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Data.Data is not null) list.Add(Data.Data);
            return list.ToArray();
        }
    }

    public class TextureData_GTA5_pc : ResourceGraphicsBlock
    {
        public override long BlockLength
        {
            get
            {
                return FullData.Length;
            }
        }

        public byte[] FullData;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            uint format = Convert.ToUInt32(parameters[0]);
            int Width = Convert.ToInt32(parameters[1]);
            int Height = Convert.ToInt32(parameters[2]);
            int Levels = Convert.ToInt32(parameters[3]);
            int Stride = Convert.ToInt32(parameters[4]);

            int fullLength = 0;
            int length = Stride * Height;
            for (int i = 0; i < Levels; i++)
            {
                fullLength += length;
                length /= 4;
            }

            FullData = reader.ReadBytes(fullLength);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(FullData);
        }
    }
}
