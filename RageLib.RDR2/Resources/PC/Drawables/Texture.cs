// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // rage::sga::Texture
    // VFT = 0x00000001409100B0
    public class Texture : DatBase64
    {
        public override long BlockLength => 0xB0;

        // structure data
        public uint Unknown_08h;
        public uint Unknown_0Ch;
        public uint Unknown_10h;
        public uint Unknown_14h;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public uint Unknown_20h;
        public uint Unknown_24h;
        public ulong NamePointer;
        public ulong Unknown_30h_Pointer;
        public ulong Unknown_38h_Pointer; // Graphics Pointer?
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong Unknown_58h; // 0x0000000000000000
        public ulong Unknown_60h; // 0x0000000000000000
        public ShaderResourceView Unknown_68h; // 0x0000000140910080	Embedded block 
        public ulong Unknown_A8h; // 0x0000000000000000

        // reference data
        public string_r? Name { get; set; }
        public ShaderResourceView? Unknown_30h_Data { get; set; }

        public Texture()
        {
            Unknown_68h = new ShaderResourceView();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_08h = reader.ReadUInt32();
            Unknown_0Ch = reader.ReadUInt32();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            NamePointer = reader.ReadUInt64();
            Unknown_30h_Pointer = reader.ReadUInt64();
            Unknown_38h_Pointer = reader.ReadUInt64();
            Unknown_40h = reader.ReadUInt64();
            Unknown_48h = reader.ReadUInt64();
            Unknown_50h = reader.ReadUInt64();
            Unknown_58h = reader.ReadUInt64();
            Unknown_60h = reader.ReadUInt64();
            Unknown_68h = reader.ReadBlock<ShaderResourceView>();
            Unknown_A8h = reader.ReadUInt64();

            // read reference data
            Name = reader.ReadBlockAt<string_r>(NamePointer);
            Unknown_30h_Data = reader.ReadBlockAt<ShaderResourceView>(Unknown_30h_Pointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data


            // write reference data
        }
    }
}
