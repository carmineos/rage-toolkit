namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class rage__fwGrassInstanceListDef__InstanceData : IMetaStructure
    {
        public int StructureSize => 0x10;

        public ArrayOfUshorts3 Position;
        public byte NormalX;
        public byte NormalY;
        public ArrayOfBytes3 Color;
        public byte Scale;
        public byte Ao;
        public ArrayOfBytes3 Pad;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
