using RageLib.Resources.GTA5.PC.Meta.Enums;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CAnchorProps : IMetaStructure
    {
        public int StructureSize => 0x18;

        public Array_byte props;
        public eAnchorPoints anchor;
        public uint pad_14h;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
