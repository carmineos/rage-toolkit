using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPVComponentData : IMetaStructure
    {
        public int StructureSize => 0x18;

        public byte numAvailTex;
        public byte pad_1h;
        public ushort pad_2h;
        public uint pad_4h;
        public Array_Structure aDrawblData3;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
