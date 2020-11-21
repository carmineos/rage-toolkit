namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioChain : IMetaStructure
    {
        public int StructureSize => 0x28;

        public byte Unk_1156691834;
        public byte pad_1h;
        public ushort pad_2h;
        public uint pad_4h;
        public Array_ushort EdgeIds;
        public uint pad_18h;
        public uint pad_1Ch;
        public uint pad_20h;
        public uint pad_24h;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
