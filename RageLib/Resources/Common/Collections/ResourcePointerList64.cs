// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.Common.Collections
{
    public class ResourcePointerList64<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long BlockLength => 0x10;

        // structure data
        public ulong EntriesPointer;
        public ushort EntriesCount;
        public ushort EntriesCapacity;

        // reference data
        public ResourcePointerArray64<T>? Entries { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            EntriesPointer = reader.ReadUInt64();
            EntriesCount = reader.ReadUInt16();
            EntriesCapacity = reader.ReadUInt16();
            reader.Position += 4;

            Entries = reader.ReadBlockAt<ResourcePointerArray64<T>>(
                EntriesPointer, // offset
                EntriesCount
            );
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update...
            EntriesPointer = (ulong)(Entries?.BlockPosition ?? 0);
            EntriesCount = (ushort)(Entries?.Count ?? 0);
            EntriesCapacity = (ushort)(Entries?.Count ?? 0);

            // write...
            writer.Write(EntriesPointer);
            writer.Write(EntriesCount);
            writer.Write(EntriesCapacity);
            writer.Write((uint)0x0000000);
        }

        public override IResourceBlock[] GetReferences()
        {
            return Entries == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Entries };
        }
    }
}
