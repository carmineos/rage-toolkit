using System.Drawing;
using System.Runtime.InteropServices;

namespace RageLib.Textures.DDS
{
    [StructLayout(LayoutKind.Explicit, Size = 20)]
    public struct DDS_HEADER_DXT10
    {
        [FieldOffset(0)] 
        public DXGI_FORMAT dxgiFormat;
        
        [FieldOffset(4)] 
        public DDS_RESOURCE_DIMENSION resourceDimension;
        
        [FieldOffset(8)] 
        public uint miscFlag;
        
        [FieldOffset(12)] 
        public uint arraySize;
        
        [FieldOffset(16)] 
        public uint miscFlags2;
    }

    public enum DDS_RESOURCE_DIMENSION : uint
    {
        DDS_RESOURCE_DIMENSION_UNKNOWN = 0,
        DDS_RESOURCE_DIMENSION_BUFFER = 1,
        DDS_RESOURCE_DIMENSION_TEXTURE1D = 2,
        DDS_RESOURCE_DIMENSION_TEXTURE2D = 3,
        DDS_RESOURCE_DIMENSION_TEXTURE3D = 4,
    }
}
