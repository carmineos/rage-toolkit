// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // sga::Shader ?
    public class Shader : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        public uint Hash;
        public uint Unknown_4h; //meta
        public ulong Unknown_8h_Pointer;
        public ulong Unknown_10h_Pointer;
        public ulong Unknown_18h_Pointer;
        public ulong Unknown_20h_Pointer;
        public ulong Unknown_28h;
        public ulong Unknown_30h;
        public byte Unknown_38h;
        public byte Unknown_39h;
        public ushort Unknown_3Ah;
        public uint Unknown_3Ch;
        public ulong Unknown_40h; 

        // reference data
        public ResourcePointerArray64<Struct_22>? Unknown_8h_Data { get; set; }


        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Hash = reader.ReadUInt32();
            Unknown_4h = reader.ReadUInt32();
            Unknown_8h_Pointer = reader.ReadUInt64();
            Unknown_10h_Pointer = reader.ReadUInt64();
            Unknown_18h_Pointer = reader.ReadUInt64();
            Unknown_20h_Pointer = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            Unknown_30h = reader.ReadUInt64();
            Unknown_38h = reader.ReadByte();
            Unknown_39h = reader.ReadByte();
            Unknown_3Ah = reader.ReadUInt16();
            Unknown_3Ch = reader.ReadUInt32();

            // read reference data
            Unknown_8h_Data = reader.ReadBlockAt<ResourcePointerArray64<Struct_22>>(Unknown_8h_Pointer, Unknown_3Ah);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data


            // write reference data
        }
    }
}
