using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefAudioEmitter : CExtensionDef
    {
        public int BlockLengthStructure => 0x40;

        public Vector4 offsetRotation;
        public MetaHash effectHash;
        public uint pad_34;
        public uint pad_38;
        public uint pad_3C;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
