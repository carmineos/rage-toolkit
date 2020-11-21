using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CCompEntityEffectsData : IMetaStructure
    {
        public int StructureSize => 0xA0;

        public uint fxType;
        public uint pad_4h;
        public uint pad_8h;
        public uint pad_Ch;
        public Vector3 fxOffsetPos;
        public float pad_1Ch;
        public Vector4 fxOffsetRot;
        public uint boneTag;
        public float startPhase;
        public float endPhase;
        public byte ptFxIsTriggered;
        public ArrayOfChars64 ptFxTag;
        public byte pad_7Dh;
        public ushort pad_7Eh;
        public float ptFxScale;
        public float ptFxProbability;
        public byte ptFxHasTint;
        public byte ptFxTintR;
        public byte ptFxTintG;
        public byte ptFxTintB;
        public uint pad_8Ch;
        public Vector3 ptFxSize;
        public uint pad_9Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
