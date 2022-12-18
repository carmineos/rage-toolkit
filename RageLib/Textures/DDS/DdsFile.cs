using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace RageLib.Textures.DDS
{
    // https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dx-graphics-dds-pguide
    // https://github.com/microsoft/DirectXTex/blob/main/DirectXTex/DDS.h
    // https://github.com/microsoft/DirectXTK12/blob/master/Src/DDS.h
    // https://github.com/dexyfex/CodeWalker/blob/master/CodeWalker.Core/GameFiles/Utils/DDSIO.cs#L1477
    public class DdsFile
    {
        public const uint DDS_MAGIC = 0x20534444;

        public DDS_HEADER header;
        public DDS_HEADER_DXT10? header10;
        public byte[] data;

        public static bool IsDDS(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return IsDDS(stream);
        }

        public static bool IsDDS(Stream stream)
        {
            var reader = new BinaryReader(stream);
            var magic = reader.ReadUInt32();
            stream.Position -= 4;

            return magic == DDS_MAGIC;
        }

        public static DdsFile Read(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return Read(stream);
        }

        public static DdsFile Read(Stream stream)
        {
            DdsFile dds = new DdsFile();
            var reader = new BinaryReader(stream);

            var magic = reader.ReadUInt32();

            if (magic != DDS_MAGIC)
                throw new InvalidDataException("Not a valid DDS magic");

            // Read Header
            reader.Read(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref dds.header, 1)));

            // Read Header DX10 if required
            if (dds.header.ddspf.dwFourCC == DDSFOURCC.DX10)
            {
                DDS_HEADER_DXT10 header10 = default;
                reader.Read(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref header10, 1)));
                dds.header10 = header10;
            }

            if (dds.header.IsCubeMap)
                throw new NotSupportedException("Cube Map isn't supported");

            if (dds.header.IsVolumeTexture)
                throw new NotSupportedException("Volume Texture isn't supported");

            if (dds.header10 is not null)
            {

            }
            else
            {
                dds.data = reader.ReadBytes((int)(stream.Length - stream.Position));
            }

            return dds;
        }

        public static void Write(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                Write(stream);
        }

        public static void Write(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
