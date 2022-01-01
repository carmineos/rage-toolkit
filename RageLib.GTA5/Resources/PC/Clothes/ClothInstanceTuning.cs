// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // pgBase
    // clothInstanceTuning
    public class ClothInstanceTuning : PgBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        public float RotationRate;
        public float AngleThreshold;
        public ulong Unknown_18h; // 0x0000000000000000
        public Vector4 ExtraForce;
        public ClothTuneFlags Flags;
        public float Weight;
        public float DistanceThreshold;
        public byte PinVert;
        public byte NonPinVert0;
        public byte NonPinVert1;
        public byte Unknown_3Eh; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.RotationRate = reader.ReadSingle();
            this.AngleThreshold = reader.ReadSingle();
            this.Unknown_18h = reader.ReadUInt64();
            this.ExtraForce = reader.ReadVector4();
            this.Flags = (ClothTuneFlags)reader.ReadUInt32();
            this.Weight = reader.ReadSingle();
            this.DistanceThreshold = reader.ReadSingle();
            this.PinVert = reader.ReadByte();
            this.NonPinVert0 = reader.ReadByte();
            this.NonPinVert1 = reader.ReadByte();
            this.Unknown_3Eh = reader.ReadByte();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.RotationRate);
            writer.Write(this.AngleThreshold);
            writer.Write(this.Unknown_18h);
            writer.Write(this.ExtraForce);
            writer.Write((uint)this.Flags);
            writer.Write(this.Weight);
            writer.Write(this.DistanceThreshold);
            writer.Write(this.PinVert);
            writer.Write(this.NonPinVert0);
            writer.Write(this.NonPinVert1);
            writer.Write(this.Unknown_3Eh);
        }
    }

    // rage__clothInstanceTuning__enCLOTH_TUNE_FLAGS
    public enum ClothTuneFlags : uint
    {
        _0x02A90554 = 0,
        _0x4752DAFA = 1,
        _0xEAC5F797 = 2,
        _0xBADE1BDA = 3,
        _0xFBF0F5B2 = 4,
        _0xD734BB7C = 5,
        _0x00F9E049 = 6,
        _0xFE291880 = 7,
        _0x2844A250 = 8,
        _0xA9AE6C72 = 9,
        _0xF10143B9 = 10,
    };
}
