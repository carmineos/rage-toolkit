// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageLib.Textures.DDS;

internal class DirectXTexUtil
{
    // https://github.com/microsoft/DirectXTex/wiki/ComputePitch
    // https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DirectXTexUtil.cpp#L892
    public static void ComputePitch(DXGI_FORMAT format, int width, int height, out int rowPitch, out int slicePitch, CP_FLAGS flags = CP_FLAGS.CP_FLAGS_NONE)
    {
        throw new NotImplementedException();
    }

    public static int ComputeScanlines(DXGI_FORMAT fmt, int height)
    {
        throw new NotImplementedException();
    }

    public static int BitsPerPixel(DXGI_FORMAT fmt)
    {
        throw new NotImplementedException();
    }
}

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
