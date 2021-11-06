using RageLib.Data;
using RageLib.Resources;
using System;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    [Flags]
    public enum BoundFlags : uint
    {
        NONE = 0x0,
        UNKNOWN = 0x1,
        MAP_WEAPON = 0x2,
        MAP_DYNAMIC = 0x4,
        MAP_ANIMAL = 0x8,
        MAP_COVER = 0x10,
        MAP_VEHICLE = 0x20,
        VEHICLE_NOT_BVH = 0x40,
        VEHICLE_BVH = 0x80,
        VEHICLE_BOX = 0x100,
        PED = 0x200,
        RAGDOLL = 0x400,
        ANIMAL = 0x800,
        ANIMAL_RAGDOLL = 0x1000,
        OBJECT = 0x2000,
        OBJECT_ENV_CLOTH = 0x4000,
        PLANT = 0x8000,
        PROJECTILE = 0x10000,
        EXPLOSION = 0x20000,
        PICKUP = 0x40000,
        FOLIAGE = 0x80000,
        FORKLIFT_FORKS = 0x100000,
        TEST_WEAPON = 0x200000,
        TEST_CAMERA = 0x400000,
        TEST_AI = 0x800000,
        TEST_SCRIPT = 0x1000000,
        TEST_VEHICLE_WHEEL = 0x2000000,
        GLASS = 0x4000000,
        MAP_RIVER = 0x8000000,
        SMOKE = 0x10000000,
        UNSMASHED = 0x20000000,
        MAP_STAIRS = 0x40000000,
        MAP_DEEP_SURFACE = 0x80000000,
    }

    public struct BoundCompositeFlags : IResourceStruct<BoundCompositeFlags>
    {
        // structure data
        public BoundFlags Flags1;
        public BoundFlags Flags2;

        public BoundCompositeFlags ReverseEndianness()
        {
            return new BoundCompositeFlags()
            {
                Flags1 = (BoundFlags)EndiannessExtensions.ReverseEndianness((uint)Flags1),
                Flags2 = (BoundFlags)EndiannessExtensions.ReverseEndianness((uint)Flags2),
            };
        }
    }
}
