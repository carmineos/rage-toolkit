using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    // datBase
    public class CLightAttrDef : IMetaStructure
    {
        public int StructureSize => 0xA0;

        public uint pad_0h;
        public uint pad_4h;
        public Vector3 posn;
        public ArrayOfBytes3 colour;
        public byte flashiness;
        public float intensity;
        public uint flags;
        public short boneTag;
        public byte lightType;
        public byte groupId;
        public uint timeFlags;
        public float falloff;
        public float falloffExponent;
        public Vector4 cullingPlane;
        public byte shadowBlur;
        public byte padding1;
        public short padding2;
        public uint padding3;
        public float volIntensity;
        public float volSizeScale;
        public ArrayOfBytes3 volOuterColour;
        public byte lightHash;
        public float volOuterIntensity;
        public float coronaSize;
        public float volOuterExponent;
        public byte lightFadeDistance;
        public byte shadowFadeDistance;
        public byte specularFadeDistance;
        public byte volumetricFadeDistance;
        public float shadowNearClip;
        public float coronaIntensity;
        public float coronaZBias;
        public Vector3 direction;
        public Vector3 tangent;
        public float coneInnerAngle;
        public float coneOuterAngle;
        public Vector3 extents;
        public uint projectedTextureKey;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
