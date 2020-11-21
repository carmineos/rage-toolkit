using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMloArchetypeDef : CBaseArchetypeDef
    {
        public int BlockLengthStructure => 0xF0;

        public uint mloFlags;
        public uint pad_94h;
        public Array_StructurePointer entities;
        public Array_Structure rooms;
        public Array_Structure portals;
        public Array_Structure entitySets;
        public Array_Structure timeCycleModifiers;
        public uint pad_E8h;
        public uint pad_ECh;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
