// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public struct JointRotationLimit : IResourceStruct<JointRotationLimit>
    {
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
        public Vector4 ZeroRotationEulers; // 0.0; 0.0; 0.0;
        public Vector4 TwistAxis; // 1.0; 0.0; 0.0
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

        public JointRotationLimit ReverseEndianness()
        {
            return new JointRotationLimit()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                BoneId = EndiannessExtensions.ReverseEndianness(BoneId),
                Unknown_Ah = EndiannessExtensions.ReverseEndianness(Unknown_Ah),
                NumControlPoints = EndiannessExtensions.ReverseEndianness(NumControlPoints),
                JointDOFs = EndiannessExtensions.ReverseEndianness(JointDOFs),
                Unknown_14h = EndiannessExtensions.ReverseEndianness(Unknown_14h),
                Unknown_18h = EndiannessExtensions.ReverseEndianness(Unknown_18h),
                Unknown_1Ch = EndiannessExtensions.ReverseEndianness(Unknown_1Ch),
                Unknown_20h = EndiannessExtensions.ReverseEndianness(Unknown_20h),
                Unknown_24h = EndiannessExtensions.ReverseEndianness(Unknown_24h),
                Unknown_28h = EndiannessExtensions.ReverseEndianness(Unknown_28h),
                Unknown_2Ch = EndiannessExtensions.ReverseEndianness(Unknown_2Ch),
                ZeroRotationEulers = EndiannessExtensions.ReverseEndianness(ZeroRotationEulers),
                TwistAxis = EndiannessExtensions.ReverseEndianness(TwistAxis),
                TwistLimitMin = EndiannessExtensions.ReverseEndianness(TwistLimitMin),
                TwistLimitMax = EndiannessExtensions.ReverseEndianness(TwistLimitMax),
                SoftLimitScale = EndiannessExtensions.ReverseEndianness(SoftLimitScale),
                Min = EndiannessExtensions.ReverseEndianness(Min),
                Max = EndiannessExtensions.ReverseEndianness(Max),
                Unknown_74h = EndiannessExtensions.ReverseEndianness(Unknown_74h),
                Unknown_80h = EndiannessExtensions.ReverseEndianness(Unknown_80h),
                Unknown_8Ch = EndiannessExtensions.ReverseEndianness(Unknown_8Ch),
                Unknown_98h = EndiannessExtensions.ReverseEndianness(Unknown_98h),
                Unknown_A4h = EndiannessExtensions.ReverseEndianness(Unknown_A4h),
                Unknown_B0h = EndiannessExtensions.ReverseEndianness(Unknown_B0h),
                UseTwistLimits = EndiannessExtensions.ReverseEndianness(UseTwistLimits),
                UseEulerAngles = EndiannessExtensions.ReverseEndianness(UseEulerAngles),
                UsePerControlTwistLimits = EndiannessExtensions.ReverseEndianness(UsePerControlTwistLimits),
                Unknown_BFh = EndiannessExtensions.ReverseEndianness(Unknown_BFh),
            };
        }
    }
}
