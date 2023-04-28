// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.WaypointRecords
{
    public class WaypointRecord : PgBase64
    {
        public override long BlockLength => 0x30;

        // structure data
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        public ulong EntriesPointer;
        public uint EntriesCount;
        private uint Unknown_24h; // 0x00000000
        private uint Unknown_28h; // 0x00000000
        private uint Unknown_2Ch; // 0x00000000

        // reference data
        public SimpleArray<WaypointRecordEntry>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();

            // read reference data
            this.Entries = reader.ReadBlockAt<SimpleArray<WaypointRecordEntry>>(
                this.EntriesPointer, // offset
                this.EntriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.EntriesPointer = (ulong)(this.Entries?.BlockPosition ?? 0);
            this.EntriesCount = (uint)(this.Entries?.Count ?? 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.EntriesPointer);
            writer.Write(this.EntriesCount);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Entries != null) list.Add(Entries);
            return list.ToArray();
        }
    }
}
