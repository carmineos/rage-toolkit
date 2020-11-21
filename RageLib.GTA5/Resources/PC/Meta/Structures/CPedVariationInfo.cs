using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPedVariationInfo : IMetaStructure
    {
        public int StructureSize => 0x70;

        public byte bHasTexVariations;
        public byte bHasDrawblVariations;
        public byte bHasLowLODs;
        public byte bIsSuperLOD;
        public ArrayOfBytes12 availComp;
        public Array_Structure aComponentData3;
        public Array_Structure aSelectionSets;
        public Array_Structure compInfos;
        public CPedPropInfo propInfo;
        public MetaHash dlcName;
        public uint pad_6C;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
