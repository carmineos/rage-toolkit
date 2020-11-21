using RageLib.Resources.GTA5.PC.Meta.Enums;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioChainingEdge : IMetaStructure
    {
        public int StructureSize => 0x8;

        public ushort NodeIndexFrom;
        public ushort NodeIndexTo;
        public CScenarioChainingEdge__eAction Action;
        public CScenarioChainingEdge__eNavMode NavMode;
        public CScenarioChainingEdge__eNavSpeed NavSpeed;
        public byte pad_7h;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
