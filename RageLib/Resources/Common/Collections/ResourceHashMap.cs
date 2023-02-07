// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common.Collections
{
    // TODO: create a common base class for this and AtHashMap
    public class ResourceHashMap<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long BlockLength => 0x10;

        // structure data
        public ulong BucketsPointer;
        public ushort BucketsCount;
        public ushort Count;
        private ushort Unknown_Ch;
        private byte Unknown_Eh;
        public byte Initialized;

        // reference data
        public ResourcePointerArray64<ResourceHashMapEntry<T>>? Buckets { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            BucketsPointer = reader.ReadUInt64();
            BucketsCount = reader.ReadUInt16();
            Count = reader.ReadUInt16();
            Unknown_Ch = reader.ReadUInt16();
            Unknown_Eh = reader.ReadByte();
            Initialized = reader.ReadByte();

            // read reference data
            Buckets = reader.ReadBlockAt<ResourcePointerArray64<ResourceHashMapEntry<T>>>(
                BucketsPointer, // offset
                BucketsCount
            );
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            BucketsPointer = (ulong)(Buckets?.BlockPosition ?? 0);

            // write structure data
            writer.Write(BucketsPointer);
            writer.Write(BucketsCount);
            writer.Write(Count);
            writer.Write(Unknown_Ch);
            writer.Write(Unknown_Eh);
            writer.Write(Initialized);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return Buckets == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Buckets };
        }

        // Don't use it for now
        //public override void Update()
        //{
        //    Resize(GetEntries());
        //}

        public ResourceHashMap()
        {

        }

        public ResourceHashMap(ICollection<KeyValuePair<uint, T>> items)
        {
            Resize(items);
        }

        private ushort GetBucketsCount(uint hashesCount)
        {
            if (hashesCount < 11) return 11;
            else if (hashesCount < 29) return 29;
            else if (hashesCount < 59) return 59;
            else if (hashesCount < 107) return 107;
            else if (hashesCount < 191) return 191;
            else if (hashesCount < 331) return 331;
            else if (hashesCount < 563) return 563;
            else if (hashesCount < 953) return 953;
            else if (hashesCount < 1609) return 1609;
            else if (hashesCount < 2729) return 2729;
            else if (hashesCount < 4621) return 4621;
            else if (hashesCount < 7841) return 7841;
            else if (hashesCount < 13297) return 13297;
            else if (hashesCount < 22571) return 22571;
            else if (hashesCount < 38351) return 38351;
            else if (hashesCount < 65167) return 65167;
            else if (hashesCount < 65521) return 65521;
            else return 0;
        }

        private void Resize(ICollection<KeyValuePair<uint, T>> entries)
        {
            Count = (ushort)entries.Count;
            BucketsCount = GetBucketsCount((uint)entries.Count);

            Buckets = new ResourcePointerArray64<ResourceHashMapEntry<T>>();

            for (int i = 0; i < BucketsCount; i++)
                Buckets.Add(null);

            foreach (var entry in entries)
                Add(entry);

            Initialized = 1;
        }

        public void Add(KeyValuePair<uint, T> item)
        {
            var entry = new ResourceHashMapEntry<T>()
            {
                Hash = item.Key,
                Data = item.Value,
                Next = null,
                NextPointer = 0,
            };

            var bucket = (int)(entry.Hash % BucketsCount);

            if (Buckets[bucket] == null)
            {
                Buckets[bucket] = entry;
            }
            else
            {
                var current = Buckets[bucket];

                while (current.Next != null)
                    current = current.Next;

                current.Next = entry;
            }
        }

        private List<KeyValuePair<uint, T>> GetEntries()
        {
            var entries = new List<KeyValuePair<uint, T>>();

            if (Buckets == null)
                return entries;

            foreach (var bucket in Buckets)
            {
                if (bucket == null)
                    continue;

                var entry = bucket;

                do
                {
                    if (entry.Data != null)
                        entries.Add(new KeyValuePair<uint, T>(entry.Hash, entry.Data));

                    entry = entry.Next;

                } while (entry != null);
            }

            return entries;
        }
    }

    public class ResourceHashMapEntry<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long BlockLength => 0x20;

        // structure data
        public uint Hash;
        public uint Unknown_4h; // 0x00000000
        public ulong DataPointer;
        public ulong NextPointer;
        public uint Unknown_18h; // 0x00000000
        public uint Unknown_1Ch; // 0x00000000

        // reference data
        public T? Data { get; set; }
        public ResourceHashMapEntry<T>? Next { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Hash = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            DataPointer = reader.ReadUInt64();
            NextPointer = reader.ReadUInt64();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();

            // read reference data
            Data = reader.ReadBlockAt<T>(
                DataPointer // offset
            );
            Next = reader.ReadBlockAt<ResourceHashMapEntry<T>>(
                NextPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            DataPointer = (ulong)(Data?.BlockPosition ?? 0);
            NextPointer = (ulong)(Next?.BlockPosition ?? 0);

            // write structure data
            writer.Write(Hash);
            writer.Write(Unknown_4h);
            writer.Write(DataPointer);
            writer.Write(NextPointer);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Data != null) list.Add(Data);
            if (Next != null) list.Add(Next);
            return list.ToArray();
        }
    }
}