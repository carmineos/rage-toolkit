// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // rage::sga::ShaderResourceView
    // VFT = 0x0000000140910080
    public class ShaderResourceView : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong VFT;
        public ulong Unknown_08h; // 0x0000000000000000
        public uint Unknown_10h;
        public uint Unknown_14h;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Unknown_38h; // 0x0000000000000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.VFT = reader.ReadUInt64();
            this.Unknown_08h = reader.ReadUInt64();
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(VFT);
            writer.Write(Unknown_08h);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_38h);
        }
    }
}
