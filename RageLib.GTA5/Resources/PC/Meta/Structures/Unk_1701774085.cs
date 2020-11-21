using RageLib.Resources.GTA5.PC.Meta.Enums;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class Unk_1701774085 : IMetaStructure
    {
        public int StructureSize => 0x60;

        public CharPointer OwnerName;
        public Vector4 Rotation;
        public Vector3 Position;
        public float pad_2Ch;
        public Vector3 Normal;
        public float pad_3Ch;
        public float CapsuleRadius;
        public float CapsuleLen;
        public float CapsuleHalfHeight;
        public float CapsuleHalfWidth;
        public Unk_3044470860 Flags;
        public uint pad_54h;
        public uint pad_58h;
        public uint pad_5Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
