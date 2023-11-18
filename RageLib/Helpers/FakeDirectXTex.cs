// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

#if !DIRECTXTEX
namespace DirectXTex;

public class DDSIO
{
    internal static ImageStruct ReadDDS(string fileName)
    {
        throw new NotImplementedException();
    }

    internal static void WriteDDS(string fileName, ImageStruct img)
    {
        throw new NotImplementedException();
    }
}

public class ImageCompressor
{
    internal static byte[] Compress(byte[] data, int width, int height, int dXGI_FORMAT_BC5_UNORM)
    {
        throw new NotImplementedException();
    }

    internal static byte[] Decompress(byte[] data, int width, int height, int dXGI_FORMAT_BC1_UNORM)
    {
        throw new NotImplementedException();
    }
}

public class ImageStruct
{
    public byte[] Data { get; internal set; }
    public int Width { get; internal set; }
    public int Height { get; internal set; }
    public int MipMapLevels { get; internal set; }
    public int Format { get; internal set; }

    internal int GetRowPitch()
    {
        throw new NotImplementedException();
    }
}

public class ImageConverter
{
    internal static byte[] Convert(byte[] data, int width, int height, int dXGI_FORMAT_A8_UNORM, int dXGI_FORMAT_R8G8B8A8_UNORM)
    {
        throw new NotImplementedException();
    }
}
#endif
