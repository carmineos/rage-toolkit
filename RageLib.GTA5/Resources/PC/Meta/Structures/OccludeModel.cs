using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class OccludeModel : IMetaStructure
    {
        public int StructureSize => 0x40;

        public Vector3 bmin;
        public float pad_Ch;
        public Vector3 bmax;
        public float pad_1Ch;
        public uint dataSize;
        public uint Unused2;
        public DataBlockPointer verts;
        public ushort numVertsInBytes;
        public ushort numTris;
        public uint flags;
        public uint pad_38h;
        public uint pad_3Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
