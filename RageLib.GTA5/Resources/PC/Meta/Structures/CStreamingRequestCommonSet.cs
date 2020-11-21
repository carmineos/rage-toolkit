using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CStreamingRequestCommonSet : IMetaStructure
    {
        public int StructureSize => 0x10;

        public Array_uint Requests;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
