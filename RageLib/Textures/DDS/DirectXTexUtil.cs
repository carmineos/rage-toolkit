// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Runtime.CompilerServices;

namespace RageLib.Textures.DDS;

internal class DirectXTexUtil
{
    // https://github.com/microsoft/DirectXTex/wiki/ComputePitch
    // https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DirectXTexUtil.cpp#L892
    public static void ComputePitch(DXGI_FORMAT fmt, uint width, uint height, out int rowPitch, out int slicePitch, CP_FLAGS flags = CP_FLAGS.CP_FLAGS_NONE)
    {
        ulong pitch = 0;
        ulong slice = 0;

        switch (fmt)
        {
            case DXGI_FORMAT.DXGI_FORMAT_BC1_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM:
                assert(IsCompressed(fmt));
                {
                    if ((flags & CP_FLAGS.CP_FLAGS_BAD_DXTN_TAILS) != 0)
                    {
                        nuint nbw = width >> 2;
                        nuint nbh = height >> 2;
                        pitch = Math.Max(1u, ((ulong)nbw) * 8u);
                        slice = Math.Max(1u, pitch * ((ulong)nbh));
                    }
                    else
                    {
                        ulong nbw = Math.Max(1u, (((ulong)width) + 3u) / 4u);
                        ulong nbh = Math.Max(1u, (((ulong)height) + 3u) / 4u);
                        pitch = nbw * 8u;
                        slice = pitch * nbh;
                    }
                }
                break;

            case DXGI_FORMAT.DXGI_FORMAT_BC2_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB:
                assert(IsCompressed(fmt));
                {
                    if ((flags & CP_FLAGS.CP_FLAGS_BAD_DXTN_TAILS) != 0)
                    {
                        nuint nbw = width >> 2;
                        nuint nbh = height >> 2;
                        pitch = Math.Max(1u, ((ulong)nbw) * 16u);
                        slice = Math.Max(1u, pitch * ((ulong)nbh));
                    }
                    else
                    {
                        ulong nbw = Math.Max(1u, (((ulong)width) + 3u) / 4u);
                        ulong nbh = Math.Max(1u, (((ulong)height) + 3u) / 4u);
                        pitch = nbw * 16u;
                        slice = pitch * nbh;
                    }
                }
                break;

            case DXGI_FORMAT.DXGI_FORMAT_R8G8_B8G8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_G8R8_G8B8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_YUY2:
                assert(IsPacked(fmt));
                pitch = ((((ulong)width) + 1u) >> 1) * 4u;
                slice = pitch * ((ulong)height);
                break;

            case DXGI_FORMAT.DXGI_FORMAT_Y210:
            case DXGI_FORMAT.DXGI_FORMAT_Y216:
                assert(IsPacked(fmt));
                pitch = ((((ulong)width) + 1u) >> 1) * 8u;
                slice = pitch * ((ulong)height);
                break;

            case DXGI_FORMAT.DXGI_FORMAT_NV12:
            case DXGI_FORMAT.DXGI_FORMAT_420_OPAQUE:
                if ((height % 2) != 0)
                {
                    // Requires a height alignment of 2.
                    //return E_INVALIDARG;
                    throw new ArgumentException();
                }
                assert(IsPlanar(fmt));
                pitch = ((((ulong)width) + 1u) >> 1) * 2u;
                slice = pitch * (((ulong)height) + ((((ulong)height) + 1u) >> 1));
                break;

            case DXGI_FORMAT.DXGI_FORMAT_P010:
            case DXGI_FORMAT.DXGI_FORMAT_P016:
                if ((height % 2) != 0)
                {
                    // Requires a height alignment of 2.
                    // return E_INVALIDARG;
                    throw new ArgumentException();
                }
                break;

                //#if (__cplusplus >= 201703L)
                //            [[fallthrough]];
                //#elif defined(__clang__)
                //            [[clang::fallthrough]];
                //#elif defined(_MSC_VER)
                //            __fallthrough;
                //#endif

            case DXGI_FORMAT.XBOX_DXGI_FORMAT_D16_UNORM_S8_UINT:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R16_UNORM_X8_TYPELESS:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_X16_TYPELESS_G8_UINT:
                assert(IsPlanar(fmt));
                pitch = ((((ulong)width) + 1u) >> 1) * 4u;
                slice = pitch * (((ulong)height) + ((((ulong)height) + 1u) >> 1));
                break;

            case DXGI_FORMAT.DXGI_FORMAT_NV11:
                assert(IsPlanar(fmt));
                pitch = ((((ulong)width) + 3u) >> 2) * 4u;
                slice = pitch * ((ulong)height) * 2u;
                break;

            case DXGI_FORMAT.WIN10_DXGI_FORMAT_P208:
                assert(IsPlanar(fmt));
                pitch = ((((ulong)width) + 1u) >> 1) * 2u;
                slice = pitch * ((ulong)height) * 2u;
                break;

            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V208:
                if ((height % 2) != 0)
                {
                    // Requires a height alignment of 2.
                    // return E_INVALIDARG;
                    throw new ArgumentException();
                }
                assert(IsPlanar(fmt));
                pitch = ((ulong)width);
                slice = pitch * (((ulong)height) + (((((ulong)height) + 1u) >> 1) * 2u));
                break;

            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V408:
                assert(IsPlanar(fmt));
                pitch = ((ulong)width);
                slice = pitch * (((ulong)height) + (((ulong)height >> 1) * 4u));
                break;
            default:
                assert(!IsCompressed(fmt) && !IsPacked(fmt) && !IsPlanar(fmt));
                {
                    nuint bpp;

                    if ((flags & CP_FLAGS.CP_FLAGS_24BPP) != 0)
                        bpp = 24;
                    else if ((flags & CP_FLAGS.CP_FLAGS_16BPP) != 0)
                        bpp = 16;
                    else if ((flags & CP_FLAGS.CP_FLAGS_8BPP) != 0)
                        bpp = 8;
                    else
                        bpp = BitsPerPixel(fmt);

                    if (bpp == 0)
                        // return E_INVALIDARG;
                        throw new ArgumentException();

                    if ((flags & (CP_FLAGS.CP_FLAGS_LEGACY_DWORD | CP_FLAGS.CP_FLAGS_PARAGRAPH | CP_FLAGS.CP_FLAGS_YMM | CP_FLAGS.CP_FLAGS_ZMM | CP_FLAGS.CP_FLAGS_PAGE4K)) != 0)
                    {
                        if ((flags & CP_FLAGS.CP_FLAGS_PAGE4K) != 0)
                        {
                            pitch = ((((ulong)width) * bpp + 32767u) / 32768u) * 4096u;
                            slice = pitch * ((ulong)height);
                        }
                        else if ((flags & CP_FLAGS.CP_FLAGS_ZMM) != 0)
                        {
                            pitch = ((((ulong)width) * bpp + 511u) / 512u) * 64u;
                            slice = pitch * ((ulong)height);
                        }
                        else if ((flags & CP_FLAGS.CP_FLAGS_YMM) != 0)
                        {
                            pitch = ((((ulong)width) * bpp + 255u) / 256u) * 32u;
                            slice = pitch * ((ulong)height);
                        }
                        else if ((flags & CP_FLAGS.CP_FLAGS_PARAGRAPH) != 0)
                        {
                            pitch = ((((ulong)width) * bpp + 127u) / 128u) * 16u;
                            slice = pitch * ((ulong)height);
                        }
                        else // DWORD alignment
                        {
                            // Special computation for some incorrectly created DDS files based on
                            // legacy DirectDraw assumptions about pitch alignment
                            pitch = ((((ulong)width) * bpp + 31u) / 32u) * sizeof(uint);
                            slice = pitch * ((ulong)height);
                        }
                    }
                    else
                    {
                        // Default byte alignment
                        pitch = (((ulong)width) * bpp + 7u) / 8u;
                        slice = pitch * (ulong)height;
                    }
                }
                break;
        }

        //#if defined(_M_IX86) || defined(_M_ARM) || defined(_M_HYBRID_X86_ARM64)
        //    static_assert(sizeof(size_t) == 4, "Not a 32-bit platform!");
        //    if (pitch > UINT32_MAX || slice > UINT32_MAX)
        //    {
        //        rowPitch = slicePitch = 0;
        //        return HRESULT_E_ARITHMETIC_OVERFLOW;
        //    }
        //#else
        //        static_assert(sizeof(size_t) == 8, "Not a 64-bit platform!");
        //#endif

        rowPitch = (int)pitch;
        slicePitch = (int)slice;

        return;
    }

    private static bool IsPacked(DXGI_FORMAT fmt)
    {
        switch (fmt)
        {
            case DXGI_FORMAT.DXGI_FORMAT_R8G8_B8G8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_G8R8_G8B8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_YUY2: // 4:2:2 8-bit
            case DXGI_FORMAT.DXGI_FORMAT_Y210: // 4:2:2 10-bit
            case DXGI_FORMAT.DXGI_FORMAT_Y216: // 4:2:2 16-bit
                return true;

            default:
                return false;
        }
    }

    public static int ComputeScanlines(DXGI_FORMAT fmt, int height)
    {
        switch (fmt)
        {
            case DXGI_FORMAT.DXGI_FORMAT_BC1_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB:
                assert(IsCompressed(fmt));
                return Math.Max(1, (height + 3) / 4);

            case DXGI_FORMAT.DXGI_FORMAT_NV11:
            case DXGI_FORMAT.WIN10_DXGI_FORMAT_P208:
                assert(IsPlanar(fmt));
                return height * 2;

            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V208:
                assert(IsPlanar(fmt));
                return height + (((height + 1) >> 1) * 2);

            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V408:
                assert(IsPlanar(fmt));
                return height + ((height >> 1) * 4);

            case DXGI_FORMAT.DXGI_FORMAT_NV12:
            case DXGI_FORMAT.DXGI_FORMAT_P010:
            case DXGI_FORMAT.DXGI_FORMAT_P016:
            case DXGI_FORMAT.DXGI_FORMAT_420_OPAQUE:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_D16_UNORM_S8_UINT:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R16_UNORM_X8_TYPELESS:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_X16_TYPELESS_G8_UINT:
                assert(IsPlanar(fmt));
                return height + ((height + 1) >> 1);

            default:
                assert(IsValid(fmt));
                assert(!IsCompressed(fmt) && !IsPlanar(fmt));
                return height;
        }
    }

    private static void assert(bool v)
    {
        if (!v) 
            throw new Exception();
    }

    // https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DirectXTex.inl
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValid(DXGI_FORMAT fmt)
    {
        return (uint)fmt >= 1 && (uint)fmt <= 190;
    }

    // https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DirectXTexUtil.cpp
    private static bool IsPlanar(DXGI_FORMAT fmt)
    {
        switch (fmt)
        {
            case DXGI_FORMAT.DXGI_FORMAT_NV12:      // 4:2:0 8-bit
            case DXGI_FORMAT.DXGI_FORMAT_P010:      // 4:2:0 10-bit
            case DXGI_FORMAT.DXGI_FORMAT_P016:      // 4:2:0 16-bit
            case DXGI_FORMAT.DXGI_FORMAT_420_OPAQUE:// 4:2:0 8-bit
            case DXGI_FORMAT.DXGI_FORMAT_NV11:      // 4:1:1 8-bit

            case DXGI_FORMAT.WIN10_DXGI_FORMAT_P208: // 4:2:2 8-bit
            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V208: // 4:4:0 8-bit
            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V408: // 4:4:4 8-bit
                                         // These are JPEG Hardware decode formats (DXGI 1.4)

            case DXGI_FORMAT.XBOX_DXGI_FORMAT_D16_UNORM_S8_UINT:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R16_UNORM_X8_TYPELESS:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_X16_TYPELESS_G8_UINT:
                // These are Xbox One platform specific types
                return true;

            default:
                return false;
        }
    }

    private static bool IsCompressed(DXGI_FORMAT fmt)
    {
        switch (fmt)
        {
            case DXGI_FORMAT.DXGI_FORMAT_BC1_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB:
                return true;

            default:
                return false;
        }
    }

    public static nuint BitsPerPixel(DXGI_FORMAT fmt)
    {
        switch (fmt)
        {
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_SINT:
                return 128;

            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_SINT:
                return 96;

            case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G32_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32G8X24_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_D32_FLOAT_S8X24_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_X32_TYPELESS_G8X24_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_Y416:
            case DXGI_FORMAT.DXGI_FORMAT_Y210:
            case DXGI_FORMAT.DXGI_FORMAT_Y216:
                return 64;

            case DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R11G11B10_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16G16_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_D32_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R32_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_R32_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R32_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_R24G8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_D24_UNORM_S8_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R24_UNORM_X8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_X24_TYPELESS_G8_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R9G9B9E5_SHAREDEXP:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8_B8G8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_G8R8_G8B8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_AYUV:
            case DXGI_FORMAT.DXGI_FORMAT_Y410:
            case DXGI_FORMAT.DXGI_FORMAT_YUY2:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R10G10B10_7E3_A2_FLOAT:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R10G10B10_6E4_A2_FLOAT:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R10G10B10_SNORM_A2_UNORM:
                return 32;

            case DXGI_FORMAT.DXGI_FORMAT_P010:
            case DXGI_FORMAT.DXGI_FORMAT_P016:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_D16_UNORM_S8_UINT:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R16_UNORM_X8_TYPELESS:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_X16_TYPELESS_G8_UINT:
            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V408:
                return 24;

            case DXGI_FORMAT.DXGI_FORMAT_R8G8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R8G8_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_R16_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R16_FLOAT:
            case DXGI_FORMAT.DXGI_FORMAT_D16_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R16_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R16_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_B5G6R5_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_A8P8:
            case DXGI_FORMAT.DXGI_FORMAT_B4G4R4A4_UNORM:
            case DXGI_FORMAT.WIN10_DXGI_FORMAT_P208:
            case DXGI_FORMAT.WIN10_DXGI_FORMAT_V208:
                return 16;

            case DXGI_FORMAT.DXGI_FORMAT_NV12:
            case DXGI_FORMAT.DXGI_FORMAT_420_OPAQUE:
            case DXGI_FORMAT.DXGI_FORMAT_NV11:
                return 12;

            case DXGI_FORMAT.DXGI_FORMAT_R8_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_R8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R8_UINT:
            case DXGI_FORMAT.DXGI_FORMAT_R8_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_R8_SINT:
            case DXGI_FORMAT.DXGI_FORMAT_A8_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_AI44:
            case DXGI_FORMAT.DXGI_FORMAT_IA44:
            case DXGI_FORMAT.DXGI_FORMAT_P8:
            case DXGI_FORMAT.XBOX_DXGI_FORMAT_R4G4_UNORM:
                return 8;

            case DXGI_FORMAT.DXGI_FORMAT_R1_UNORM:
                return 1;

            case DXGI_FORMAT.DXGI_FORMAT_BC1_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_TYPELESS:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM:
            case DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM:
                return 4;

            default:
                return 0;
        }
    }
}

[Flags]
public enum CP_FLAGS : uint
{
    // Normal operation
    CP_FLAGS_NONE = 0x0,

    // Assume pitch is DWORD aligned instead of BYTE aligned
    CP_FLAGS_LEGACY_DWORD = 0x1,

    // Assume pitch is 16-byte aligned instead of BYTE aligned
    CP_FLAGS_PARAGRAPH = 0x2,

    // Assume pitch is 32-byte aligned instead of BYTE aligned
    CP_FLAGS_YMM = 0x4,

    // Assume pitch is 64-byte aligned instead of BYTE aligned
    CP_FLAGS_ZMM = 0x8,

    // Assume pitch is 4096-byte aligned instead of BYTE aligned
    CP_FLAGS_PAGE4K = 0x200,

    // BC formats with malformed mipchain blocks smaller than 4x4
    CP_FLAGS_BAD_DXTN_TAILS = 0x1000,

    // Override with a legacy 24 bits-per-pixel format size
    CP_FLAGS_24BPP = 0x10000,

    // Override with a legacy 16 bits-per-pixel format size
    CP_FLAGS_16BPP = 0x20000,

    // Override with a legacy 8 bits-per-pixel format size
    CP_FLAGS_8BPP = 0x40000,
};
