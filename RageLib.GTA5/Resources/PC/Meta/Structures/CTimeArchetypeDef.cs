using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CTimeArchetypeDef : CBaseArchetypeDef
    {
        public int BlockLengthStructure => 0xA0;

        public uint timeFlags;
        public uint pad_94h;
        public uint pad_98h;
        public uint pad_9Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
