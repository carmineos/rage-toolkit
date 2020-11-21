namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPedPropInfo : IMetaStructure
    {
        public int StructureSize => 0x28;

        public byte numAvailProps;
        public byte pad_1h;
        public ushort pad_2h;
        public uint pad_4h;
        public Array_Structure aPropMetaData;
        public Array_Structure aAnchors;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
