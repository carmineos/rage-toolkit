/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Compression;
using RageLib.ResourceWrappers;
using System;

namespace RageLib.Helpers.DDS
{
    public class DDSIO
    {
        private static ImageStruct ReadDDS(string fileName)
        {
            throw new NotImplementedException();
        }

        private static void WriteDDS(string fileName, ImageStruct image)
        {
            byte[] buffer = new byte[image.Data.Length];
            image.Data.AsSpan().CopyTo(buffer);

            TexMetadata meta;
            meta.width = (nuint)image.Width;
            meta.height = (nuint)image.Height;
            meta.depth = 1;
            meta.arraySize = 1;
            meta.mipLevels = (nuint)image.MipMapLevels;
            meta.miscFlags = 0;
            meta.miscFlags2 = 0;
            meta.format = (DXGI_FORMAT)image.Format;
            meta.dimension = TEX_DIMENSION.TEX_DIMENSION_TEXTURE2D;

            Image[] images = new Image[image.MipMapLevels];

            int div = 1;
            int add = 0;
            for (int i = 0; i < image.MipMapLevels; i++)
            {
                images[i].width = (nuint)(image.Width / div);
                images[i].height = (nuint)(image.Height / div);
                images[i].format = (DXGI_FORMAT)image.Format;
                images[i].pixels = IntPtr.Add(images[i].pixels, add);

                DirectX.ComputePitch(images[i].format, images[i].width, images[i].height, out images[i].rowPitch, out images[i].slicePitch, 0);

                add += (int)images[i].slicePitch;
                div *= 2;
            }

            // TODO: DirectX.SaveToDDSFile
        }

        public static void LoadTextureData(ITexture texture, string fileName)
        {
            ImageStruct img = DDSIO.ReadDDS(fileName);
    
            switch ((DXGI_FORMAT)img.Format)
            {
                // compressed
                case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch() / 4, (TextureFormat)D3DFORMAT.D3DFMT_DXT1);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch() / 4, (TextureFormat)D3DFORMAT.D3DFMT_DXT3);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch() / 4, (TextureFormat)D3DFORMAT.D3DFMT_DXT5);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch() / 4, (TextureFormat)D3DFORMAT.D3DFMT_ATI1);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch() / 4, (TextureFormat)D3DFORMAT.D3DFMT_ATI2);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch() / 4, (TextureFormat)D3DFORMAT.D3DFMT_BC7);
                    texture.SetTextureData(img.Data);
                    break;
    
    
    
                // uncompressed
                case DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch(), (TextureFormat)D3DFORMAT.D3DFMT_A1R5G5B5);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_A8_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch(), (TextureFormat)D3DFORMAT.D3DFMT_A8);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch(), (TextureFormat)D3DFORMAT.D3DFMT_A8B8G8R8);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_R8_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch(), (TextureFormat)D3DFORMAT.D3DFMT_L8);
                    texture.SetTextureData(img.Data);
                    break;
    
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM:
                    texture.Reset(img.Width, img.Height, img.MipMapLevels, img.GetRowPitch(), (TextureFormat)D3DFORMAT.D3DFMT_A8R8G8B8);
                    texture.SetTextureData(img.Data);
                    break;
    
                default:
                    throw new Exception("unsupported format");
            }
        }
    
        public static void SaveTextureData(ITexture texture, string fileName)
        {
            var data = texture.GetTextureData();

            var format = (D3DFORMAT)texture.Format switch
            {
                // compressed
                D3DFORMAT.D3DFMT_DXT1 => DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM,
                D3DFORMAT.D3DFMT_DXT3 => DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM,
                D3DFORMAT.D3DFMT_DXT5 => DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM,
                D3DFORMAT.D3DFMT_ATI1 => DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM,
                D3DFORMAT.D3DFMT_ATI2 => DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM,
                D3DFORMAT.D3DFMT_BC7 => DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM,
                
                // uncompressed
                D3DFORMAT.D3DFMT_A1R5G5B5 => DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM,
                D3DFORMAT.D3DFMT_A8 => DXGI_FORMAT.DXGI_FORMAT_A8_UNORM,
                D3DFORMAT.D3DFMT_A8B8G8R8 => DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM,
                D3DFORMAT.D3DFMT_L8 => DXGI_FORMAT.DXGI_FORMAT_R8_UNORM,
                D3DFORMAT.D3DFMT_A8R8G8B8 => DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
                _ => DXGI_FORMAT.DXGI_FORMAT_UNKNOWN,
            };

            ImageStruct img = new ImageStruct
            {
                Data = data,
                Width = texture.Width,
                Height = texture.Height,
                MipMapLevels = texture.MipMapLevels,
                Format = (int)format
            };

            DDSIO.WriteDDS(fileName, img);
        }
    }
}
