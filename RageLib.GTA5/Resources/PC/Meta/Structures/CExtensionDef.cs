using RageLib.Resources.GTA5.PC.Meta.Types;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    // datBase
    // fwExtensionDef
    // CExtensionDef
    public class CExtensionDef : IMetaStructure
    {
        public int StructureSize => 0x20;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash name;
        public uint pad_Ch;
        public Vector3 offsetPosition;
        public float pad_1Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
