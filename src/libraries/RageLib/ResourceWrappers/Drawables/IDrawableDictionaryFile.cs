// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Drawables
{
    /// <summary>
    /// Representa a drawable dictionary file.
    /// </summary>
    public interface IDrawableDictionaryFile : IResourceFile
    {
        /// <summary>
        /// Gets the drawable dictionary.
        /// </summary>
        IDrawableDictionary DrawableDictionary { get; }        
    }
}