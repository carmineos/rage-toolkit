using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefSpawnPointOverride : CExtensionDef
    {
        public int BlockLengthStructure => 0x40;

        public MetaHash ScenarioType;
        public byte iTimeStartOverride;
        public byte iTimeEndOverride;
        public ushort pad_26h;
        public MetaHash Group;
        public MetaHash ModelSet;
        public Unk_3573596290 AvailabilityInMpSp;
        public CScenarioPointFlags__Flags Flags;
        public float Radius;
        public float TimeTillPedLeaves;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
