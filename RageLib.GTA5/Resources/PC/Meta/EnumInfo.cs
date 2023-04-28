// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Meta
{
    public class EnumInfo : ResourceSystemBlock
    {
        public override long BlockLength => 24;

        // structure data
        public int EnumNameHash { get; set; }
        public int EnumKey { get; set; }
        public long EntriesPointer { get; private set; }
        public int EntriesCount { get; private set; }
        private int Unknown_14h { get; set; } = 0x00000000;

        // reference data
        public ResourceSimpleArray<EnumEntryInfo> Entries;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EnumNameHash = reader.ReadInt32();
            this.EnumKey = reader.ReadInt32();
            this.EntriesPointer = reader.ReadInt64();
            this.EntriesCount = reader.ReadInt32();
            this.Unknown_14h = reader.ReadInt32();

            // read reference data
            this.Entries = reader.ReadBlockAt<ResourceSimpleArray<EnumEntryInfo>>(
                (ulong)this.EntriesPointer, // offset
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
            this.EntriesCount = this.Entries?.Count ?? 0;

            // write structure data
            writer.Write(this.EnumNameHash);
            writer.Write(this.EnumKey);
            writer.Write(this.EntriesPointer);
            writer.Write(this.EntriesCount);
            writer.Write(this.Unknown_14h);
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
