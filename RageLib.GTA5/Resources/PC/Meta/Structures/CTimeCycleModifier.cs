using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CTimeCycleModifier : IMetaStructure
    {
        public int StructureSize => 0x40;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash name;
        public uint pad_Ch;
        public Vector3 minExtents;
        public float pad_1Ch;
        public Vector3 maxExtents;
        public float pad_2Ch;
        public float percentage;
        public float range;
        public uint startHour;
        public uint endHour;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
