using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioChainingNode : IMetaStructure
    {
        public int StructureSize => 0x20;

        public Vector3 Position;
        public float pad_Ch;
        public MetaHash Unk_2602393771;
        public MetaHash ScenarioType;
        public byte Unk_407126079_NotFirst;
        public byte Unk_1308720135_NotLast;
        public ushort pad_1Ah;
        public uint pad_1Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
