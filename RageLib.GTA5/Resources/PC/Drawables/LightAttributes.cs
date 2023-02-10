// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // CLightAttr
    public struct LightAttributes : IResourceStruct<LightAttributes>
    {
        private uint Unknown_0h; // 0x00000000
        private uint Unknown_4h; // 0x00000000
        public Vector3 Position;
        private uint Unknown_14h; // 0x00000000
        public byte ColorR;
        public byte ColorG;
        public byte ColorB;
        public byte Flashiness;
        public float Intensity;
        public uint Flags;
        public ushort BoneId;
        public byte Type;
        public byte GroupId;
        public uint TimeFlags;
        public float Falloff;
        public float FalloffExponent;
        public Vector3 CullingPlaneNormal;
        public float CullingPlaneOffset;
        public byte ShadowBlur;
        private byte Unknown_45h;
        private ushort Unknown_46h;
        private uint Unknown_48h; // 0x00000000
        public float VolumeIntensity;
        public float VolumeSizeScale;
        public byte VolumeOuterColorR;
        public byte VolumeOuterColorG;
        public byte VolumeOuterColorB;
        public byte LightHash;
        public float VolumeOuterIntensity;
        public float CoronaSize;
        public float VolumeOuterExponent;
        public byte LightFadeDistance;
        public byte ShadowFadeDistance;
        public byte SpecularFadeDistance;
        public byte VolumetricFadeDistance;
        public float ShadowNearClip;
        public float CoronaIntensity;
        public float CoronaZBias;
        public Vector3 Direction;
        public Vector3 Tangent;
        public float ConeInnerAngle;
        public float ConeOuterAngle;
        public Vector3 Extent;
        public uint ProjectedTextureHash;
        private uint Unknown_A4h; // 0x00000000

        public LightAttributes ReverseEndianness()
        {
            return new LightAttributes()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Position = EndiannessExtensions.ReverseEndianness(Position),
                Unknown_14h = EndiannessExtensions.ReverseEndianness(Unknown_14h),
                ColorR = EndiannessExtensions.ReverseEndianness(ColorR),
                ColorG = EndiannessExtensions.ReverseEndianness(ColorG),
                ColorB = EndiannessExtensions.ReverseEndianness(ColorB),
                Flashiness = EndiannessExtensions.ReverseEndianness(Flashiness),
                Intensity = EndiannessExtensions.ReverseEndianness(Intensity),
                Flags = EndiannessExtensions.ReverseEndianness(Flags),
                BoneId = EndiannessExtensions.ReverseEndianness(BoneId),
                Type = EndiannessExtensions.ReverseEndianness(Type),
                GroupId = EndiannessExtensions.ReverseEndianness(GroupId),
                TimeFlags = EndiannessExtensions.ReverseEndianness(TimeFlags),
                Falloff = EndiannessExtensions.ReverseEndianness(Falloff),
                FalloffExponent = EndiannessExtensions.ReverseEndianness(FalloffExponent),
                CullingPlaneNormal = EndiannessExtensions.ReverseEndianness(CullingPlaneNormal),
                CullingPlaneOffset = EndiannessExtensions.ReverseEndianness(CullingPlaneOffset),
                ShadowBlur = EndiannessExtensions.ReverseEndianness(ShadowBlur),
                Unknown_45h = EndiannessExtensions.ReverseEndianness(Unknown_45h),
                Unknown_46h = EndiannessExtensions.ReverseEndianness(Unknown_46h),
                Unknown_48h = EndiannessExtensions.ReverseEndianness(Unknown_48h),
                VolumeIntensity = EndiannessExtensions.ReverseEndianness(VolumeIntensity),
                VolumeSizeScale = EndiannessExtensions.ReverseEndianness(VolumeSizeScale),
                VolumeOuterColorR = EndiannessExtensions.ReverseEndianness(VolumeOuterColorR),
                VolumeOuterColorG = EndiannessExtensions.ReverseEndianness(VolumeOuterColorG),
                VolumeOuterColorB = EndiannessExtensions.ReverseEndianness(VolumeOuterColorB),
                LightHash = EndiannessExtensions.ReverseEndianness(LightHash),
                VolumeOuterIntensity = EndiannessExtensions.ReverseEndianness(VolumeOuterIntensity),
                CoronaSize = EndiannessExtensions.ReverseEndianness(CoronaSize),
                VolumeOuterExponent = EndiannessExtensions.ReverseEndianness(VolumeOuterExponent),
                LightFadeDistance = EndiannessExtensions.ReverseEndianness(LightFadeDistance),
                ShadowFadeDistance = EndiannessExtensions.ReverseEndianness(ShadowFadeDistance),
                SpecularFadeDistance = EndiannessExtensions.ReverseEndianness(SpecularFadeDistance),
                VolumetricFadeDistance = EndiannessExtensions.ReverseEndianness(VolumetricFadeDistance),
                ShadowNearClip = EndiannessExtensions.ReverseEndianness(ShadowNearClip),
                CoronaIntensity = EndiannessExtensions.ReverseEndianness(CoronaIntensity),
                CoronaZBias = EndiannessExtensions.ReverseEndianness(CoronaZBias),
                Direction = EndiannessExtensions.ReverseEndianness(Direction),
                Tangent = EndiannessExtensions.ReverseEndianness(Tangent),
                ConeInnerAngle = EndiannessExtensions.ReverseEndianness(ConeInnerAngle),
                ConeOuterAngle = EndiannessExtensions.ReverseEndianness(ConeOuterAngle),
                Extent = EndiannessExtensions.ReverseEndianness(Extent),
                ProjectedTextureHash = EndiannessExtensions.ReverseEndianness(ProjectedTextureHash),
                Unknown_A4h = EndiannessExtensions.ReverseEndianness(Unknown_A4h),
            };
        }
    }
}
