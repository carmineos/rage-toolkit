namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CDistantLODLight : IMetaStructure
    {
        public int StructureSize => 0x30;

        public uint pad_0h;
        public uint pad_4h;
        public Array_Structure position;
        public Array_uint RGBI;
        public ushort numStreetLights;
        public ushort category;
        public uint pad_2Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
