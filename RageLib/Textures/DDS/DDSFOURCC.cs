namespace RageLib.Textures.DDS
{
    public static class DDSFOURCC
    {
        public const uint DX10 = 'D' | 'X' << 8 | '1' << 16 | '0' << 24;
        public const uint DXT1 = 'D' | 'X' << 8 | 'T' << 16 | '1' << 24;
        public const uint DXT2 = 'D' | 'X' << 8 | 'T' << 16 | '2' << 24;
        public const uint DXT3 = 'D' | 'X' << 8 | 'T' << 16 | '3' << 24;
        public const uint DXT4 = 'D' | 'X' << 8 | 'T' << 16 | '4' << 24;
        public const uint DXT5 = 'D' | 'X' << 8 | 'T' << 16 | '5' << 24;
        public const uint ATI1 = 'A' | 'T' << 8 | 'I' << 16 | '1' << 24;
        public const uint ATI2 = 'A' | 'T' << 8 | 'I' << 16 | '2' << 24;
        public const uint MET1 = 'M' | 'E' << 8 | 'T' << 16 | '1' << 24;
        public const uint BC7  = 'B' | 'C' << 8 | '7' << 16 | ' ' << 24;
        public const uint UYVY = 'U' | 'Y' << 8 | 'V' << 16 | 'Y' << 24;
        public const uint YUY2 = 'Y' | 'U' << 8 | 'Y' << 16 | '2' << 24;
        public const uint RGBG = 'R' | 'G' << 8 | 'B' << 16 | 'G' << 24;
        public const uint GRGB = 'G' | 'R' << 8 | 'G' << 16 | 'B' << 24;
        public const uint BC4U = 'B' | 'C' << 8 | '4' << 16 | 'U' << 24;
        public const uint BC4S = 'B' | 'C' << 8 | '4' << 16 | 'S' << 24;
        public const uint BC5S = 'B' | 'C' << 8 | '5' << 16 | 'S' << 24;
    }

    public static class DDSPIXELFORMATS
    {
        //public static DDS_PIXELFORMAT DXGI_FORMAT_R16G16_UNORM = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 32, 0x0000FFFF, 0xFFFF0000, 0x00000000, 0x00000000);
        //public static DDS_PIXELFORMAT D3DFMT_G16R16 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 32, 0x0000FFFF, 0xFFFF0000, 0x00000000, 0x00000000);

        public readonly static DDS_PIXELFORMAT A8B8G8R8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 32, 0x000000FF, 0x0000FF00, 0x00FF0000, 0xFF000000);
        public readonly static DDS_PIXELFORMAT G16R16 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 32, 0x0000FFFF, 0xFFFF0000, 0x00000000, 0x00000000);
        public readonly static DDS_PIXELFORMAT A1R5G5B5 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 16, 0x00007C00, 0x000003E0, 0x0000001F, 0x00008000);
        public readonly static DDS_PIXELFORMAT R5G6B5 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 16, 0x0000F800, 0x000007E0, 0x0000001F, 0x00000000);
        public readonly static DDS_PIXELFORMAT A8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_ALPHA, 0, 8, 0x00000000, 0x00000000, 0x00000000, 0x000000FF);

        public readonly static DDS_PIXELFORMAT A8R8G8B8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 32, 0x00FF0000, 0x0000FF00, 0x000000FF, 0xFF000000);
        public readonly static DDS_PIXELFORMAT X8R8G8B8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 32, 0x00FF0000, 0x0000FF00, 0x000000FF, 0x00000000);
        public readonly static DDS_PIXELFORMAT X8B8G8R8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 32, 0x000000FF, 0x0000FF00, 0x00FF0000, 0x00000000);
        public readonly static DDS_PIXELFORMAT R8G8B8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 24, 0x000000FF, 0x0000FF00, 0x00FF0000, 0x00000000);
        public readonly static DDS_PIXELFORMAT X1R5G5B5 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 16, 0x00007C00, 0x000003E0, 0x0000001F, 0x00000000);
        public readonly static DDS_PIXELFORMAT A4R4G4B4 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 16, 0x00000F00, 0x000000F0, 0x0000000F, 0x0000F000);
        public readonly static DDS_PIXELFORMAT X4R4G4B4 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB, 0, 16, 0x00000F00, 0x000000F0, 0x0000000F, 0x00000000);
        public readonly static DDS_PIXELFORMAT A8R3G3B2 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 16, 0x000000E0, 0x0000001C, 0x00000003, 0xFF000000);
        public readonly static DDS_PIXELFORMAT A8L8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_LUMINANCEA, 0, 16, 0x000000FF, 0x00000000, 0x00000000, 0x0000FF00);
        public readonly static DDS_PIXELFORMAT L16 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_LUMINANCE, 0, 16, 0x0000FFFF, 0x00000000, 0x00000000, 0x00000000);
        public readonly static DDS_PIXELFORMAT L8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_LUMINANCE, 0, 8, 0x000000FF, 0x00000000, 0x00000000, 0x00000000);
        public readonly static DDS_PIXELFORMAT A4L4 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_LUMINANCEA, 0, 8, 0x0000000F, 0x00000000, 0x00000000, 0x000000F0);

        public readonly static DDS_PIXELFORMAT DXT1 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.DXT1, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT DXT2 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.DXT2, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT DXT3 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.DXT3, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT DXT4 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.DXT4, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT DXT5 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.DXT5, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT UYVY = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.UYVY, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT YUY2 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.YUY2, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT CxV8U8 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 117, 0, 0, 0, 0, 0);

        public readonly static DDS_PIXELFORMAT DX10 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.DX10, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT RGBG = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.RGBG, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT GRGB = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.GRGB, 0, 0, 0, 0, 0);

        // A robust DDS reader must be able to handle these legacy format codes. However, such a DDS reader should prefer to use the "DX10" header extension when it writes these format codes to avoid ambiguity.
        public readonly static DDS_PIXELFORMAT BC4U = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.BC4U, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT BC4S = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.BC4S, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT ATI2 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.ATI2, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT BC5S = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, DDSFOURCC.BC5S, 0, 0, 0, 0, 0);

        public readonly static DDS_PIXELFORMAT A16B16G16R16 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 36, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT Q16W16V16U16 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 110, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT R16F = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 111, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT G16R16F = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 112, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT A16B16G16R16F = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 113, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT R32F = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 114, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT G32R32F = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 115, 0, 0, 0, 0, 0);
        public readonly static DDS_PIXELFORMAT A32B32G32R32F = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_FOURCC, 116, 0, 0, 0, 0, 0);

        // Because of some long-standing issues in common implementations of DDS readers and writers, the most robust way to write out 10:10:10:2-type data is to use the "DX10" header extension with the DXGI_FORMAT code "24" (that is, the DXGI_FORMAT_R10G10B10A2_UNORM value).
        // D3DFMT_A2R10G10B10 data should be converted to 10:10:10:2-type data before being written out as a DXGI_FORMAT_R10G10B10A2_UNORM format DDS file.
        public readonly static DDS_PIXELFORMAT A2B10G10R10 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 32, 0x000003FF, 0x000FFC00, 0x3FF00000, 0x00000000);
        //public static DDS_PIXELFORMAT DXGI_FORMAT_R10G10B10A2_UNORM = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGBA, 0, 32, 0x000003FF, 0x000FFC00, 0x3FF00000, 0x00000000);
        //public static DDS_PIXELFORMAT D3DFMT_A2R10G10B10 = new DDS_PIXELFORMAT(DDPF_Flags.DDPF_RGB,  0, 32, 0x3FF00000, 0x000FFC00, 0x000003FF, 0xC0000000);

        public static DDS_PIXELFORMAT GetPixelFormat(DXGI_FORMAT dxgiFormat)
        {
            return dxgiFormat switch
            {
                DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM => A8R8G8B8,
                DXGI_FORMAT.DXGI_FORMAT_R16G16_UNORM => G16R16,
                DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM => A1R5G5B5,
                DXGI_FORMAT.DXGI_FORMAT_B5G6R5_UNORM => R5G6B5,
                //DXGI_FORMAT.DXGI_A8_UNORM => A8,

                DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM => DXT1,
                DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM => DXT3,
                DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM => DXT5,

                DXGI_FORMAT.DXGI_FORMAT_R8G8_B8G8_UNORM => RGBG,
                DXGI_FORMAT.DXGI_FORMAT_G8R8_G8B8_UNORM => GRGB,

                // Legacy
                DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM => BC4U,
                DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM => BC4S,
                DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM => ATI2,
                DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM => BC5S,

                DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UNORM => A16B16G16R16,
                DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_SNORM => Q16W16V16U16,
                DXGI_FORMAT.DXGI_FORMAT_R16_FLOAT => R16F,
                DXGI_FORMAT.DXGI_FORMAT_R16G16_FLOAT => G16R16F,
                DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_FLOAT => A16B16G16R16F,
                DXGI_FORMAT.DXGI_FORMAT_R32_FLOAT => R32F,
                DXGI_FORMAT.DXGI_FORMAT_R32G32_FLOAT => G32R32F,
                DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT => A32B32G32R32F,

                //DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_UNORM => A2B10G10R10,

                _ => DX10,
            };
        }

        public static DDS_PIXELFORMAT GetPixelFormat(D3DFORMAT d3dFormat)
        {
            return d3dFormat switch
            {
                D3DFORMAT.D3DFMT_A8B8G8R8 => A8B8G8R8,
                D3DFORMAT.D3DFMT_G16R16 => G16R16,
                D3DFORMAT.D3DFMT_A1R5G5B5 => R5G6B5,
                D3DFORMAT.D3DFMT_A8 => A8,

                D3DFORMAT.D3DFMT_A8R8G8B8 => A8R8G8B8,
                D3DFORMAT.D3DFMT_X8R8G8B8 => X8R8G8B8,
                D3DFORMAT.D3DFMT_X8B8G8R8 => X8B8G8R8,
                D3DFORMAT.D3DFMT_R8G8B8 => R8G8B8,
                D3DFORMAT.D3DFMT_X1R5G5B5 => X1R5G5B5,
                D3DFORMAT.D3DFMT_A4R4G4B4 => A4R4G4B4,
                D3DFORMAT.D3DFMT_X4R4G4B4 => X4R4G4B4,
                D3DFORMAT.D3DFMT_A8R3G3B2 => A8R3G3B2,
                D3DFORMAT.D3DFMT_A8L8 => A8L8,
                D3DFORMAT.D3DFMT_L16 => L16,
                D3DFORMAT.D3DFMT_L8 => L8,
                D3DFORMAT.D3DFMT_A4L4 => A4L4,

                D3DFORMAT.D3DFMT_DXT1 => DXT1,
                D3DFORMAT.D3DFMT_DXT2 => DXT2,
                D3DFORMAT.D3DFMT_DXT3 => DXT3,
                D3DFORMAT.D3DFMT_DXT4 => DXT4,
                D3DFORMAT.D3DFMT_DXT5 => DXT5,
                D3DFORMAT.D3DFMT_UYVY => UYVY,
                D3DFORMAT.D3DFMT_YUY2 => YUY2,
                D3DFORMAT.D3DFMT_CxV8U8 => CxV8U8,

                D3DFORMAT.D3DFMT_R8G8_B8G8 => RGBG,
                D3DFORMAT.D3DFMT_G8R8_G8B8 => GRGB,

                // Legacy
                D3DFORMAT.D3DFMT_A16B16G16R16 => A16B16G16R16,
                D3DFORMAT.D3DFMT_Q16W16V16U16 => Q16W16V16U16,
                D3DFORMAT.D3DFMT_R16F => R16F,
                D3DFORMAT.D3DFMT_G16R16F => G16R16F,
                D3DFORMAT.D3DFMT_A16B16G16R16F => A16B16G16R16F,
                D3DFORMAT.D3DFMT_R32F => R32F,
                D3DFORMAT.D3DFMT_G32R32F => G32R32F,
                D3DFORMAT.D3DFMT_A32B32G32R32F => A32B32G32R32F,

                //D3DFORMAT.D3DFMT_A2B10G10R10 => A2B10G10R10,

                _ => default,
            };
        }
    }
}
