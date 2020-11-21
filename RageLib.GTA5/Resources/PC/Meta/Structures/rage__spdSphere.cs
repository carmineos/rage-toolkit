using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__spdSphere : IMetaStructure
    {
        public int StructureSize => 0x10;

        public Vector4 centerAndRadius;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
