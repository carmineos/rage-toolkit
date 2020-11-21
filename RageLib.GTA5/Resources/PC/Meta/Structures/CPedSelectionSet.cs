using RageLib.Resources.GTA5.PC.Meta.Types;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPedSelectionSet : IMetaStructure
    {
        public int StructureSize => 0x30;

        public MetaHash name;
        public ArrayOfBytes12 compDrawableId;
        public ArrayOfBytes12 compTexId;
        public ArrayOfBytes6 propAnchorId;
        public ArrayOfBytes6 propDrawableId;
        public ArrayOfBytes6 propTexId;
        public ushort pad_2Eh;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
