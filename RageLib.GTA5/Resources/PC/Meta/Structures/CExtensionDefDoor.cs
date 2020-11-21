using RageLib.Resources.GTA5.PC.Meta.Types;
using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefDoor : CExtensionDef
    {
        public int BlockLengthStructure => 0x30;

        public byte enableLimitAngle;
        public byte startsLocked;
        public byte canBreak;
        public byte pad_23h;
        public float limitAngle;
        public float doorTargetRatio;
        public MetaHash audioHash;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
