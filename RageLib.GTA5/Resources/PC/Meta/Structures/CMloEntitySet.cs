using RageLib.Resources.GTA5.PC.Meta.Types;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMloEntitySet : IMetaStructure
    {
        public int StructureSize => 0x30;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash name;
        public uint pad_Ch;
        public Array_uint locations;
        public Array_StructurePointer entities;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
