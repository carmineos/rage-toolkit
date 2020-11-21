using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPVTextureData : IMetaStructure
    {
        public int StructureSize => 0x3;

        public byte texId;
        public byte distribution;
        public byte pad_2h;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
