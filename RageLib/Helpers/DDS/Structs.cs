using RageLib.ResourceWrappers;
using System;

namespace RageLib.Helpers.DDS
{
    public struct ImageStruct
    {
        public int Width;
        public int Height;
        public int Format;
        public int MipMapLevels;
        public byte[] Data;

        public static ImageStruct GetImageStruct(ITexture texture)
        {
            return new ImageStruct()
            {
                Width = texture.Width,
                Height = texture.Height,
                Format = (int)texture.Format,
                MipMapLevels = texture.MipMapLevels,
                Data = texture.GetTextureData(),
            };
        }

        public int GetRowPitch()
        {
            throw new NotImplementedException();
        }

        public int GetSlicePitch()
        {
            throw new NotImplementedException();
        }
    }

    public struct Image
    {
        public nuint width;
        public nuint height;
        public DXGI_FORMAT format;
        public nuint rowPitch;
        public nuint slicePitch;
        public IntPtr pixels;
    }

    public struct TexMetadata
    {
        public nuint width;
        public nuint height;
        public nuint depth;
        public nuint arraySize;
        public nuint mipLevels;
        public uint miscFlags;
        public uint miscFlags2;
        public DXGI_FORMAT format;
        public TEX_DIMENSION dimension;
    }
}
