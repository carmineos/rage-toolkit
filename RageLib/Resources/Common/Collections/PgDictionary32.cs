// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;

namespace RageLib.Resources.Common.Collections
{
    // pgBase
    // pgDictionaryBase
    // pgDictionary<T>
    public class PgDictionary32<T> : PgBase32 where T : IResourceSystemBlock, new()
    {
        public override long BlockLength => 0x20;

        // structure data
        public uint ParentPointer;
        public uint Count;
        public SimpleList32<uint> Hashes;
        public ResourcePointerList32<T> Values;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            ParentPointer = reader.ReadUInt32();
            Count = reader.ReadUInt32();
            Hashes = reader.ReadBlock<SimpleList32<uint>>();
            Values = reader.ReadBlock<ResourcePointerList32<T>>();
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
            writer.WriteBlock(Hashes);
            writer.WriteBlock(Values);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x10, Hashes),
                new Tuple<long, IResourceBlock>(0x18, Values)
            };
        }
    }
}
