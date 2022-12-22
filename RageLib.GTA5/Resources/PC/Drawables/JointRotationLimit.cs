// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class JointRotationLimit : ResourceSystemBlock
    {
        public override long BlockLength => 0xC0;

        // structure data
        public ulong Unknown_0h; // 0x0000000000000000
        public ushort BoneId;
        public ushort Unknown_Ah;
        public uint NumControlPoints; // 0x00000001
        public uint JointDOFs; // 0x00000003
        public uint Unknown_14h; // 0x00000000
        public uint Unknown_18h; // 0x00000000
        public uint Unknown_1Ch; // 0x00000000
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public float Unknown_2Ch; // 1.0
        public Vector3 ZeroRotationEulers; // 0.0; 0.0; 0.0
        public uint Unknown_3Ch; // 0x00000000
        public Vector3 TwistAxis; // 1.0; 0.0; 0.0
        public uint Unknown_4Ch; // 0x00000000
        public float TwistLimitMin; // -pi
        public float TwistLimitMax; // pi
        public float SoftLimitScale; // 1.0
        public Vector3 Min; // in rad
        public Vector3 Max; // in rad
        public Vector3 Unknown_74h; // pi; -pi; pi; MaxSwing; MinTwist; MaxTwist;
        public Vector3 Unknown_80h; // pi; -pi; pi; MaxSwing; MinTwist; MaxTwist;
        public Vector3 Unknown_8Ch; // pi; -pi; pi; MaxSwing; MinTwist; MaxTwist;
        public Vector3 Unknown_98h; // pi; -pi; pi; MaxSwing; MinTwist; MaxTwist;
        public Vector3 Unknown_A4h; // pi; -pi; pi; MaxSwing; MinTwist; MaxTwist;
        public Vector3 Unknown_B0h; // pi; -pi; pi; MaxSwing; MinTwist; MaxTwist;
        public byte UseTwistLimits;
        public byte UseEulerAngles; // 0x01
        public byte UsePerControlTwistLimits;
        public byte Unknown_BFh;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt64();
            this.BoneId = reader.ReadUInt16();
            this.Unknown_Ah = reader.ReadUInt16();
            this.NumControlPoints = reader.ReadUInt32();
            this.JointDOFs = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadSingle();
            this.ZeroRotationEulers = reader.ReadVector3();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.TwistAxis = reader.ReadVector3();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.TwistLimitMin = reader.ReadSingle();
            this.TwistLimitMax = reader.ReadSingle();
            this.SoftLimitScale = reader.ReadSingle();
            this.Min = reader.ReadVector3();
            this.Max = reader.ReadVector3();
            this.Unknown_74h = reader.ReadVector3();
            this.Unknown_80h = reader.ReadVector3();
            this.Unknown_8Ch = reader.ReadVector3();
            this.Unknown_98h = reader.ReadVector3();
            this.Unknown_A4h = reader.ReadVector3();
            this.Unknown_B0h = reader.ReadVector3();
            this.UseTwistLimits = reader.ReadByte();
            this.UseEulerAngles = reader.ReadByte();
            this.UsePerControlTwistLimits = reader.ReadByte();
            this.Unknown_BFh = reader.ReadByte();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.BoneId);
            writer.Write(this.Unknown_Ah);
            writer.Write(this.NumControlPoints);
            writer.Write(this.JointDOFs);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.ZeroRotationEulers);
            writer.Write(this.Unknown_3Ch);
            writer.Write(this.TwistAxis);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.TwistLimitMin);
            writer.Write(this.TwistLimitMax);
            writer.Write(this.SoftLimitScale);
            writer.Write(this.Min);
            writer.Write(this.Max);
            writer.Write(this.Unknown_74h);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_8Ch);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_A4h);
            writer.Write(this.Unknown_B0h);
            writer.Write(this.UseTwistLimits);
            writer.Write(this.UseEulerAngles);
            writer.Write(this.UsePerControlTwistLimits);
            writer.Write(this.Unknown_BFh);
        }
    }
}
