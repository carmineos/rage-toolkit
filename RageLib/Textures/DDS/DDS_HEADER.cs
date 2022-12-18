using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RageLib.Textures.DDS
{
    // https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dds-header
    [StructLayout(LayoutKind.Explicit, Size = 124)]
    public unsafe struct DDS_HEADER
    {
        [FieldOffset(0)]
        public uint dwSize; // 124

        [FieldOffset(4)]
        public DDS_FLAGS dwFlags;

        [FieldOffset(8)]
        public uint dwHeight;

        [FieldOffset(12)]
        public uint dwWidth;

        [FieldOffset(16)]
        public uint dwPitchOrLinearSize;

        [FieldOffset(20)]
        public uint dwDepth;

        [FieldOffset(24)]
        public uint dwMipMapCount;

        [FieldOffset(28)]
        public fixed uint dwReserved1[11]; // Unused

        [FieldOffset(72)]
        public DDS_PIXELFORMAT ddspf;

        [FieldOffset(104)]
        public DDSCAPS dwCaps;

        [FieldOffset(108)]
        public DDSCAPS2 dwCaps2;

        [FieldOffset(112)]
        public uint dwCaps3; // Unused

        [FieldOffset(116)]
        public uint dwCaps4; // Unused

        [FieldOffset(120)]
        public uint dwReserved2; // Unused

        public bool IsCubeMap => (dwCaps2 & DDSCAPS2.DDSCAPS2_CUBEMAP) == DDSCAPS2.DDSCAPS2_CUBEMAP;

        public bool IsVolumeTexture => (dwCaps2 & DDSCAPS2.DDSCAPS2_VOLUME) == DDSCAPS2.DDSCAPS2_VOLUME;

        public bool IsCompressed => (dwFlags & DDS_FLAGS.DDSD_LINEARSIZE) == DDS_FLAGS.DDSD_LINEARSIZE;

        public bool HasMipMaps => (dwCaps & DDSCAPS.DDSCAPS_MIPMAP) == DDSCAPS.DDSCAPS_MIPMAP &&
                (dwFlags & DDS_FLAGS.DDSD_MIPMAPCOUNT) == DDS_FLAGS.DDSD_MIPMAPCOUNT;
    }

    [Flags]
    public enum DDS_FLAGS : uint
    {
        /// <summary>
        /// Required in every .dds file. 	
        /// </summary>
        DDSD_CAPS = 0x1,

        /// <summary>
        /// Required in every .dds file. 	
        /// </summary>
        DDSD_HEIGHT = 0x2,

        /// <summary>
        /// Required in every .dds file. 	
        /// </summary>
        DDSD_WIDTH = 0x4,

        /// <summary>
        /// Required when pitch is provided for an uncompressed texture.  	
        /// </summary>
        DDSD_PITCH = 0x8,

        /// <summary>
        /// Required in every .dds file. 	
        /// </summary>
        DDSD_PIXELFORMAT = 0x1000,

        /// <summary>
        /// Required in a mipmapped texture. 	
        /// </summary>
        DDSD_MIPMAPCOUNT = 0x20000,

        /// <summary>
        /// Required when pitch is provided for a compressed texture. 
        /// </summary>
        DDSD_LINEARSIZE = 0x80000,

        /// <summary>
        /// Required in a depth texture.
        /// </summary>
        DDSD_DEPTH = 0x800000,

        // Dds.h
        DDS_HEADER_FLAGS_TEXTURE = DDSD_CAPS | DDSD_HEIGHT | DDSD_WIDTH | DDSD_PIXELFORMAT,
        DDS_HEADER_FLAGS_MIPMAP = DDSD_MIPMAPCOUNT,
        DDS_HEADER_FLAGS_VOLUME = DDSD_DEPTH,
        DDS_HEADER_FLAGS_PITCH = DDSD_PITCH,
        DDS_HEADER_FLAGS_LINEARSIZE = DDSD_LINEARSIZE,
    }

    [Flags]
    public enum DDSCAPS : uint
    {
        /// <summary>
        /// Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).
        /// </summary>
        DDSCAPS_COMPLEX = 0x8,

        /// <summary>
        /// Required
        /// </summary>
        DDSCAPS_TEXTURE = 0x1000,

        /// <summary>
        /// Optional; should be used for a mipmap.
        /// </summary>
        DDSCAPS_MIPMAP = 0x400000,

        // Dds.h
        DDS_SURFACE_FLAGS_MIPMAP = DDSCAPS_COMPLEX | DDSCAPS_MIPMAP,
        DDS_SURFACE_FLAGS_TEXTURE = DDSCAPS_TEXTURE,
        DDS_SURFACE_FLAGS_CUBEMAP = DDSCAPS_COMPLEX,
    }

    [Flags]
    public enum DDSCAPS2 : uint
    {
        /// <summary>
        /// Required for a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP = 0x200,

        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP_POSITIVEX = 0x400,

        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP_NEGATIVEX = 0x800,

        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP_POSITIVEY = 0x1000,

        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP_NEGATIVEY = 0x2000,

        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP_POSITIVEZ = 0x4000,

        /// <summary>
        /// Required when these surfaces are stored in a cube map.
        /// </summary>
        DDSCAPS2_CUBEMAP_NEGATIVEZ = 0x8000,

        /// <summary>
        /// Required for a volume texture.
        /// </summary>
        DDSCAPS2_VOLUME = 0x200000,

        // Dds.h
        DDS_CUBEMAP_POSITIVEX = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_POSITIVEX,
        DDS_CUBEMAP_NEGATIVEX = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_NEGATIVEX,
        DDS_CUBEMAP_POSITIVEY = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_POSITIVEY,
        DDS_CUBEMAP_NEGATIVEY = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_NEGATIVEY,
        DDS_CUBEMAP_POSITIVEZ = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_POSITIVEZ,
        DDS_CUBEMAP_NEGATIVEZ = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_NEGATIVEZ,
        DDS_CUBEMAP_ALLFACES = DDS_CUBEMAP_POSITIVEX | DDS_CUBEMAP_NEGATIVEX | DDS_CUBEMAP_POSITIVEY | DDS_CUBEMAP_NEGATIVEY | DDS_CUBEMAP_POSITIVEZ | DDSCAPS2_CUBEMAP_NEGATIVEZ,
        DDS_FLAGS_VOLUME = DDSCAPS2_VOLUME,
    }
}
