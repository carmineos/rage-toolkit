using RageLib.Resources.GTA5.PC.Meta.Types;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CCarGen : IMetaStructure
    {
        public int StructureSize => 0x50;

        public uint pad_0h;
        public uint pad_4h;
        public uint pad_8h;
        public uint pad_Ch;
        public Vector3 position;
        public float pad_1Ch;
        public float orientX;
        public float orientY;
        public float perpendicularLength;
        public MetaHash carModel;
        public uint flags;
        public int bodyColorRemap1;
        public int bodyColorRemap2;
        public int bodyColorRemap3;
        public int bodyColorRemap4;
        public MetaHash popGroup;
        public sbyte livery;
        public byte pad_49h;
        public ushort pad_4Ah;
        public uint pad_4Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
