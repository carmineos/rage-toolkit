using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class BoxOccluder : IMetaStructure
    {
        public int StructureSize => 0x10;

        public short iCenterX;
        public short iCenterY;
        public short iCenterZ;
        public short iCosZ;
        public short iLength;
        public short iWidth;
        public short iHeight;
        public short iSinZ;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
