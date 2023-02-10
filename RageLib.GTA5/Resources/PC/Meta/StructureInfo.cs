// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Meta
{
    public class StructureInfo : ResourceSystemBlock
    {
        public override long BlockLength => 0x20;

        // structure data
        public int StructureNameHash { get; set; }
        public int StructureKey { get; set; }
        public int Unknown_8h { get; set; }
        private int Unknown_Ch { get; set; } = 0x00000000;
        public long EntriesPointer { get; private set; }
        public int StructureLength { get; set; }
        private short Unknown_1Ch { get; set; } = 0x0000;
        public short EntriesCount { get; private set; }

        // reference data
        public ResourceSimpleArray<StructureEntryInfo> Entries;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.StructureNameHash = reader.ReadInt32();
            this.StructureKey = reader.ReadInt32();
            this.Unknown_8h = reader.ReadInt32();
            this.Unknown_Ch = reader.ReadInt32();
            this.EntriesPointer = reader.ReadInt64();
            this.StructureLength = reader.ReadInt32();
            this.Unknown_1Ch = reader.ReadInt16();
            this.EntriesCount = reader.ReadInt16();

            // read reference data
            this.Entries = reader.ReadBlockAt<ResourceSimpleArray<StructureEntryInfo>>(
                (uint)this.EntriesPointer, // offset
                this.EntriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.EntriesPointer = this.Entries?.BlockPosition ?? 0;
            this.EntriesCount = (short)(this.Entries?.Count ?? 0);

            // write structure data
            writer.Write(this.StructureNameHash);
            writer.Write(this.StructureKey);
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.EntriesPointer);
            writer.Write(this.StructureLength);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.EntriesCount);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Entries != null) list.Add(Entries);
            return list.ToArray();
        }
    }
}
