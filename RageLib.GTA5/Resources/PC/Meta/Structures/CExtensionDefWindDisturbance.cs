using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefWindDisturbance : CExtensionDef
    {
        public int BlockLengthStructure => 0x60;

        public Vector4 offsetRotation;
        public int disturbanceType;
        public int boneTag;
        public uint pad_38h;
        public uint pad_3Ch;
        public Vector4 size;
        public float strength;
        public int flags;
        public uint pad_58h;
        public uint pad_5Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
