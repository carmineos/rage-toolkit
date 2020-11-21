using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CComponentInfo : IMetaStructure
    {
        public int StructureSize => 0x30;

        public MetaHash Unk_802196719;
        public MetaHash Unk_4233133352;
        public ArrayOfBytes5 Unk_128864925;
        public byte pad_Dh;
        public ushort pad_Eh;
        public uint pad_10h;
        public uint pad_14h;
        public uint pad_18h;
        public uint flags;
        public int inclusions;
        public int exclusions;
        public ePedVarComp Unk_1613922652;
        public ushort Unk_2114993291;
        public byte Unk_3509540765;
        public byte Unk_4196345791;
        public ushort pad_2Eh;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
