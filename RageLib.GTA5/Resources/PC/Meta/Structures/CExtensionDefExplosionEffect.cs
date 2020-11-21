using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefExplosionEffect : CExtensionDef
    {
        public int BlockLengthStructure => 0x50;

        public Vector4 offsetRotation;
        public CharPointer explosionName;
        public int boneTag;
        public int explosionTag;
        public int explosionType;
        public uint flags;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
