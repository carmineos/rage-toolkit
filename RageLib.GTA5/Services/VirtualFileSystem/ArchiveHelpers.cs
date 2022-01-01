// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;
using RageLib.Data;
using RageLib.GTA5.ArchiveWrappers;
using System;
using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public static class ArchiveHelpers
    {
        public static IArchive Open(Stream stream, string name)
        {
            var reader = new DataReader(stream);
            var magic = reader.ReadUInt32();
            stream.Position -= 4;

            // TODO: Use IsRPF7
            if (magic == 0x52504637)
                return RageArchiveWrapper7.Open(stream, name);

            throw new Exception("Unknown RPF version");
        }
    }
}
