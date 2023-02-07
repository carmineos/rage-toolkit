// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.Common.Collections
{
    // aiSplitArray<T, C>
    // C is used as max capacity of each SplitArrayPart<T>
    // C = 16.384 / size of (T)
    // Examples: 
    //          aiSplitArray<CNavMeshCompressedVertex,2730>
    //          aiSplitArray<TAdjPoly,2048>
    //          aiSplitArray<ushort,8192>
    //          aiSplitArray<TNavMeshPoly,341>
    public class SimpleSplitArray<T> : ResourceSystemBlock where T : unmanaged
    {
        public override long BlockLength => 0x30;

        // structure data
        public ulong VFT;
        public uint EntriesCount;
        public uint Unknown_Ch; // 0x00000000
        public ulong PartsPointer;
        public ulong OffsetsPointer;
        public uint PartsCount;
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000

        // reference data
        public ResourceSimpleArray<SimpleSplitArrayPart<T>>? Parts { get; set; }
        public SimpleArray<uint>? Offsets { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            VFT = reader.ReadUInt64();
            EntriesCount = reader.ReadUInt32();
            Unknown_Ch = reader.ReadUInt32();
            PartsPointer = reader.ReadUInt64();
            OffsetsPointer = reader.ReadUInt64();
            PartsCount = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();

            // read reference data
            Parts = reader.ReadBlockAt<ResourceSimpleArray<SimpleSplitArrayPart<T>>>(
                PartsPointer, // offset
                PartsCount
            );
            Offsets = reader.ReadBlockAt<SimpleArray<uint>>(
                OffsetsPointer, // offset
                PartsCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            PartsPointer = (ulong)(Parts?.BlockPosition ?? 0);
            OffsetsPointer = (ulong)(Offsets?.BlockPosition ?? 0);
            PartsCount = (uint)(Parts?.Count ?? 0);

            // write structure data
            writer.Write(VFT);
            writer.Write(EntriesCount);
            writer.Write(Unknown_Ch);
            writer.Write(PartsPointer);
            writer.Write(OffsetsPointer);
            writer.Write(PartsCount);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Parts != null) list.Add(Parts);
            if (Offsets != null) list.Add(Offsets);
            return list.ToArray();
        }
    }

    public class SimpleSplitArrayPart<T> : ResourceSystemBlock where T : unmanaged
    {
        public override long BlockLength => 0x10;

        // structure data
        public ulong Pointer;
        public uint Count;
        public uint Unknown_Ch; // 0x00000000

        // reference data
        public SimpleArray<T>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Pointer = reader.ReadUInt64();
            Count = reader.ReadUInt32();
            Unknown_Ch = reader.ReadUInt32();

            // read reference data
            Entries = reader.ReadBlockAt<SimpleArray<T>>(
                Pointer, // offset
                Count
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            Pointer = (ulong)(Entries?.BlockPosition ?? 0);
            Count = (uint)(Entries?.Count ?? 0);

            // write structure data
            writer.Write(Pointer);
            writer.Write(Count);
            writer.Write(Unknown_Ch);
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
