namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CShaderVariableComponent : IMetaStructure
    {
        public int StructureSize => 0x48;

        public uint pad_0h;
        public uint pad_4h;
        public uint pedcompID;
        public uint maskID;
        public uint shaderVariableHashString;
        public uint pad_14h;
        public Array_byte tracks;
        public Array_ushort ids;
        public Array_byte components;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
