// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Bounds
{
    /// <summary>
    /// Represents a bound file.
    /// </summary>
    public interface IBoundFile : IResourceFile
    {
        /// <summary>
        /// Gets the bound.
        /// </summary>
        IBound Bound { get; }
    }
}