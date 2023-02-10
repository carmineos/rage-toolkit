// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // datBase
    // grcIndexBuffer
    // grcIndexBufferD3D11
    public class IndexBuffer : DatBase64
    {
        public override long BlockLength => 0x60;

        // structure data
        public uint IndicesCount;
        private uint Unknown_Ch; // 0x00000000
        public ulong IndicesPointer;
        private ulong Unknown_18h; // 0x0000000000000000
        private ulong Unknown_20h; // 0x0000000000000000
        private ulong Unknown_28h; // 0x0000000000000000
        private ulong Unknown_30h; // 0x0000000000000000
        private ulong Unknown_38h; // 0x0000000000000000
        private ulong Unknown_40h; // 0x0000000000000000
        private ulong Unknown_48h; // 0x0000000000000000
        private ulong Unknown_50h; // 0x0000000000000000
        private ulong Unknown_58h; // 0x0000000000000000

        // reference data
        public IndexData_GTA5_pc? Indices { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.IndicesCount = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.IndicesPointer = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.Unknown_58h = reader.ReadUInt64();

            // read reference data
            this.Indices = reader.ReadBlockAt<IndexData_GTA5_pc>(
                this.IndicesPointer, // offset
                this.IndicesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.IndicesCount = (uint)(this.Indices != null ? this.Indices.BlockLength / 2 : 0);
            this.IndicesPointer = (ulong)(this.Indices?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.IndicesCount);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.IndicesPointer);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_58h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Indices != null) list.Add(Indices);
            return list.ToArray();
        }
    }

    public class IndexData_GTA5_pc : ResourceSystemBlock
    {
        public override long BlockLength => Data?.Length ?? 0;

        // structure data
        public byte[] Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int count = Convert.ToInt32(parameters[0]);

            Data = reader.ReadBytes(count * 2);
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
