// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Nodes
{
    public class NodesFile : PgBase64
    {
        public override long BlockLength => 0x70;

        // structure data
        public ulong NodesPointer;
        public uint NodesCount;
        public uint Unknown_1Ch;
        public uint Unknown_20h;
        public uint Unknown_24h; // 0x00000000
        public ulong Unknown_28h_Pointer;
        public uint DataPointer1Length;
        public uint Unknown_34h; // 0x00000000
        public ulong Unknown_38h_Pointer;
        public ulong Unknown_40h_Pointer;
        public uint Unknown_48h; // 0x00000001
        public uint Unknown_4Ch; // 0x00000000
        public ulong Unknown_50h_Pointer;
        public ushort cnt5a;
        public ushort cnt5b; // same as cnt5a
        public uint Unknown_5Ch; // 0x00000000
        public uint len4; // same as cnt5a
        public uint len5;
        public uint Unknown_68h; // 0x00000000
        public uint Unknown_6Ch; // 0x00000000

        // reference data
        public SimpleArray<Node>? Nodes { get; set; }
        public SimpleArray<Unknown_ND_002>? Unknown_28h_Data { get; set; }
        public SimpleArray<Unknown_ND_003>? Unknown_38h_Data { get; set; }
        public SimpleArray<byte>? Unknown_40h_Data { get; set; }
        public SimpleArray<Unknown_ND_004>? Unknown_50h_Data { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.NodesPointer = reader.ReadUInt64();
            this.NodesCount = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h_Pointer = reader.ReadUInt64();
            this.DataPointer1Length = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h_Pointer = reader.ReadUInt64();
            this.Unknown_40h_Pointer = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h_Pointer = reader.ReadUInt64();
            this.cnt5a = reader.ReadUInt16();
            this.cnt5b = reader.ReadUInt16();
            this.Unknown_5Ch = reader.ReadUInt32();
            this.len4 = reader.ReadUInt32();
            this.len5 = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();

            // read reference data
            this.Nodes = reader.ReadBlockAt<SimpleArray<Node>>(
                this.NodesPointer, // offset
                this.NodesCount
            );
            this.Unknown_28h_Data = reader.ReadBlockAt<SimpleArray<Unknown_ND_002>>(
                this.Unknown_28h_Pointer, // offset
                this.DataPointer1Length
            );
            this.Unknown_38h_Data = reader.ReadBlockAt<SimpleArray<Unknown_ND_003>>(
                this.Unknown_38h_Pointer, // offset
                this.len4
            );
            this.Unknown_40h_Data = reader.ReadBlockAt<SimpleArray<byte>>(
                this.Unknown_40h_Pointer, // offset
                this.len5
            );
            this.Unknown_50h_Data = reader.ReadBlockAt<SimpleArray<Unknown_ND_004>>(
                this.Unknown_50h_Pointer, // offset
                this.cnt5b
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.NodesPointer = (ulong)(this.Nodes?.BlockPosition ?? 0);
            this.NodesCount = (uint)(this.Nodes != null ? this.Nodes.Count : 0);
            this.Unknown_28h_Pointer = (ulong)(this.Unknown_28h_Data?.BlockPosition ?? 0);
            this.DataPointer1Length = (uint)(this.Unknown_28h_Data != null ? this.Unknown_28h_Data.Count : 0);
            this.Unknown_38h_Pointer = (ulong)(this.Unknown_38h_Data?.BlockPosition ?? 0);
            this.Unknown_40h_Pointer = (ulong)(this.Unknown_40h_Data?.BlockPosition ?? 0);
            this.Unknown_50h_Pointer = (ulong)(this.Unknown_50h_Data?.BlockPosition ?? 0);
            this.cnt5a = (ushort)(this.Unknown_50h_Data?.Count ?? 0);
            this.cnt5b = (ushort)(this.Unknown_50h_Data?.Count ?? 0);
            this.len4 = (uint)(this.Unknown_38h_Data?.Count ?? 0);
            this.len5 = (uint)(this.Unknown_40h_Data?.Count ?? 0);

            // write structure data
            writer.Write(this.NodesPointer);
            writer.Write(this.NodesCount);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h_Pointer);
            writer.Write(this.DataPointer1Length);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h_Pointer);
            writer.Write(this.Unknown_40h_Pointer);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.Unknown_50h_Pointer);
            writer.Write(this.cnt5a);
            writer.Write(this.cnt5b);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.len4);
            writer.Write(this.len5);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Nodes != null) list.Add(Nodes);
            if (Unknown_28h_Data != null) list.Add(Unknown_28h_Data);
            if (Unknown_38h_Data != null) list.Add(Unknown_38h_Data);
            if (Unknown_40h_Data != null) list.Add(Unknown_40h_Data);
            if (Unknown_50h_Data != null) list.Add(Unknown_50h_Data);
            return list.ToArray();
        }
    }
}
