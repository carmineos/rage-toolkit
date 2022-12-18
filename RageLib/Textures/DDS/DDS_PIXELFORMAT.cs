using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RageLib.Textures.DDS
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct DDS_PIXELFORMAT
    {
        [FieldOffset(0)]
        public uint dwSize;

        [FieldOffset(4)]
        public DDPF_Flags dwFlags;

        [FieldOffset(8)]
        public uint dwFourCC;

        [FieldOffset(12)]
        public uint dwRGBBitCount;

        [FieldOffset(16)]
        public uint dwRBitMask;

        [FieldOffset(20)]
        public uint dwGBitMask;

        [FieldOffset(24)]
        public uint dwBBitMask;

        [FieldOffset(28)]
        public uint dwABitMask;

        public DDS_PIXELFORMAT(
            DDPF_Flags flags,
            uint fourCC,
            uint rgbBitCount,
            uint rBitMask,
            uint gBitMask,
            uint bBitMask,
            uint aBitMask)
        {
            dwSize = 32;
            dwFlags = flags;
            dwFourCC = fourCC;
            dwRGBBitCount = rgbBitCount;
            dwRBitMask = rBitMask;
            dwGBitMask = gBitMask;
            dwBBitMask = bBitMask;
            dwABitMask = aBitMask;
        }
    }

    [Flags]
    public enum DDPF_Flags : uint
    {
        /// <summary>
        /// Texture contains alpha data; dwRGBAlphaBitMask contains valid data.     
        /// </summary>
        DDPF_ALPHAPIXELS = 0x1,

        /// <summary>
        /// Used in some older DDS files for alpha channel only uncompressed data (dwRGBBitCount contains the alpha channel bitcount; dwABitMask contains valid data)  
        /// </summary>
        DDPF_ALPHA = 0x2,

        /// <summary>
        /// Texture contains compressed RGB data; dwFourCC contains valid data.
        /// </summary>
        DDPF_FOURCC = 0x4,

        /// <summary>
        /// Texture contains uncompressed RGB data; dwRGBBitCount and the RGB masks(dwRBitMask, dwGBitMask, dwBBitMask) contain valid data.
        /// </summary>
        DDPF_RGB = 0x40,

        /// <summary>
        /// Used in some older DDS files for YUV uncompressed data(dwRGBBitCount contains the YUV bit count; dwRBitMask contains the Y mask, dwGBitMask contains the U mask, dwBBitMask contains the V mask)
        /// </summary>
        DDPF_YUV = 0x200,

        /// <summary>
        /// Used in some older DDS files for single channel color uncompressed data(dwRGBBitCount contains the luminance channel bit count; dwRBitMask contains the channel mask). Can be combined with DDPF_ALPHAPIXELS for a two channel DDS file.
        /// </summary>
        DDPF_LUMINANCE = 0x20000,

        DDPF_RGBA = DDPF_RGB | DDPF_ALPHAPIXELS,
        DDPF_LUMINANCEA = DDPF_LUMINANCE | DDPF_ALPHAPIXELS,
    }
}
