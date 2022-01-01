// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Bounds
{
    /// <summary>
    /// Representa a drawable dictionary file.
    /// </summary>
    public interface IBoundDictionaryFile : IResourceFile
    {
        /// <summary>
        /// Gets the drawable dictionary.
        /// </summary>
        IBoundDictionary BoundDictionary { get; }
    }
}
