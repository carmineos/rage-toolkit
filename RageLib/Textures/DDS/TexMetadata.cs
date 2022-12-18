// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Runtime.InteropServices;

namespace RageLib.Textures.DDS;

//https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DirectXTex.h#L163
[StructLayout(LayoutKind.Sequential)]
public struct TexMetadata
{
    public nuint width;
    public nuint height;     // Should be 1 for 1D textures
    public nuint depth;      // Should be 1 for 1D or 2D textures
    public nuint arraySize;  // For cubemap, this is a multiple of 6
    public nuint mipLevels;
    public uint miscFlags;
    public uint miscFlags2;
    public DXGI_FORMAT format;
    public TEX_DIMENSION dimension;
}

public enum TEX_DIMENSION
// Subset here matches D3D10_RESOURCE_DIMENSION and D3D11_RESOURCE_DIMENSION
{
    TEX_DIMENSION_TEXTURE1D = 2,
    TEX_DIMENSION_TEXTURE2D = 3,
    TEX_DIMENSION_TEXTURE3D = 4,
};
