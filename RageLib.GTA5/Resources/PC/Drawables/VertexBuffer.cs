// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

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
        private ushort Unknown_Ah;
        private uint Unknown_Ch; // 0x00000000
        public ulong DataPointer1;
        public uint VertexCount;
        private uint Unknown_1Ch; // 0x00000000
        public ulong DataPointer2;
        private ulong Unknown_28h; // 0x0000000000000000
        public ulong InfoPointer;
        private ulong Unknown_38h; // 0x0000000000000000
        private ulong Unknown_40h; // 0x0000000000000000
        private ulong Unknown_48h; // 0x0000000000000000
        private ulong Unknown_50h; // 0x0000000000000000
        private ulong Unknown_58h; // 0x0000000000000000
        private ulong Unknown_60h; // 0x0000000000000000
        private ulong Unknown_68h; // 0x0000000000000000
        private ulong Unknown_70h; // 0x0000000000000000
        private ulong Unknown_78h; // 0x0000000000000000

        // reference data
        public VertexData_GTA5_pc? Data1 { get; set; }
        public VertexData_GTA5_pc? Data2 { get; set; }
        public VertexDeclaration? Info { get; set; }

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
            this.DataPointer1 = reader.ReadUInt64();
            this.VertexCount = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.DataPointer2 = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.InfoPointer = reader.ReadUInt64();
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
            this.Info = reader.ReadBlockAt<VertexDeclaration>(
                this.InfoPointer // offset
            );
            this.Data1 = reader.ReadBlockAt<VertexData_GTA5_pc>(
                this.DataPointer1, // offset
                this.VertexStride,
                this.VertexCount,
                this.Info
            );
            this.Data2 = reader.ReadBlockAt<VertexData_GTA5_pc>(
                this.DataPointer2, // offset
                this.VertexStride,
                this.VertexCount,
                this.Info
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.DataPointer1 = (ulong)(this.Data1?.BlockPosition ?? 0);
            this.DataPointer2 = (ulong)(this.Data2?.BlockPosition ?? 0);
            this.InfoPointer = (ulong)(this.Info?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.VertexStride);
            writer.Write(this.Unknown_Ah);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.DataPointer1);
            writer.Write(this.VertexCount);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.DataPointer2);
            writer.Write(this.Unknown_28h);
            writer.Write(this.InfoPointer);
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
            if (Data1 != null) list.Add(Data1);
            if (Data2 != null) list.Add(Data2);
            if (Info != null) list.Add(Info);
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
