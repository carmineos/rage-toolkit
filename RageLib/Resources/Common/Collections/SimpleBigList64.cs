﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.Common
{
    public struct SimpleBigList64<T>  where T : unmanaged
    {
        // structure data
        public ulong EntriesPointer;
        public uint EntriesCount;
        public uint EntriesCapacity;

        // reference data
        public SimpleArray<T>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt32();
            this.EntriesCapacity = reader.ReadUInt32();

            // read reference data
            this.Entries = reader.ReadBlockAt<SimpleArray<T>>(
                this.EntriesPointer, // offset
                this.EntriesCapacity
            );

            // TODO: see https://github.com/carmineos/gta-toolkit/issues/13
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.EntriesPointer = (ulong)(this.Entries?.BlockPosition ?? 0);
            this.EntriesCount = (uint)(this.Entries?.Count ?? 0);
            this.EntriesCapacity = (uint)(this.Entries?.Count ?? 0);

            // write structure data
            writer.Write(this.EntriesPointer);
            writer.Write(this.EntriesCount);
            writer.Write(this.EntriesCapacity);
        }
    }
}
