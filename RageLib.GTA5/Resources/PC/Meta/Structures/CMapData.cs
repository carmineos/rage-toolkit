using RageLib.Resources.GTA5.PC.Meta.Types;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMapData : IMetaStructure
    {
        public int StructureSize => 0x200;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash name;
        public MetaHash parent;
        public uint flags;
        public uint contentFlags;
        public uint pad_18h;
        public uint pad_1Ch;
        public Vector3 streamingExtentsMin;
        public float pad_2Ch;
        public Vector3 streamingExtentsMax;
        public float pad_3Ch;
        public Vector3 entitiesExtentsMin;
        public float pad_4Ch;
        public Vector3 entitiesExtentsMax;
        public float pad_5Ch;
        public Array_StructurePointer entities;
        public Array_Structure containerLods;
        public Array_Structure boxOccluders;
        public Array_Structure occludeModels;
        public Array_uint physicsDictionaries;
        public rage__fwInstancedMapData instancedData;
        public Array_Structure timeCycleModifiers;
        public Array_Structure carGenerators;
        public CLODLight LODLightsSOA;
        public CDistantLODLight DistantLODLightsSOA;
        public CBlockDesc block;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
