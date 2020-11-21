using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__fwInstancedMapData : IMetaStructure
    {
        public int StructureSize => 0x40;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash ImapLink;
        public uint pad_Ch;
        public Array_Structure PropInstanceList;
        public Array_Structure GrassInstanceList;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
