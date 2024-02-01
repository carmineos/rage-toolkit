// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // grmGeometry
    // VFT - 0x0000000140912C48
    public class GrmGeometry : DatBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        private ulong VertexBufferPointer;
        private ulong IndexBufferPointer;
        public ulong Unknown_18h; // 0x0000000000000000
        public ulong Unknown_20h; // 0x0000000000000000          
        public uint Unknown_28h;
        public ushort Unknown_2Ch;
        public ushort Unknown_2Eh;
        public uint Unknown_30h;
        public uint Unknown_34h;
        public ulong Unknown_38h; // 0x0000000000000000			

        // reference data
        public VertexBuffer? VertexBuffer { get; set; }
        public IndexBuffer? IndexBuffer { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            VertexBufferPointer = reader.ReadUInt64();
            IndexBufferPointer = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt64();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt16();
            Unknown_2Eh = reader.ReadUInt16();
            Unknown_30h = reader.ReadUInt32();
            Unknown_30h = reader.ReadUInt32();
            Unknown_38h = reader.ReadUInt64();

            // read reference data
            VertexBuffer = reader.ReadBlockAt<VertexBuffer>(VertexBufferPointer);
            IndexBuffer = reader.ReadBlockAt<IndexBuffer>(IndexBufferPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            VertexBufferPointer = (ulong)(VertexBuffer?.BlockPosition ?? 0);
            IndexBufferPointer = (ulong)(IndexBuffer?.BlockPosition ?? 0);

            // write structure data
            writer.Write(VertexBufferPointer);
            writer.Write(IndexBufferPointer);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
            writer.Write(Unknown_2Eh);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_38h);
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
