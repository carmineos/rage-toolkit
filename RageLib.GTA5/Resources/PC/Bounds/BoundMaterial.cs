// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    public struct BoundMaterial : IResourceStruct<BoundMaterial>
    {
        // structure data
        public ulong Data;

        public byte Type
        {
            get => (byte)(Data & 0xFFu);
            set => Data &= 0xFFFFFFFFFFFFFF00u | value;
        }

        public byte ProceduralId
        {
            get => (byte)((Data >> 8) & 0xFFu);
            set => Data &= 0xFFFFFFFFFFFF00FFu | ((ulong)value << 8);
        }

        public byte RoomId
        {
            get => (byte)((Data >> 16) & 0x1Fu);
            set => Data &= 0xFFFFFFFFFFE0FFFFu | (((ulong)value & 0x1Fu) << 16);
        }

        public byte PedDensity
        {
            get => (byte)((Data >> 21) & 0x7u);
            set => Data &= 0xFFFFFFFFFF1FFFFFu | (((ulong)value & 0x7u) << 21);
        }

        public BoundMaterialFlags Flags
        {
            get => (BoundMaterialFlags)((Data >> 24) & 0xFFFF);
            set => Data &= 0xFFFFFF0000FFFFFFu | ((ulong)value << 24);
        }

        public byte MaterialColorIndex
        {
            get => (byte)((Data >> 40) & 0xFFu);
            set => Data &= 0xFFFF00FFFFFFFFFFu | ((ulong)value << 40);
        }

        private ushort Unknown
        {
            get => (ushort)((Data >> 48) & 0xFFFFu);
            set => Data &= 0x0000FFFFFFFFFFFFu | ((ulong)value << 48);
        }

        public BoundMaterial ReverseEndianness()
        {
            return new BoundMaterial()
            {
                Data = EndiannessExtensions.ReverseEndianness(Data)
            };
        }
    }

    [Flags]
    public enum BoundMaterialFlags : ushort
    {
        NONE = 0,
        FLAG_STAIRS = 0x1,
        FLAG_NOT_CLIMBABLE = 0x2,
        FLAG_SEE_THROUGH = 0x4,
        FLAG_SHOOT_THROUGH = 0x8,
        FLAG_NOT_COVER = 0x10,
        FLAG_WALKABLE_PATH = 0x20,
        FLAG_NO_CAM_COLLISION = 0x40,
        FLAG_SHOOT_THROUGH_FX = 0x80,
        FLAG_NO_DECAL = 0x100,
        FLAG_NO_NAVMESH = 0x200,
        FLAG_NO_RAGDOLL = 0x400,
        FLAG_VEHICLE_WHEEL = 0x800,
        FLAG_NO_PTFX = 0x1000,
        FLAG_TOO_STEEP_FOR_PLAYER = 0x2000,
        FLAG_NO_NETWORK_SPAWN = 0x4000,
        FLAG_NO_CAM_COLLISION_ALLOW_CLIPPING = 0x8000,
    }
}
