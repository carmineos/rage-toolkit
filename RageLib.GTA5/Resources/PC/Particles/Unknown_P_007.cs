// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public class Unknown_P_007 : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public uint Hash;
        public uint Unknown_4h; // 0x00000000
        public ulong p1;

        // reference data
        public Unknown_P_003 p1data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Hash = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.p1 = reader.ReadUInt64();

            // read reference data
            this.p1data = reader.ReadBlockAt<Unknown_P_003>(
                this.p1 // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.p1 = (ulong)(this.p1data != null ? this.p1data.BlockPosition : 0);

            // write structure data
            writer.Write(this.Hash);
            writer.Write(this.Unknown_4h);
            writer.Write(this.p1);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (p1data != null) list.Add(p1data);
            return list.ToArray();
        }
    }
}
