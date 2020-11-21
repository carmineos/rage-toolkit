using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CBlockDesc : IMetaStructure
    {
        public int StructureSize => 0x48;

        public uint version;
        public uint flags;
        public CharPointer name;
        public CharPointer exportedBy;
        public CharPointer owner;
        public CharPointer time;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
