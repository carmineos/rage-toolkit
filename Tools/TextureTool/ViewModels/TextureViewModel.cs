// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Compression;
using RageLib.ResourceWrappers;
using System.Windows.Media;
using TextureTool.Models;

namespace TextureTool.ViewModels
{
    public class TextureViewModel : BaseViewModel
    {
        private TextureModel model;

        public string Name
        {
            get
            {
                return model.Name;
            }
        }

        public string Size
        {
            get
            {
                return model.Texture.Width + "x" + model.Texture.Height;
            }
        }

        public int Levels
        {
            get
            {
                return model.Texture.MipMapLevels;
            }
        }

        public string Format
        {
            get
            {
                switch (model.Texture.Format)
                {
                    case TextureFormat.D3DFMT_DXT1: return "DXT1";
                    case TextureFormat.D3DFMT_DXT3: return "DXT3";
                    case TextureFormat.D3DFMT_DXT5: return "DXT5";
                    case TextureFormat.D3DFMT_ATI1: return "ATI1";
                    case TextureFormat.D3DFMT_ATI2: return "ATI2";
                    case TextureFormat.D3DFMT_BC7: return "BC7";
                    case TextureFormat.D3DFMT_A1R5G5B5: return "A1R5G5B5";
                    case TextureFormat.D3DFMT_A8: return "A8";
                    case TextureFormat.D3DFMT_L8: return "L8";
                    case TextureFormat.D3DFMT_A8B8G8R8: return "A8B8G8R8";
                    case TextureFormat.D3DFMT_A8R8G8B8: return "A8R8G8B8";
                    default: return "Unknown";
                }
            }
        }

        public ImageSource Image
        {
            get
            {
                var y = TextureHelper.GetRgbaImage(model.Texture, 0);
                return new RgbaBitmapSource(y, model.Texture.Width, model.Texture.Height);
            }
        }

        public TextureViewModel(TextureModel model)
        {
            this.model = model;
        }

        public TextureModel GetModel()
        {
            return model;
        }
    }
}