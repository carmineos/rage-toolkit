using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__spdGrid2D : IMetaStructure
    {
        public int StructureSize => 0x40;

        public uint pad_0h;
        public uint pad_8h;
        public uint pad_Ch;
        public int MinCellX;
        public int MaxCellX;
        public int MinCellY;
        public int MaxCellY;
        public uint pad_1Ch;
        public uint pad_20h;
        public uint pad_24h;
        public uint pad_28h;
        public float CellDimX;
        public float CellDimY;
        public uint pad_34h;
        public uint pad_38h;
        public uint pad_3Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
