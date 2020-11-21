using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CStreamingRequestRecord : IMetaStructure
    {
        public int StructureSize => 0x28;

        public Array_Structure Frames;
        public Array_Structure CommonSets;
        public byte NewStyle;
        public byte pad_21C;
        public ushort pad_22C;
        public uint pad_24C;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
