// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public struct BoneIdMapping : IResourceStruct<BoneIdMapping>
    {
        public ushort Id;
        public ushort Index;

        public BoneIdMapping ReverseEndianness()
        {
            return new BoneIdMapping()
            {
                Id = EndiannessExtensions.ReverseEndianness(Id),
                Index = EndiannessExtensions.ReverseEndianness(Index)
            };
        }
    }
}
