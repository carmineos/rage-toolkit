using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioEntityOverride : IMetaStructure
    {
        public int StructureSize => 0x50;

        public Vector3 EntityPosition;
        public float pad_Ch;
        public MetaHash EntityType;
        public uint pad_14h;
        public Array_Structure ScenarioPoints;
        public uint pad_28h ;
        public uint pad_2Ch;
        public uint pad_30h;
        public uint pad_34h;
        public uint pad_38h;
        public uint pad_3Ch;
        public byte Unk_538733109;
        public byte Unk_1035513142;
        public ushort pad_42h;
        public uint pad_44h;
        public uint pad_48h;
        public uint pad_4Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
