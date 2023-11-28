// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class GrmGeometry : DatBase32
    {
        public override long BlockLength => 0x50;

        // structure data
        public uint Unknown_04h;
        public uint Unknown_08h;
        private uint VertexBufferPointer;
        public uint Unknown_10h;
        public uint Unknown_14h;
        public uint Unknown_18h;
        private uint IndexBufferPointer;
        public uint Unknown_20h;
        public uint Unknown_24h;
        public uint Unknown_28h;
        public uint IndicesCount;
        public uint FacesCount;
        public ushort VerticesCount;
        public ushort PrimitiveType;
        public uint Unknown_38h;
        public ushort VertexStride;
        public ushort Unknown_3Eh;
        public uint Unknown_40h;
        public uint Unknown_44h;
        public uint Unknown_48h;
        public uint Unknown_4Ch;

        // reference data
        public GrcVertexBuffer? VertexBuffer { get; set; }
        public IndexBuffer? IndexBuffer { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_04h = reader.ReadUInt32();
            Unknown_08h = reader.ReadUInt32();
            VertexBufferPointer = reader.ReadUInt32();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            IndexBufferPointer = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt32();
            IndicesCount = reader.ReadUInt32();
            FacesCount = reader.ReadUInt32();
            VerticesCount = reader.ReadUInt16();
            PrimitiveType = reader.ReadUInt16();
            Unknown_38h = reader.ReadUInt32();
            VertexStride = reader.ReadUInt16();
            Unknown_3Eh = reader.ReadUInt16();
            Unknown_40h = reader.ReadUInt16();
            Unknown_44h = reader.ReadUInt16();
            Unknown_48h = reader.ReadUInt16();
            Unknown_4Ch = reader.ReadUInt16();

            // read reference data
            VertexBuffer = reader.ReadBlockAt<GrcVertexBuffer>(VertexBufferPointer);
            IndexBuffer = reader.ReadBlockAt<IndexBuffer>(IndexBufferPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            VertexBufferPointer = (uint)(VertexBuffer?.BlockPosition ?? 0);
            IndexBufferPointer = (uint)(IndexBuffer?.BlockPosition ?? 0);

            // write structure data
            writer.Write(Unknown_04h);
            writer.Write(Unknown_08h);
            writer.Write(VertexBufferPointer);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(IndexBufferPointer);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
            writer.Write(IndicesCount);
            writer.Write(FacesCount);
            writer.Write(VerticesCount);
            writer.Write(PrimitiveType);
            writer.Write(Unknown_38h);
            writer.Write(VertexStride);
            writer.Write(Unknown_3Eh);
            writer.Write(Unknown_40h);
            writer.Write(Unknown_44h);
            writer.Write(Unknown_48h);
            writer.Write(Unknown_4Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (VertexBuffer is not null) list.Add(VertexBuffer);
            if (IndexBuffer is not null) list.Add(IndexBuffer);
            return list.ToArray();
        }
    }
}
