// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Drawables
{
    public interface IDrawableDictionary
    {
        IDrawableList Drawables { get; set; }

        uint GetHash(int index);
    }
}
