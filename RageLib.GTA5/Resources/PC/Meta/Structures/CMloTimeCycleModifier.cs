using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMloTimeCycleModifier : IMetaStructure
    {
        public int StructureSize => 0x30;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash name;
        public uint Unused2;
        public Vector4 sphere;
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
