// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.Common.Collections
{
    public class SimpleList64<T> : ResourceSystemBlock where T : unmanaged
    {
        public override long BlockLength => 0x10;

        // structure data
        public ulong EntriesPointer;
        public ushort EntriesCount;
        public ushort EntriesCapacity;

        // reference data
        public SimpleArray<T>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            EntriesPointer = reader.ReadUInt64();
            EntriesCount = reader.ReadUInt16();
            EntriesCapacity = reader.ReadUInt16();
            reader.Position += 4;

            // read reference data
            Entries = reader.ReadBlockAt<SimpleArray<T>>(
                EntriesPointer, // offset
                EntriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            EntriesPointer = (ulong)(Entries?.BlockPosition ?? 0);
            EntriesCount = (ushort)(Entries?.Count ?? 0);
            EntriesCapacity = (ushort)(Entries?.Count ?? 0);

            // write structure data
            writer.Write(EntriesPointer);
            writer.Write(EntriesCount);
            writer.Write(EntriesCapacity);
            writer.Write((uint)0x00000000);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return Entries == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Entries };
        }
    }
}
