using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMloInstanceDef : CEntityDef
    {
        public int BlockLengthStructure => 0xA0;

        public uint groupId;
        public uint floorId;
        public Array_uint defaultEntitySets;
        public uint numExitPortals;
        public uint MLOInstflags;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
