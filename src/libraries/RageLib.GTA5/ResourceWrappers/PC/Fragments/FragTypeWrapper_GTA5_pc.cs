// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5.PC.Fragments;
using RageLib.ResourceWrappers.Fragments;

namespace RageLib.GTA5.ResourceWrappers.PC.Fragments
{
    public class FragTypeWrapper_GTA5_pc : IFragType
    {
        private FragType fragType;

        public IFragDrawable Drawable1
        {
            get
            {
                if (fragType.PrimaryDrawable != null)
                    return new FragDrawableWrapper_GTA5_pc(fragType.PrimaryDrawable);
                else
                    return null;
            }
        }

        public IFragDrawable Drawable2
        {
            get
            {
                if (fragType.ClothDrawable != null)
                    return new FragDrawableWrapper_GTA5_pc(fragType.ClothDrawable);
                else
                    return null;
            }
        }

        public FragTypeWrapper_GTA5_pc(FragType fragType)
        {
            this.fragType = fragType;
        }

        public void UpdateClass()
        {

        }
    }
}