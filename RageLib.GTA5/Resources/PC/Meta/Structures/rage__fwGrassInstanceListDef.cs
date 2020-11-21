using RageLib.Resources.GTA5.PC.Meta.Types;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__fwGrassInstanceListDef : IMetaStructure
    {
        public int StructureSize => 0x60;

        public rage__spdAABB BatchAABB;
        public Vector3 ScaleRange;
        public float Unused0;
        public MetaHash archetypeName;
        public uint lodDist;
        public float LodFadeStartDist;
        public float LodInstFadeRange;
        public float OrientToTerrain;
        public uint pad_44h;
        public Array_Structure InstanceList;
        public uint pad_58h;
        public uint pad_5Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
