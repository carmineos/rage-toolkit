// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // IndexBuffer
    // VFT = 0x00000001409123E0 
    public class IndexBuffer : DatBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        public uint IndicesCount;
        public uint IndexSize; // 2
        public uint Unknown_10h;
        public uint Unknown_14h;
        private ulong IndicesPointer;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        private ulong ShaderResourceViewPointer;
        public ulong Unknown_38h; // 0x0000000000000000

        // reference data
        public SimpleArray<ushort>? Indices { get; set; }
        public ShaderResourceView? ShaderResourceView { get; set; }


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            IndicesCount = reader.ReadUInt32();
            IndexSize = reader.ReadUInt32();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            IndicesPointer = reader.ReadUInt64();
            Unknown_20h = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            ShaderResourceViewPointer = reader.ReadUInt64();
            Unknown_38h = reader.ReadUInt64();

            // read reference data
            Indices = reader.ReadBlockAt<SimpleArray<ushort>>(IndicesPointer, IndicesCount);
            ShaderResourceView = reader.ReadBlockAt<ShaderResourceView>(ShaderResourceViewPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            IndicesPointer = (ulong)(Indices?.BlockPosition ?? 0);
            ShaderResourceViewPointer = (ulong)(ShaderResourceView?.BlockPosition ?? 0);

            // write reference data
            writer.Write(IndicesCount);
            writer.Write(IndexSize);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(IndicesPointer);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(ShaderResourceViewPointer);
            writer.Write(Unknown_38h);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Indices is not null) list.Add(Indices);
            if (ShaderResourceView is not null) list.Add(ShaderResourceView);
            return list.ToArray();
        }
    }
}
