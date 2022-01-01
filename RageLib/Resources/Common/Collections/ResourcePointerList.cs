// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.Common
{
    public class ResourcePointerList<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long BlockLength
        {
            get { return 8; }
        }

        // structure data
        public uint DataPointer;
        public ushort DataCount1;
        public ushort DataCount2;

        // reference data
        public ResourcePointerArray<T> Entries;
        
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            this.DataPointer = reader.ReadUInt32();
            this.DataCount1 = reader.ReadUInt16();
            this.DataCount2 = reader.ReadUInt16();

            this.Entries = reader.ReadBlockAt<ResourcePointerArray<T>>(
                this.DataPointer, // offset
                this.DataCount1
            );
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update...
            this.DataPointer = (uint)Entries.BlockPosition;
            this.DataCount1 = (ushort)Entries.Count;
            this.DataCount2 = (ushort)Entries.Count;

            // write...
            writer.Write(DataPointer);
            writer.Write(DataCount1);
            writer.Write(DataCount2);
        }

        public override IResourceBlock[] GetReferences()
        {
            return Entries == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Entries };
        }
    }
}
