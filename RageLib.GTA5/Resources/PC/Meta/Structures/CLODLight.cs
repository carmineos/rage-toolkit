using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CLODLight : IMetaStructure
    {
        public int StructureSize => 0x88;

        public uint pad_0h;
        public uint pad_4h;
        public Array_Structure direction;
        public Array_float falloff;
        public Array_float falloffExponent;
        public Array_uint timeAndStateFlags;
        public Array_uint hash;
        public Array_byte coneInnerAngle;
        public Array_byte coneOuterAngleOrCapExt;
        public Array_byte coronaIntensity;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
