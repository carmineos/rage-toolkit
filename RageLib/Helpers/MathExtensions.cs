// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Numerics;

namespace RageLib.Helpers;

public static class MathHelpers
{
    public static bool WithinEpsilon(Vector4 v1, Vector4 v2, float epsilon = 0.001f)
    {
        return
            (MathF.Abs(v1.X - v2.X) < epsilon) &&
            (MathF.Abs(v1.Y - v2.Y) < epsilon) &&
            (MathF.Abs(v1.Z - v2.Z) < epsilon) &&
            (MathF.Abs(v1.W - v2.W) < epsilon);
    }

    public static bool WithinEpsilon(Matrix4x4 m1, Matrix4x4 m2, float epsilon = 0.001f)
    {
        return
            (MathF.Abs(m1.M11 - m2.M11) < epsilon) &&
            (MathF.Abs(m1.M12 - m2.M12) < epsilon) &&
            (MathF.Abs(m1.M13 - m2.M13) < epsilon) &&
            (MathF.Abs(m1.M14 - m2.M14) < epsilon) &&
            (MathF.Abs(m1.M21 - m2.M21) < epsilon) &&
            (MathF.Abs(m1.M22 - m2.M22) < epsilon) &&
            (MathF.Abs(m1.M23 - m2.M23) < epsilon) &&
            (MathF.Abs(m1.M24 - m2.M24) < epsilon) &&
            (MathF.Abs(m1.M31 - m2.M31) < epsilon) &&
            (MathF.Abs(m1.M32 - m2.M32) < epsilon) &&
            (MathF.Abs(m1.M33 - m2.M33) < epsilon) &&
            (MathF.Abs(m1.M34 - m2.M34) < epsilon) &&
            (MathF.Abs(m1.M41 - m2.M41) < epsilon) &&
            (MathF.Abs(m1.M42 - m2.M42) < epsilon) &&
            (MathF.Abs(m1.M43 - m2.M43) < epsilon) &&
            (MathF.Abs(m1.M44 - m2.M44) < epsilon);
    }
}
