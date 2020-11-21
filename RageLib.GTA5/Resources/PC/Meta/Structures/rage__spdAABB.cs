using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__spdAABB : IMetaStructure
    {
        public int StructureSize => 0x20;

        public Vector4 min;
        public Vector4 max;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
