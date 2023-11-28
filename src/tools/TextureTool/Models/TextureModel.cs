// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Helpers;
using RageLib.ResourceWrappers;

namespace TextureTool.Models
{
    public class TextureModel
    {
        private ITexture texture;

        public ITexture Texture
        {
            get
            {
                return texture;
            }
        }

        public string Name
        {
            get
            {
                return texture.Name;
            }
            set
            {
                texture.Name = value;
            }
        }
        
        public TextureModel(ITexture texture)
        {
            this.texture = texture;
        }

        public void Import(string fileName)
        {
            try
            {
                // only DDS supported
                DDSIO.LoadTextureData(texture, fileName);
            }
            catch
            { }
        }

        public void Export(string fileName)
        {
            try
            {
                // only DDS supported
                DDSIO.SaveTextureData(texture, fileName);
            }
            catch
            { }
        }
    }
}
