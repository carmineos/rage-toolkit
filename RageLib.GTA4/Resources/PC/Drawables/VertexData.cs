// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public struct VertexDataType1
    {
        public Vector3 Position;
        public Vector3 Normal;
        public uint Color;
        public Vector2 Texcoord;
    }

    public struct VertexDataType2
    {
        public Vector3 Position;
        public Vector3 Normal;
        public uint Color;
        public Vector2 TexCoord0;
        public Vector4 Tangent;
    }
}
