using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefParticleEffect : CExtensionDef
    {
        public int BlockLengthStructure => 0x60;

        public Vector4 offsetRotation;
        public CharPointer fxName;
        public int fxType;
        public int boneTag;
        public float scale;
        public int probability;
        public int flags;
        public uint color;
        public uint pad_58h;
        public uint pad_5Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
