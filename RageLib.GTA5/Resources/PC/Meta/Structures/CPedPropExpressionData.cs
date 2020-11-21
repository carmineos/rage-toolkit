using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPedPropExpressionData : IMetaStructure
    {
        public int StructureSize => 0x58;

        public uint pad_0h;
        public uint pad_4h;
        public uint pedPropID;
        public int pedPropVarIndex;
        public uint pedPropExpressionIndex;
        public uint pad_14h;
        public Array_byte tracks;
        public Array_ushort ids;
        public Array_byte types;
        public Array_byte components;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
