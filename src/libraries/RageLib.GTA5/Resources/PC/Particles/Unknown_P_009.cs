// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public struct Unknown_P_009 : IResourceStruct<Unknown_P_009>
    {
        // structure data
        public Vector4 Unknown_0h;
        public Vector4 Unknown_10h;

        public Unknown_P_009 ReverseEndianness()
        {
            return new Unknown_P_009()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
            };
        }
    }
}
