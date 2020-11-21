using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioPointRegion : IMetaStructure
    {
        public int StructureSize => 0x178;

        public int VersionNumber;
        public uint pad_4h;
        public CScenarioPointContainer Points;
        public uint pad_38h;
        public uint pad_3Ch;
        public uint pad_40h;
        public uint pad_44h;
        public Array_Structure EntityOverrides;
        public uint pad_58h;
        public uint pad_5Ch;
        public Unk_4023740759 Unk_3696045377;
        public rage__spdGrid2D AccelGrid;
        public Array_ushort Unk_3844724227;
        public Array_Structure Clusters;
        public CScenarioPointLookUps LookUps;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
