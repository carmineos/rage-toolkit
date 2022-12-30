// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common
{
    public struct SimpleList64<T> where T : unmanaged
    {
        // structure data
        public ulong EntriesPointer;
        public ushort EntriesCount;
        public ushort EntriesCapacity;

        // reference data
        public SimpleArray<T>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt16();
            this.EntriesCapacity = reader.ReadUInt16();
            reader.Position += 4;

            // read reference data
            this.Entries = reader.ReadBlockAt<SimpleArray<T>>(
                this.EntriesPointer, // offset
                this.EntriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.EntriesPointer = (ulong)(this.Entries?.BlockPosition ?? 0);
            this.EntriesCount = (ushort)(this.Entries?.Count ?? 0);
            this.EntriesCapacity = (ushort)(this.Entries?.Count ?? 0);

            // write structure data
            writer.Write(this.EntriesPointer);
            writer.Write(this.EntriesCount);
            writer.Write(this.EntriesCapacity);
            writer.Write((uint)0x00000000);
        }
    }
}
