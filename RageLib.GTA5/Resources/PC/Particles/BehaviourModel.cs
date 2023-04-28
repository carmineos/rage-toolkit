// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxd_Model
    public class BehaviourModel : Behaviour
    {
        public override long BlockLength => 0x40;

        // structure data
        private ulong Unknown_10h; // 0x0000000000000000
        private ulong Unknown_18h; // 0x0000000000000000
        private ulong Unknown_20h; // 0x0000000000000000
        private ulong Unknown_28h; // 0x0000000000000000
        private uint Unknown_30h; // 0x00000000
        private uint Unknown_34h;
        private uint Unknown_38h;
        private uint Unknown_3Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
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
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }
    }
}
