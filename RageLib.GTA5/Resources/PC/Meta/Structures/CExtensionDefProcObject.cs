namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefProcObject : CExtensionDef
    {
        public int BlockLengthStructure => 0x50;

        public float radiusInner;
        public float radiusOuter;
        public float spacing;
        public float minScale;
        public float maxScale;
        public float minScaleZ;
        public float maxScaleZ;
        public float minZOffset;
        public float maxZOffset;
        public uint objectHash;
        public uint flags;
        public uint pad_4Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
