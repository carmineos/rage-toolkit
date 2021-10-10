using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageLib.Helpers.DDS
{
    internal static class DirectX
    {
        internal static void ComputePitch(DXGI_FORMAT format, nuint width, nuint height, out nuint rowPitch, out nuint slicePitch, uint flags)
        {
            throw new NotImplementedException();
        }
    }

    internal static class ImageConverter
    {
        public static byte[] Convert(byte[] data, int width, int height, int inputFormat, int outputFormat)
        {
            throw new NotImplementedException();
        }
    }

    internal static class ImageCompressor
    {
        public static byte[] Decompress(byte[] data, int width, int height, int format)
        {
            throw new NotImplementedException();
        }

        public static byte[] Compress(byte[] data, int width, int height, int format)
        {
            throw new NotImplementedException();
        }
    }
}
