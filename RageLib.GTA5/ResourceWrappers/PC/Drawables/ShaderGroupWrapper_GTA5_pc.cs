// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.ResourceWrappers;
using RageLib.ResourceWrappers.Drawables;
using RageLib.GTA5.ResourceWrappers.PC.Textures;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Drawables
{
    public class ShaderGroupWrapper_GTA5_pc : IShaderGroup
    {
        private ShaderGroup shaderGroup;

        public IShaderList Shaders
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITextureDictionary TextureDictionary
        {
            get
            {
                if (shaderGroup.TextureDictionary.Data != null)
                    return new TextureDictionaryWrapper_GTA5_pc(shaderGroup.TextureDictionary.Data);
                else
                    return null;
            }
        }

        public ShaderGroupWrapper_GTA5_pc(ShaderGroup shaderGroup)
        {
            this.shaderGroup = shaderGroup;
        }
    }
}