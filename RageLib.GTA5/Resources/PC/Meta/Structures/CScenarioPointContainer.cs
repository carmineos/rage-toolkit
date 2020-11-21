namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioPointContainer : IMetaStructure
    {
        public int StructureSize => 0x30;

        public Array_Structure LoadSavePoints;
        public Array_Structure MyPoints;
        public uint pad_20h;
        public uint pad_24h;
        public uint pad_28h;
        public uint pad_2Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
