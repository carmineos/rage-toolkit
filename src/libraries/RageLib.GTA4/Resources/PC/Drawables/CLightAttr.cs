// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class CLightAttr : ResourceSystemBlock
    {
        public override long BlockLength => 0x68;

        // structure data
        public Vector3 Position;
        public Vector3 Direction;
        public Vector3 Tangent;
        public uint Color;
        public float LodDistance;
        public float VolumeIntensity;
        public float VolumeSize;
        public float LightAttenuationEnd;
        public float LightIntensity;
        public float CoronaSize;
        public float LightHotspot;
        public float LightFalloff;
        public uint Flags;
        public uint CoronaHash;
        public uint LumHash;
        public byte Flashiness;
        private ushort Unknown_55h;
        public byte LightType;
        public float CoronaHDRMultiplier;
        public float LightFadeDistance;
        public float ShadowFadeDistance;
        public ushort BoneID;
        private ushort Unknown_66h;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Position = reader.ReadVector3();
            Direction = reader.ReadVector3();
            Tangent = reader.ReadVector3();
            Color = reader.ReadUInt32();
            LodDistance = reader.ReadSingle();
            VolumeIntensity = reader.ReadSingle();
            VolumeSize = reader.ReadSingle();
            LightAttenuationEnd = reader.ReadSingle();
            LightIntensity = reader.ReadSingle();
            CoronaSize = reader.ReadSingle();
            LightHotspot = reader.ReadSingle();
            LightFalloff = reader.ReadSingle();
            Flags = reader.ReadUInt32();
            CoronaHash = reader.ReadUInt32();
            LumHash = reader.ReadUInt32();
            Flashiness = reader.ReadByte();
            Unknown_55h = reader.ReadUInt16();
            LightType = reader.ReadByte();
            CoronaHDRMultiplier = reader.ReadSingle();
            LightFadeDistance = reader.ReadSingle();
            ShadowFadeDistance = reader.ReadSingle();
            BoneID = reader.ReadUInt16();
            Unknown_66h = reader.ReadUInt16();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(Position);
            writer.Write(Direction);
            writer.Write(Tangent);
            writer.Write(Color);
            writer.Write(LodDistance);
            writer.Write(VolumeIntensity);
            writer.Write(VolumeSize);
            writer.Write(LightAttenuationEnd);
            writer.Write(LightIntensity);
            writer.Write(CoronaSize);
            writer.Write(LightHotspot);
            writer.Write(LightFalloff);
            writer.Write(Flags);
            writer.Write(CoronaHash);
            writer.Write(LumHash);
            writer.Write(Flashiness);
            writer.Write(Unknown_55h);
            writer.Write(LightType);
            writer.Write(CoronaHDRMultiplier);
            writer.Write(LightFadeDistance);
            writer.Write(ShadowFadeDistance);
            writer.Write(BoneID);
            writer.Write(Unknown_66h);
        }
    }
}
