using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class Unk_4023740759 : IMetaStructure
    {
        public int StructureSize => 0x58;

        public Array_Structure Nodes;
        public Array_Structure Edges;
        public Array_Structure Chains;
        public uint pad_30h;
        public uint pad_34h;
        public uint pad_38h;
        public uint pad_3Ch;
        public uint pad_40h;
        public uint pad_44h;
        public uint pad_48h;
        public uint pad_4Ch;
        public uint pad_50h;
        public uint pad_54h;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
