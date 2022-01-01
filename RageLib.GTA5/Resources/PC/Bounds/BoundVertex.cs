// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    public struct BoundVertex : IResourceStruct<BoundVertex>
    {
        public short X;
        public short Y;
        public short Z;

        public BoundVertex ReverseEndianness()
        {
            return new BoundVertex()
            {
                X = EndiannessExtensions.ReverseEndianness(X),
                Y = EndiannessExtensions.ReverseEndianness(Y),
                Z = EndiannessExtensions.ReverseEndianness(Z),
            };
        }
    }
}
