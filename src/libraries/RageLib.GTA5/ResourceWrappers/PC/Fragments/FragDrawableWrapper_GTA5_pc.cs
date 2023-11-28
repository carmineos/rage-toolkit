// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.ResourceWrappers.PC.Drawables;
using RageLib.Resources.GTA5.PC.Fragments;
using RageLib.ResourceWrappers.Drawables;
using RageLib.ResourceWrappers.Fragments;

namespace RageLib.GTA5.ResourceWrappers.PC.Fragments
{
    public class FragDrawableWrapper_GTA5_pc : IFragDrawable
    {
        private FragDrawable fragDrawable;

        public FragDrawableWrapper_GTA5_pc(FragDrawable fragDrawable)
        {
            this.fragDrawable = fragDrawable;
        }

        public string Name
        {
            get
            {
                return (string)fragDrawable.Name;
            }
        }

        public IShaderGroup ShaderGroup
        {
            get
            {
                if (fragDrawable.ShaderGroup != null)
                    return new ShaderGroupWrapper_GTA5_pc(fragDrawable.ShaderGroup);
                else
                    return null;
            }
        }
    }
}
