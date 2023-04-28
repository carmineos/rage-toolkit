// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // grmMatrixSet
    public class MatrixSet : ResourceSystemBlock
    {
        public override long BlockLength
        {
            get { return 32 + Data.BlockLength; }
        }

        // structure data
        private uint Unknown_0h; // 0x00000000
        private uint Unknown_4h; // 0x00000000
        private uint Unknown_8h; // 0x00000000
        private uint Unknown_Ch; // 0x00000000
        public byte cnt1;
        public byte cnt2;
        private ushort Unknown_12h;
        private uint Unknown_14h; // 0x00000000
        private uint Unknown_18h; // 0x00000000
        private uint Unknown_1Ch; // 0x00000000
        public SimpleArray<Matrix3x4> Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.Unknown_8h = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.cnt1 = reader.ReadByte();
            this.cnt2 = reader.ReadByte();
            this.Unknown_12h = reader.ReadUInt16();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Data = reader.ReadBlock<SimpleArray<Matrix3x4>>(
                cnt1
                );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.cnt1);
            writer.Write(this.cnt2);
            writer.Write(this.Unknown_12h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
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
