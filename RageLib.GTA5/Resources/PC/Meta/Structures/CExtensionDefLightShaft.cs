using RageLib.Resources.GTA5.PC.Meta.Enums;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefLightShaft : CExtensionDef
    {
        public int BlockLengthStructure => 0xB0;

        public Vector3 cornerA;
        public float pad_2Ch;
        public Vector3 cornerB;
        public float pad_3Ch;
        public Vector3 cornerC;
        public float pad_4Ch;
        public Vector3 cornerD;
        public float pad_5Ch;
        public Vector3 direction;
        public float pad_6Ch;
        public float directionAmount;
        public float length;
        public float fadeInTimeStart;
        public float fadeInTimeEnd;
        public float fadeOutTimeStart;
        public float fadeOutTimeEnd;
        public float fadeDistanceStart;
        public float fadeDistanceEnd;
        public uint color;
        public float intensity;
        public byte flashiness;
        public byte Unused09;
        public ushort Unused10;
        public uint flags;
        public Unk_1931949281 densityType;
        public Unk_2266515059 volumeType;
        public float softness;
        public byte scaleBySunIntensity;
        public byte pad_ADh;
        public ushort pad_AEh;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
