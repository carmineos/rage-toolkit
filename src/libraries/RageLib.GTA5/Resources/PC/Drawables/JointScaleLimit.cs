// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables;

public struct JointScaleLimit : IResourceStruct<JointScaleLimit>
{
    private ulong ALIGNMENT_PAD_00h; //0x0000000000000000
    private ulong ALIGNMENT_PAD_08h; //0x0000000000000000
    private ulong ALIGNMENT_PAD_10h; //0x0000000000000000
    private ulong ALIGNMENT_PAD_18h; //0x0000000000000000
    public Vector4 LimitMin;
    public Vector4 LimitMax;

    public JointScaleLimit ReverseEndianness()
    {
        return new JointScaleLimit()
        {
            LimitMin = EndiannessExtensions.ReverseEndianness(LimitMin),
            LimitMax = EndiannessExtensions.ReverseEndianness(LimitMax),
        };
    }
}
