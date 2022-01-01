// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    [Flags]
    public enum PhBoundMaterialFlags : ushort
    {
        Unknown_0 = 0x1,
        Unknown_1 = 0x2,
        Unknown_2 = 0x4,
        Unknown_3 = 0x8,
        Unknown_4 = 0x10,
        Unknown_5 = 0x20,
        Unknown_6 = 0x40,
        Unknown_7 = 0x80,
        Unknown_8 = 0x100,
        Unknown_9 = 0x200,
        Unknown_10 = 0x400,
        Unknown_11 = 0x800,
        Unknown_12 = 0x1000,
        Unknown_13 = 0x2000,
        Unknown_14 = 0x4000,
        Unknown_15 = 0x8000,
    }

    public struct PhBoundMaterial : IResourceStruct<PhBoundMaterial>
    {
        public byte MaterialId;
        public byte Unknown_01h;
        public PhBoundMaterialFlags Flags;

        public PhBoundMaterial ReverseEndianness()
        {
            return new PhBoundMaterial()
            {
                MaterialId = EndiannessExtensions.ReverseEndianness(MaterialId),
                Unknown_01h = EndiannessExtensions.ReverseEndianness(Unknown_01h),
                Flags = (PhBoundMaterialFlags)EndiannessExtensions.ReverseEndianness((ushort)Flags),
            };
        }
    }
}
