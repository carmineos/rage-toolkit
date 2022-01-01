// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common
{
    public class ResourcePointerList64<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long BlockLength
        {
            get { return 16; }
        }

        // structure data
        public ulong EntriesPointer;
        public ushort EntriesCount;
        public ushort EntriesCapacity;

        // reference data
        public ResourcePointerArray64<T> Entries;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt16();
            this.EntriesCapacity = reader.ReadUInt16();
            reader.Position += 4;

            this.Entries = reader.ReadBlockAt<ResourcePointerArray64<T>>(
                this.EntriesPointer, // offset
                this.EntriesCount
            );
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update...
            this.EntriesPointer = (ulong)(this.Entries != null ? this.Entries.BlockPosition : 0);
            this.EntriesCount = (ushort)(this.Entries != null ? this.Entries.Count : 0);
            this.EntriesCapacity = (ushort)(this.Entries != null ? this.Entries.Count : 0);

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
