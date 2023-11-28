// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Drawables
{
    public interface IShaderGroup
    {
        ITextureDictionary TextureDictionary { get; }
        IShaderList Shaders { get; }
    }
}
