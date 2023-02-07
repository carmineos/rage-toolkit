// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.Common.Collections
{
    public class SimpleBigList32<T> : ResourceSystemBlock where T : unmanaged
    {
        public override long BlockLength => 0x10;

        // structure data
        public uint EntriesPointer;
        public uint EntriesCount;
        public uint EntriesCapacity;
        private uint Unknown_0Ch; // 0xCDCDCDCD

        // reference data
        public SimpleArray<T>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            EntriesPointer = reader.ReadUInt32();
            EntriesCount = reader.ReadUInt32();
            EntriesCapacity = reader.ReadUInt32();
            Unknown_0Ch = reader.ReadUInt32();

            // read reference data
            this.Entries = reader.ReadBlockAt<SimpleArray<T>>(EntriesPointer, EntriesCapacity);

            // TODO: see https://github.com/carmineos/gta-toolkit/issues/13
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            EntriesPointer = (uint)(Entries?.BlockPosition ?? 0);
            EntriesCount = (uint)(Entries?.Count ?? 0);
            EntriesCapacity = (uint)(Entries?.Count ?? 0);

            // write structure data
            writer.Write(EntriesPointer);
            writer.Write(EntriesCount);
            writer.Write(EntriesCapacity);
            writer.Write(Unknown_0Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return Entries is null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Entries };
        }
    }
}
