using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CCompositeEntityType : IMetaStructure
    {
        public int StructureSize => 0x130;

        public ArrayOfChars64 Name;
        public float lodDist;
        public uint flags;
        public uint specialAttribute;
        public uint pad_4Ch;
        public Vector3 bbMin;
        public float pad_5Ch;
        public Vector3 bbMax;
        public float pad_6Ch;
        public Vector3 bsCentre;
        public float pad_7Ch;
        public float bsRadius;
        public uint pad_84h;
        public ArrayOfChars64 StartModel;
        public ArrayOfChars64 EndModel;
        public MetaHash StartImapFile;
        public MetaHash EndImapFile;
        public MetaHash PtFxAssetName;
        public uint pad_114h;
        public Array_Structure Animations;
        public uint pad_128h;
        public uint pad_12Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
