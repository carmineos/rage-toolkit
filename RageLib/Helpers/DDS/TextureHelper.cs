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

using RageLib.Helpers.DDS;
using RageLib.ResourceWrappers;

namespace RageLib.Compression
{
    public static class TextureHelper
    {
        public static byte[] GetRgbaImage(ITexture model, int mipMapLevel = 0)
        {
            var x = model.GetTextureData(mipMapLevel);
            var format = (D3DFORMAT)model.Format;
            return format switch
            {
                // compressed formats...
                D3DFORMAT.D3DFMT_DXT1 => TextureCompressionHelper.DecompressBC1(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_DXT3 => TextureCompressionHelper.DecompressBC2(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_DXT5 => TextureCompressionHelper.DecompressBC3(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_ATI1 => TextureCompressionHelper.DecompressBC4(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_ATI2 => TextureCompressionHelper.DecompressBC5(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_BC7 => TextureCompressionHelper.DecompressBC7(x, model.Width, model.Height),
                
                // uncompressed formats...
                D3DFORMAT.D3DFMT_A8 => TextureConvert.MakeRGBAFromA8(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_L8 => TextureConvert.MakeARGBFromL8(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_A1R5G5B5 => TextureConvert.MakeARGBFromA1R5G5B5(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_A8B8G8R8 => TextureConvert.MakeRGBAFromA8B8G8R8(x, model.Width, model.Height),
                D3DFORMAT.D3DFMT_A8R8G8B8 => TextureConvert.MakeRGBAFromA8R8G8B8(x, model.Width, model.Height),
                _ => throw new System.Exception($"Unsupported {nameof(D3DFORMAT)}: {format}"),
            };
        }
    }

    public static class TextureCompressionHelper
    {
        public static byte[] DecompressBC1(byte[] data, int width, int height)
        {
            return ImageCompressor.Decompress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM);
        }

        public static byte[] DecompressBC2(byte[] data, int width, int height)
        {
            return ImageCompressor.Decompress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM);
        }

        public static byte[] DecompressBC3(byte[] data, int width, int height)
        {
            return ImageCompressor.Decompress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM);
        }

        public static byte[] DecompressBC4(byte[] data, int width, int height)
        {
            return ImageCompressor.Decompress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM);
        }

        public static byte[] DecompressBC5(byte[] data, int width, int height)
        {
            return ImageCompressor.Decompress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM);
        }

        public static byte[] DecompressBC7(byte[] data, int width, int height)
        {
            return ImageCompressor.Decompress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM);
        }

        public static byte[] CompressBC1(byte[] data, int width, int height)
        {
            return ImageCompressor.Compress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM);
        }

        public static byte[] CompressBC2(byte[] data, int width, int height)
        {
            return ImageCompressor.Compress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM);
        }

        public static byte[] CompressBC3(byte[] data, int width, int height)
        {
            return ImageCompressor.Compress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM);
        }

        public static byte[] CompressBC4(byte[] data, int width, int height)
        {
            return ImageCompressor.Compress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM);
        }

        public static byte[] CompressBC5(byte[] data, int width, int height)
        {
            return ImageCompressor.Compress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM);
        }

        public static byte[] CompressBC7(byte[] data, int width, int height)
        {
            return ImageCompressor.Compress(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM);
        }
    }

    public static class TextureConvert
    {
        public static byte[] MakeRGBAFromA8(byte[] data, int width, int height)
        {
            return ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_A8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        public static byte[] MakeRGBAFromA8B8G8R8(byte[] data, int width, int height)
        {
            // return ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
            return data;
        }

        public static byte[] MakeRGBAFromA8R8G8B8(byte[] data, int width, int height)
        {
            return ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        public static byte[] MakeARGBFromL8(byte[] data, int width, int height)
        {
            return ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_R8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        public static byte[] MakeARGBFromA1R5G5B5(byte[] data, int width, int height)
        {
            return ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }
    }
}