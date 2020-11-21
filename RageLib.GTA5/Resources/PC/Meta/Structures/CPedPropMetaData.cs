using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CPedPropMetaData : IMetaStructure
    {
        public int StructureSize => 0x38;

        public MetaHash audioId;
        public ArrayOfBytes5 expressionMods;
        public byte pad_9h;
        public ushort pad_Ah;
        public uint pad_Ch;
        public uint pad_10h;
        public uint pad_14h;
        public Array_Structure texData;
        public ePropRenderFlags renderFlags;
        public uint propFlags;
        public ushort flags;
        public byte anchorId;
        public byte propId;
        public byte Unk_2894625425;
        public byte pad_35h;
        public ushort pad_36h;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
