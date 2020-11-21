using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioPointCluster : IMetaStructure
    {
        public int StructureSize => 0x50;

        public CScenarioPointContainer Points;
        public rage__spdSphere ClusterSphere;
        public float Unk_1095875445;
        public byte Unk_3129415068;
        public uint pad_48h;
        public uint pad_4Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
