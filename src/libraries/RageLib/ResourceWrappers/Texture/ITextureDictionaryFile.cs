// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers
{
    /// <summary>
    /// Representa a texture dictionary file.
    /// </summary>
    public interface ITextureDictionaryFile : IResourceFile
    {
        /// <summary>
        /// Gets the texture dictionary.
        /// </summary>
        ITextureDictionary TextureDictionary { get; }        
    }
}