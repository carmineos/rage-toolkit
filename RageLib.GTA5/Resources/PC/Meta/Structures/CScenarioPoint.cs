using RageLib.Resources.GTA5.PC.Meta.Enums;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioPoint : IMetaStructure
    {
        public int StructureSize => 0x40;

        public uint pad_0h;
        public uint pad_4h;
        public uint pad_8h;
        public uint pad_Ch;
        public uint pad_10h;
        public byte pad_14h;
        public byte iType;
        public byte ModelSetId;
        public byte iInterior;
        public byte iRequiredIMapId;
        public byte iProbability;
        public byte uAvailableInMpSp;
        public byte iTimeStartOverride;
        public byte iTimeEndOverride;
        public byte iRadius;
        public byte iTimeTillPedLeaves;
        public byte pad_1Fh;
        public ushort iScenarioGroup;
        public ushort pad_22h;
        public CScenarioPointFlags__Flags Flags;
        public uint pad_28h;
        public uint pad_2Ch;
        public Vector4 vPositionAndDirection;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
