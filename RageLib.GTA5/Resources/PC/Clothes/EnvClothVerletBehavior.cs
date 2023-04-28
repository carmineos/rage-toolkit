// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // datBase
    // phInstBehavior
    // phClothVerletBehavior
    // phEnvClothVerletBehavior
    public class EnvClothVerletBehavior : DatBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        private uint Unknown_8h; // 0x00000000
        private uint Unknown_Ch; // 0x00000000
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        private uint Unknown_18h; // 0x00000000
        private uint Unknown_1Ch; // 0x00000000
        private uint Unknown_20h; // 0x00000000
        private uint Unknown_24h; // 0x00000000
        private uint Unknown_28h; // 0x00000000
        private uint Unknown_2Ch; // 0x00000000
        private uint Unknown_30h; // 0x00000000
        private uint Unknown_34h; // 0x00000000
        private uint Unknown_38h; // 0x00000000
        private uint Unknown_3Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_8h = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }
    }
}
