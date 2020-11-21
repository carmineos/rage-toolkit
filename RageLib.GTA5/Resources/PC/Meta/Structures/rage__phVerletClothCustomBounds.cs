using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__phVerletClothCustomBounds : IMetaStructure
    {
        public int StructureSize => 0x20;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash name;
        public uint pad_Ch;
        public Array_Structure CollisionData;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
