// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // pgArray<Matrix34> array
    public class Unknown_F_001 : PgBase64
    {
        public override long BlockLength
        {
            get { return 0x20 + Data.BlockLength; }
        }

        // structure data
        public uint Count;
        public uint Unknown_14h; // 0x00000000
        public ulong Unknown_18h; // 0x0000000000000000
        public SimpleArray<Matrix4x4> Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Count = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt64();
            this.Data = reader.ReadBlock<SimpleArray<Matrix4x4>>(
              Count
              );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Count);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.WriteBlock(this.Data);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(32, Data)
            };
        }
    }
}
