// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    // phBoundBVH
    public class BoundBVH : BoundGeometry
    {
        public override long BlockLength => 0x150;

        // structure data
        public PgRef64<BVH> BVH;
        public ulong Unknown_138h; // 0x0000000000000000
        public ushort Unknown_140h; // 0xFFFF
        public ushort Unknown_142h; // 0x0000
        public uint Unknown_144h; // 0x00000000
        public ulong Unknown_148h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.BVH = reader.ReadPointer<BVH>();
            this.Unknown_138h = reader.ReadUInt64();
            this.Unknown_140h = reader.ReadUInt16();
            this.Unknown_142h = reader.ReadUInt16();
            this.Unknown_144h = reader.ReadUInt32();
            this.Unknown_148h = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.BVH);
            writer.Write(this.Unknown_138h);
            writer.Write(this.Unknown_140h);
            writer.Write(this.Unknown_142h);
            writer.Write(this.Unknown_144h);
            writer.Write(this.Unknown_148h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (BVH.Data is not null) list.Add(BVH.Data);
            return list.ToArray();
        }
    }
}
