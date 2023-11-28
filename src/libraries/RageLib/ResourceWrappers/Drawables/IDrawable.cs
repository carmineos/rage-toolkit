// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.ResourceWrappers.Drawables
{
    /// <summary>
    /// Represents a drawable list.
    /// </summary>
    public interface IDrawableList : IList<IDrawable>
    {



    }

    /// <summary>
    /// Represents a drawable.
    /// </summary>
    public interface IDrawable
    {

        string Name { get; }
        IShaderGroup ShaderGroup { get; }

    }
}