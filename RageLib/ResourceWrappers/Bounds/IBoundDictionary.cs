// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Bounds
{
    public interface IBoundDictionary
    {
        IBoundList Bounds { get; set; }

        uint GetHash(int index);
    }
}
