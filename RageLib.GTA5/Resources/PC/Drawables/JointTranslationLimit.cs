// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public struct JointTranslationLimit : IResourceStruct<JointTranslationLimit>
    {
        // structure data
        private ulong Unknown_0h; // 0x0000000000000000
        public uint BoneId;
        private uint Unknown_Ch; // 0x00000000
        private ulong Unknown_10h; // 0x0000000000000000
        private ulong Unknown_18h; // 0x0000000000000000
        public Vector4 LimitMin;
        public Vector4 LimitMax;

        public JointTranslationLimit ReverseEndianness()
        {
            return new JointTranslationLimit()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                BoneId = EndiannessExtensions.ReverseEndianness(BoneId),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
                Unknown_18h = EndiannessExtensions.ReverseEndianness(Unknown_18h),
                LimitMin = EndiannessExtensions.ReverseEndianness(LimitMin),
                LimitMax = EndiannessExtensions.ReverseEndianness(LimitMax),
            };
        }
    }
}
