using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMloPortalDef : IMetaStructure
    {
        public int StructureSize => 0x40;

        public uint pad_0h;
        public uint pad_4h;
        public uint roomFrom;
        public uint roomTo;
        public uint flags;
        public uint mirrorPriority;
        public uint opacity;
        public uint audioOcclusion;
        public Array_Vector3 corners;
        public Array_uint attachedObjects;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
