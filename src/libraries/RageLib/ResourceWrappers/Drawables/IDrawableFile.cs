// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Drawables
{
    /// <summary>
    /// Representa a drawable file.
    /// </summary>
    public interface IDrawableFile : IResourceFile
    {
        /// <summary>
        /// Gets the drawable.
        /// </summary>
        IDrawable Drawable { get; }        
    }
}