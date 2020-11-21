using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefSpawnPoint : CExtensionDef
    {
        public int BlockLengthStructure => 0x60;

        public Vector4 offsetRotation;
        public MetaHash spawnType;
        public MetaHash pedType;
        public MetaHash group;
        public MetaHash interior;
        public MetaHash requiredImap;
        public Unk_3573596290 availableInMpSp;
        public float probability;
        public float timeTillPedLeaves;
        public float radius;
        public byte start;
        public byte end;
        public ushort pad_56h;
        public CScenarioPointFlags__Flags flags;
        public byte highPri;
        public byte extendedRange;
        public byte shortRange;
        public byte pad_5Fh;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
