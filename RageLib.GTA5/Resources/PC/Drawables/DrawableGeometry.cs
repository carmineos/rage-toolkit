// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // datBase
    // grmGeometry
    // grmGeometryQB
    public class DrawableGeometry : DatBase64
    {
        public override long BlockLength => 0xA0;

        // structure data
        public ulong Unknown_8h; // 0x0000000000000000
        public ulong Unknown_10h; // 0x0000000000000000
        public PgRef64<VertexBuffer> VertexBuffer;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public PgRef64<IndexBuffer> IndexBuffer;
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000
        public ulong Unknown_50h; // 0x0000000000000000
        public uint IndicesCount;
        public uint FacesCount;
        public ushort VerticesCount;
        public ushort IndicesPerFace; // 0x0003
        public uint Unknown_64h; // 0x00000000
        public PgRef64<SimpleArray<ushort>> BonesId;
        public ushort VertexStride;
        public ushort BonesCount;
        public uint Unknown_74h; // 0x00000000
        public PgRef64<VertexData_GTA5_pc> VertexData;
        public ulong Unknown_80h; // 0x0000000000000000
        public ulong Unknown_88h; // 0x0000000000000000
        public ulong Unknown_90h; // 0x0000000000000000
        public ulong Unknown_98h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_8h = reader.ReadUInt64();
            this.Unknown_10h = reader.ReadUInt64();
            this.VertexBuffer = reader.ReadPointer<VertexBuffer>();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.IndexBuffer = reader.ReadPointer<IndexBuffer>();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.IndicesCount = reader.ReadUInt32();
            this.FacesCount = reader.ReadUInt32();
            this.VerticesCount = reader.ReadUInt16();
            this.IndicesPerFace = reader.ReadUInt16();
            this.Unknown_64h = reader.ReadUInt32();
            this.BonesId = reader.ReadPointer<SimpleArray<ushort>>(false);
            this.VertexStride = reader.ReadUInt16();
            this.BonesCount = reader.ReadUInt16();
            this.Unknown_74h = reader.ReadUInt32();
            this.VertexData = reader.ReadPointer<VertexData_GTA5_pc>(false);
            this.Unknown_80h = reader.ReadUInt64();
            this.Unknown_88h = reader.ReadUInt64();
            this.Unknown_90h = reader.ReadUInt64();
            this.Unknown_98h = reader.ReadUInt64();

            this.BonesId.ReadReference(reader, this.BonesCount);
            this.VertexData.ReadReference(reader, this.VertexStride, this.VerticesCount, this.VertexBuffer.Data?.Info);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_10h);
            writer.Write(this.VertexBuffer);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.IndexBuffer);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.IndicesCount);
            writer.Write(this.FacesCount);
            writer.Write(this.VerticesCount);
            writer.Write(this.IndicesPerFace);
            writer.Write(this.Unknown_64h);
            writer.Write(this.BonesId);
            writer.Write(this.VertexStride);
            writer.Write(this.BonesCount);
            writer.Write(this.Unknown_74h);
            writer.Write(this.VertexData);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_98h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (VertexBuffer.Data != null) list.Add(VertexBuffer.Data);
            if (IndexBuffer.Data != null) list.Add(IndexBuffer.Data);
            if (BonesId.Data != null) list.Add(BonesId.Data);
            if (VertexData.Data != null) list.Add(VertexData.Data);
            return list.ToArray();
        }
    }
}
