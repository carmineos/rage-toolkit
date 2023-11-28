// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // rage__characterClothController__BindingInfo
    public struct BindingInfo : IResourceStruct<BindingInfo>
    {
        // structure data
        public Vector4 Weights;
        public uint BlendIndex0;
        public uint BlendIndex1;
        public uint BlendIndex2;
        public uint BlendIndex3;

        public BindingInfo ReverseEndianness()
        {
            return new BindingInfo()
            {
                Weights = EndiannessExtensions.ReverseEndianness(Weights),
                BlendIndex0 = EndiannessExtensions.ReverseEndianness(BlendIndex0),
                BlendIndex1 = EndiannessExtensions.ReverseEndianness(BlendIndex1),
                BlendIndex2 = EndiannessExtensions.ReverseEndianness(BlendIndex2),
                BlendIndex3 = EndiannessExtensions.ReverseEndianness(BlendIndex3),
            };
        }
    }
}
