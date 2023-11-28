// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class GrcVertexBuffer : DatBase32
    {
        public override long BlockLength => 0x30;

        // structure data
        public ushort VerticesCount;
        public ushort Unknown_06h;
        private uint VertexData1Pointer;
        public uint VertexStride;
        private uint VertexDeclarationPointer;
        public uint Unknown_24h;
        private uint VertexData2Pointer;
        public uint Unknown_2Ch;

        // reference data
        public VertexData_GTA4_pc? VertexData1 { get; set; }
        public GrcVertexFormat? VertexDeclaration { get; set; }
        public VertexData_GTA4_pc? VertexData2 { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            VerticesCount = reader.ReadUInt16();
            Unknown_06h = reader.ReadUInt16();
            VertexData1Pointer = reader.ReadUInt32();
            VertexStride = reader.ReadUInt32();
            VertexDeclarationPointer = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            VertexData2Pointer = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();

            // read reference data
            VertexData1 = reader.ReadBlockAt<VertexData_GTA4_pc>(VertexData1Pointer, VertexStride, VerticesCount);
            VertexDeclaration = reader.ReadBlockAt<GrcVertexFormat>(VertexDeclarationPointer);
            VertexData2 = reader.ReadBlockAt<VertexData_GTA4_pc>(VertexData2Pointer, VertexStride, VerticesCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            VertexData1Pointer = (uint)(VertexData1?.BlockPosition ?? 0);
            VertexDeclarationPointer = (uint)(VertexDeclaration?.BlockPosition ?? 0);
            VertexData2Pointer = (uint)(VertexData2?.BlockPosition ?? 0);
            VerticesCount = (ushort)((VertexData1?.Data.Length ?? VertexData2?.Data.Length ?? 0) / VertexStride);

            // write structure data
            writer.Write(VerticesCount);
            writer.Write(Unknown_06h);
            writer.Write(VertexData1Pointer);
            writer.Write(VertexStride);
            writer.Write(VertexDeclarationPointer);
            writer.Write(Unknown_24h);
            writer.Write(VertexData2Pointer);
            writer.Write(Unknown_2Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (VertexData1 is not null) list.Add(VertexData1);
            if (VertexDeclaration is not null) list.Add(VertexDeclaration);
            if (VertexData2 is not null) list.Add(VertexData2);
            return list.ToArray();
        }
    }

    public class VertexData_GTA4_pc : ResourceGraphicsBlock
    {
        public override long BlockLength => Data?.Length ?? 0;

        // structure data
        public byte[] Data;

        public VertexData_GTA4_pc()
        {
            Data = Array.Empty<byte>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int stride = Convert.ToInt32(parameters[0]);
            int count = Convert.ToInt32(parameters[1]);

            Data = reader.ReadBytes(count * stride);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Data);
        }
    }
}
