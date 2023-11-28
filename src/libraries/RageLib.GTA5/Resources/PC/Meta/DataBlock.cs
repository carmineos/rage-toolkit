// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Meta
{
    public class DataBlock : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public int StructureNameHash { get; set; }
        public int DataLength { get; private set; }
        public long DataPointer { get; private set; }

        // reference data
        public SimpleArray<byte> Data { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.StructureNameHash = reader.ReadInt32();
            this.DataLength = reader.ReadInt32();
            this.DataPointer = reader.ReadInt64();

            // read reference data
            this.Data = reader.ReadBlockAt<SimpleArray<byte>>(
                (ulong)this.DataPointer, // offset
                this.DataLength
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.DataLength = this.Data?.Count ?? 0;
            this.DataPointer = this.Data?.BlockPosition ?? 0;

            // write structure data
            writer.Write(this.StructureNameHash);
            writer.Write(this.DataLength);
            writer.Write(this.DataPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Data != null) list.Add(Data);
            return list.ToArray();
        }
    }
}
