using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMloRoomDef : IMetaStructure
    {
        public int StructureSize => 0x70;

        public uint pad_0h;
        public uint pad_4h;
        public CharPointer name;
        public uint pad_18h;
        public uint pad_1Ch;
        public Vector3 bbMin;
        public float pad_2Ch;
        public Vector3 bbMax;
        public float pad_3Ch;
        public float blend;
        public MetaHash timecycleName;
        public MetaHash secondaryTimecycleName;
        public uint flags;
        public uint portalCount;
        public int floorId;
        public int exteriorVisibiltyDepth;
        public uint pad_5Ch;
        public Array_uint attachedObjects;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
