// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;

namespace RageLib.Resources.Common.Collections
{
    // pgBase
    // pgDictionaryBase
    // pgDictionary<T>
    public class PgDictionary64<T> : PgBase64 where T : IResourceSystemBlock, new()
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong ParentPointer; // 0x0000000000000000
        public uint Count; // 0x00000001
        private uint Unknown_1Ch; // 0x00000000
        public SimpleList64<uint> Hashes;
        public ResourcePointerList64<T> Values;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            ParentPointer = reader.ReadUInt64();
            Count = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Hashes = reader.ReadBlock<SimpleList64<uint>>();
            Values = reader.ReadBlock<ResourcePointerList64<T>>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(ParentPointer);
            writer.Write(Count);
            writer.Write(Unknown_1Ch);
            writer.WriteBlock(Hashes);
            writer.WriteBlock(Values);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, Hashes),
                new Tuple<long, IResourceBlock>(0x30, Values)
            };
        }
    }
}
