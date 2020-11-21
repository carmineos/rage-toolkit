using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CBaseArchetypeDef : IMetaStructure
    {
        public int StructureSize => 0x90;

        public uint pad_0h;
        public uint pad_4h;
        public float lodDist;
        public uint flags;
        public uint specialAttribute;
        public uint pad_14h;
        public uint pad_18h;
        public uint pad_1Ch;
        public Vector3 bbMin;
        public float pad_2Ch;
        public Vector3 bbMax;
        public float pad_3C;
        public Vector3 bsCentre;
        public float pad_4Ch;
        public float bsRadius;
        public float hdTextureDist;
        public MetaHash name;
        public MetaHash textureDictionary;
        public MetaHash clipDictionary;
        public MetaHash drawableDictionary;
        public MetaHash physicsDictionary;
        public rage__fwArchetypeDef__eAssetType assetType;
        public MetaHash assetName;
        public uint pad_74h;
        public Array_StructurePointer extensions;
        public uint pad_88h;
        public uint pad_8Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
