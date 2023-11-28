// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers
{
    /// <summary>
    /// Represents a texture dictionary.
    /// </summary>
    public interface ITextureDictionary
    {
        ITextureList Textures { get; set; }
    }
}