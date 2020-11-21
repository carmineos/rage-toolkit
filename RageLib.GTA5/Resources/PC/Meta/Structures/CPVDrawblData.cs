using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPVDrawblData : IMetaStructure
    {
        public int StructureSize => 0x30;

        public byte propMask;
        public byte numAlternatives;
        public ushort pad_2h;
        public uint pad_4h;
        public Array_Structure aTexData;
        public Unk_2236980467 clothData;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
