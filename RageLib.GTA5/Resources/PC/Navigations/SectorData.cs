// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    public class SectorData : ResourceSystemBlock
    {
        public override long BlockLength => 0x20;

        // structure data
        public uint c1;
        public uint Unknown_4h; // 0x00000000
        private ulong p1;
        private ulong p2;
        public ushort c2;
        public ushort c3;
        public uint Unknown_1Ch; // 0x00000000

        // reference data
        public SimpleArray<ushort>? p1data { get; set; }
        public SimpleArray<SectorDataUnk>? p2data { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.c1 = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.p1 = reader.ReadUInt64();
            this.p2 = reader.ReadUInt64();
            this.c2 = reader.ReadUInt16();
            this.c3 = reader.ReadUInt16();
            this.Unknown_1Ch = reader.ReadUInt32();

            // read reference data
            this.p1data = reader.ReadBlockAt<SimpleArray<ushort>>(
                this.p1, // offset
                this.c2
            );
            this.p2data = reader.ReadBlockAt<SimpleArray<SectorDataUnk>>(
                this.p2, // offset
                this.c3
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.p1 = (ulong)(this.p1data?.BlockPosition ?? 0);
            this.p2 = (ulong)(this.p2data?.BlockPosition ?? 0);
            this.c2 = (ushort)(this.p1data?.Count ?? 0);
            this.c3 = (ushort)(this.p2data?.Count ?? 0);

            // write structure data
            writer.Write(this.c1);
            writer.Write(this.Unknown_4h);
            writer.Write(this.p1);
            writer.Write(this.p2);
            writer.Write(this.c2);
            writer.Write(this.c3);
            writer.Write(this.Unknown_1Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (p1data != null) list.Add(p1data);
            if (p2data != null) list.Add(p2data);
            return list.ToArray();
        }
    }
}
