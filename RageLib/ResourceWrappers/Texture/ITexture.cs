// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.ResourceWrappers
{
    // D3DFORMAT
    public enum TextureFormat
    {
        D3DFMT_A8R8G8B8 = 21,
        D3DFMT_A1R5G5B5 = 25,
        D3DFMT_A8 = 28,
        D3DFMT_A8B8G8R8 = 32,
        D3DFMT_L8 = 50,

        // fourCC
        D3DFMT_DXT1 = 0x31545844,
        D3DFMT_DXT3 = 0x33545844,
        D3DFMT_DXT5 = 0x35545844,
        D3DFMT_ATI1 = 0x31495441,
        D3DFMT_ATI2 = 0x32495441,
        D3DFMT_BC7 = 0x20374342,

        UNKNOWN
    }

    /// <summary>
    /// Represents a texture list.
    /// </summary>
    public interface ITextureList : IList<ITexture>
    { }

    /// <summary>
    /// Represents a texture.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Gets or sets the name of the texture.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the number of mipmaps of the texture.
        /// </summary>
        int MipMapLevels { get; }

        /// <summary>
        /// Gets the compression format of the texture.
        /// </summary>
        TextureFormat Format { get; }

        int Stride { get; }

        /// <summary>
        /// Gets the texture data.
        /// </summary>
        byte[] GetTextureData();

        /// <summary>
        /// Gets the texture data of the specified mipMapLevel.
        /// </summary>
        byte[] GetTextureData(int mipMapLevel);

        /// <summary>
        /// Sets the texture data.
        /// </summary>
        void SetTextureData(byte[] data);

        /// <summary>
        /// Sets the texture data of the specified mipMapLevel.
        /// </summary>
        void SetTextureData(byte[] data, int mipMapLevel);

        /// <summary>
        /// Resets all the data of the texture.
        /// </summary>
        void Reset(
            int width,
            int height,
            int mipMapLevels,
            int stride,
            TextureFormat Format);        
    }
}