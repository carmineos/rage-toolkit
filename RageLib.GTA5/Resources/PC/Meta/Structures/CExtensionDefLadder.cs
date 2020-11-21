using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefLadder : CExtensionDef
    {
        public int BlockLengthStructure => 0x60;

        public Vector3 bottom;
        public float pad_2Ch;
        public Vector3 top;
        public float pad_3Ch;
        public Vector3 normal;
        public float pad_4Ch;
        public Unk_1294270217 materialType;
        public MetaHash template;
        public byte canGetOffAtTop;
        public byte canGetOffAtBottom;
        public ushort pad_5Ah;
        public uint pad_5Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
