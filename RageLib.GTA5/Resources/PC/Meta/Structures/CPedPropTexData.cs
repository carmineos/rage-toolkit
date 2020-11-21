namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPedPropTexData : IMetaStructure
    {
        public int StructureSize => 0xC;

        public int inclusions;
        public int exclusions;
        public byte texId;
        public byte inclusionId;
        public byte exclusionId;
        public byte distribution;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
