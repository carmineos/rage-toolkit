// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.Common.Collections
{
    public class ResourcePointerList32<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long BlockLength => 0x8;

        // structure data
        public uint EntriesPointer;
        public ushort EntriesCount;
        public ushort EntriesCapacity;

        // reference data
        public ResourcePointerArray32<T>? Entries { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            EntriesPointer = reader.ReadUInt32();
            EntriesCount = reader.ReadUInt16();
            EntriesCapacity = reader.ReadUInt16();

            Entries = reader.ReadBlockAt<ResourcePointerArray32<T>>(
                EntriesPointer, // offset
                EntriesCount
            );
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update...
            EntriesPointer = (uint)Entries.BlockPosition;
            EntriesCount = (ushort)Entries.Count;
            EntriesCapacity = (ushort)Entries.Count;

            // write...
            writer.Write(EntriesPointer);
            writer.Write(EntriesCount);
            writer.Write(EntriesCapacity);
        }

        public override IResourceBlock[] GetReferences()
        {
            return Entries == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Entries };
        }
    }
}
