using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefLightEffect : CExtensionDef
    {
        public int BlockLengthStructure => 0x30;

        public Array_Structure instances;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
