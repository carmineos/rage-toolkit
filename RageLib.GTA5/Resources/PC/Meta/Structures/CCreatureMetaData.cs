namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CCreatureMetaData : IMetaStructure
    {
        public int StructureSize => 0x38;

        public uint pad_0h;
        public uint pad_4h;
        public Array_Structure shaderVariableComponents;
        public Array_Structure pedPropExpressions;
        public Array_Structure pedCompExpressions;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
