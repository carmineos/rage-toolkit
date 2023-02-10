// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crPropertyAttribute
    public class PropertyAttribute : ResourceSystemBlock, IResourceXXSystemBlock
    {
        public override long BlockLength => 0x20;

        // structure data
        public uint VFT;
        private uint Unknown_4h; // 0x00000001
        public byte Type;
        private byte Unknown_9h; // 0x00
        private ushort Unknown_Ah; // 0x0000
        private uint Unknown_Ch; // 0x00000000
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        public uint NameHash;
        private uint Unknown_1Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.VFT = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.Type = reader.ReadByte();
            this.Unknown_9h = reader.ReadByte();
            this.Unknown_Ah = reader.ReadUInt16();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.NameHash = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.VFT);
            writer.Write(this.Unknown_4h);
            writer.Write(this.Type);
            writer.Write(this.Unknown_9h);
            writer.Write(this.Unknown_Ah);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.NameHash);
            writer.Write(this.Unknown_1Ch);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 8;
            var type = reader.ReadByte();
            reader.Position -= 9;

            switch (type)
            {
                //crPropertyAttributeSituation
                //crPropertyAttributeBitSet
                //crPropertyAttributeVector4
                //crPropertyAttributeMatrix34
                //crPropertyAttributeData

                case 1: return new PropertyAttributeFloat();
                case 2: return new PropertyAttributeInt();
                case 3: return new PropertyAttributeBool();
                case 4: return new PropertyAttributeString();
                case 6: return new PropertyAttributeVector3();
                case 8: return new PropertyAttributeQuaternion();
                case 12: return new PropertyAttributeHashString();
                default: throw new Exception("Unknown attribute type");
            }
        }
    }
}
