// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public struct LodDrawBucketMask : IResourceStruct<LodDrawBucketMask>
    {
        private uint data;

        public byte Flags
        {
            get => (byte)(data & 0xFFu);
            set => data &= 0xFFFFFF00u | value;
        }

        public byte Mask
        {
            get => (byte)((data >> 8) & 0xFFu);
            set => data &= 0xFFFF00FFu | ((uint)(value << 8));
        }

        public ushort Unknown_02h
        {
            get => (byte)((data >> 16) & 0xFFFFu);
            set => data &= 0x0000FFFFu | ((uint)(value << 16));
        }

        public LodDrawBucketMask ReverseEndianness()
        {
            return new LodDrawBucketMask()
            {
                data = EndiannessExtensions.ReverseEndianness(data),
            };
        }

        public LodDrawBucketMask(uint data)
        {
            this.data = data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(LodDrawBucketMask drawBucketMask) => drawBucketMask.data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator LodDrawBucketMask(uint data) => new LodDrawBucketMask(data);
    }
}
