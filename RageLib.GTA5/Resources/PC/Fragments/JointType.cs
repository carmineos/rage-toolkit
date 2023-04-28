// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // pgBase
    // phJointType
    public class JointType : PgBase64, IResourceXXSystemBlock
    {
        public override long BlockLength => 0x20;

        // structure data
        private uint Unknown_10h; // 0x00000000
        private byte Unknown_14h; // 0x3F533333
        public byte Type;
        private ushort Unknown_16h;
        private uint Unknown_18h; // 0x00000000
        private uint Unknown_1Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadByte();
            this.Type = reader.ReadByte();
            this.Unknown_16h = reader.ReadUInt16();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Type);
            writer.Write(this.Unknown_16h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 21;
            var type = reader.ReadByte();
            reader.Position -= 22;

            switch (type)
            {
                case 0: return new Joint1DofType();
                case 1: return new Joint3DofType();
                default: throw new Exception("Unknown type");
            }
        }
    }
}
