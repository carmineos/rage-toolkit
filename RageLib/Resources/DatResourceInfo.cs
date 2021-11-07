namespace RageLib.Resources
{
    // datResourceInfo
    public struct DatResourceInfo
    {
        public uint VirtualFlags;
        public uint PhysicalFlags;

        public DatResourceInfo(uint virtualFlags, uint physicalFlags)
        {
            VirtualFlags = virtualFlags;
            PhysicalFlags = physicalFlags;
        }

        public int Version => (int)(((PhysicalFlags & 0xF0000000) >> 28) | (VirtualFlags & 0xF0000000) >> 24);
    }
}
