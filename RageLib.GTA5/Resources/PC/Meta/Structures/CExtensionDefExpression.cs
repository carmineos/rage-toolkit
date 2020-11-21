using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefExpression : CExtensionDef
    {
        public int BlockLengthStructure => 0x30;

        public MetaHash expressionDictionaryName;
        public MetaHash expressionName;
        public MetaHash creatureMetadataName;
        public byte initialiseOnCollision;
        public byte pad_2Dh;
        public ushort pad_2Eh;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
