// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // datBase
    // grcVertexBuffer
    // grcVertexBufferD3D11
    public class VertexBuffer : DatBase64
    {
        public override long BlockLength => 0x80;

        // structure data
        public ushort VertexStride;
        public ushort Unknown_Ah;
        public uint Unknown_Ch; // 0x00000000
        public PgRef64<VertexData_GTA5_pc> Data1;
        public uint VertexCount;
        public uint Unknown_1Ch; // 0x00000000
        public PgRef64<VertexData_GTA5_pc> Data2;
        public ulong Unknown_28h; // 0x0000000000000000
        public PgRef64<VertexDeclaration> Info;
        public ulong Unknown_38h; // 0x0000000000000000
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong Unknown_58h; // 0x0000000000000000
        public ulong Unknown_60h; // 0x0000000000000000
        public ulong Unknown_68h; // 0x0000000000000000
        public ulong Unknown_70h; // 0x0000000000000000
        public ulong Unknown_78h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.VertexStride = reader.ReadUInt16();
            this.Unknown_Ah = reader.ReadUInt16();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Data1 = reader.ReadPointer<VertexData_GTA5_pc>(false);
            this.VertexCount = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Data2 = reader.ReadPointer<VertexData_GTA5_pc>(false);
            this.Unknown_28h = reader.ReadUInt64();
            this.Info = reader.ReadPointer<VertexDeclaration>();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.Unknown_58h = reader.ReadUInt64();
            this.Unknown_60h = reader.ReadUInt64();
            this.Unknown_68h = reader.ReadUInt64();
            this.Unknown_70h = reader.ReadUInt64();
            this.Unknown_78h = reader.ReadUInt64();

            // read reference data
            this.Data1.ReadReference(reader, this.VertexStride, this.VertexCount, this.Info);
            this.Data2.ReadReference(reader, this.VertexStride, this.VertexCount, this.Info);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.VertexStride);
            writer.Write(this.Unknown_Ah);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Data1);
            writer.Write(this.VertexCount);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Data2);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Info);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_78h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Data1.Data != null) list.Add(Data1.Data);
            if (Data2.Data != null) list.Add(Data2.Data);
            if (Info.Data != null) list.Add(Info.Data);
            return list.ToArray();
        }
    }

    public class VertexData_GTA5_pc : ResourceSystemBlock
    {
        public override long BlockLength => Data?.Length ?? 0;

        // structure data
        public byte[] Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int stride = Convert.ToInt32(parameters[0]);
            int count = Convert.ToInt32(parameters[1]);

            Data = reader.ReadBytes(count * stride);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Data);
        }
    }
}
