using RageLib.Resources.GTA5.PC.Meta.Enums;
using RageLib.Resources.GTA5.PC.Meta.Types;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    // datBase
    // fwEntityDef
    // CEntityDef
    public class CEntityDef : IMetaStructure
    {
        public int StructureSize => 0x80;

        public uint pad_0h;
        public uint pad_4h;
        public MetaHash archetypeName;
        public uint flags;
        public uint guid;
        public uint pad_14h;
        public uint pad_18h;
        public uint pad_1Ch;
        public Vector3 position;
        public float pad_2C;
        public Vector4 rotation;
        public float scaleXY;
        public float scaleZ;
        public int parentIndex;
        public float lodDist;
        public float childLodDist;
        public rage__eLodType lodLevel;
        public uint numChildren;
        public rage__ePriorityLevel priorityLevel;
        public Array_StructurePointer extensions;
        public int ambientOcclusionMultiplier;
        public int artificialAmbientOcclusion;
        public uint tintValue;
        public uint pad_7Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}