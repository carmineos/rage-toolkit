namespace RageLib.Resources.GTA5.PC.Meta
{
    public interface IMetaStructure
    {
        int StructureSize { get; }

        StructureInfo GetStructureInfo();
    }
}