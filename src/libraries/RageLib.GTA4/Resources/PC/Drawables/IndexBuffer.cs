// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class IndexBuffer : DatBase32
    {
        public override long BlockLength => 0x30;

        // structure data
        public uint IndicesCount;
        private uint IndicesPointer;
        private uint Unknown_0Ch; // 0x00000000
        private uint Unknown_10h; // 0xCDCDCDCD
        private uint Unknown_14h; // 0xCDCDCDCD
        private uint Unknown_18h; // 0xCDCDCDCD
        private uint Unknown_1Ch; // 0xCDCDCDCD
        private uint Unknown_20h; // 0xCDCDCDCD
        private uint Unknown_24h; // 0xCDCDCDCD
        private uint Unknown_28h; // 0xCDCDCDCD
        private uint Unknown_2Ch; // 0xCDCDCDCD

        // reference data
        public IndexBuffer_GTA4_pc? Indices { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            IndicesCount = reader.ReadUInt32();
            IndicesPointer = reader.ReadUInt32();
            Unknown_0Ch = reader.ReadUInt32();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();

            // read reference data
            Indices = reader.ReadBlockAt<IndexBuffer_GTA4_pc>(IndicesPointer, IndicesCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            IndicesPointer = (uint)(Indices?.BlockPosition ?? 0);
            IndicesCount = (uint)((Indices?.Data.Length ?? 0) / 2);

            // write structure data
            writer.Write(IndicesCount);
            writer.Write(IndicesPointer);
            writer.Write(Unknown_0Ch);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Indices is not null) list.Add(Indices);
            return list.ToArray();
        }
    }

    public class IndexBuffer_GTA4_pc : ResourceGraphicsBlock
    {
        public override long BlockLength => Data?.Length ?? 0;

        // structure data
        public byte[] Data;

        public IndexBuffer_GTA4_pc()
        {
            Data = Array.Empty<byte>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int count = Convert.ToInt32(parameters[0]);
            Data = reader.ReadBytes(count * 2);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Data);
        }
    }
}
