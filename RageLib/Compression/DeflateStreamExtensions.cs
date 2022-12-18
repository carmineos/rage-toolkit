// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.IO.Compression;

namespace RageLib.Compression
{
    // Breaking Change in .NET6 https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/partial-byte-reads-in-streams
    public static class DeflateStreamExtensions
    {
#if !NET7_0_OR_GREATER
        public static void ReadExactly(this DeflateStream stream, byte[] buffer, int offset, int count)
        {
    #if NET6
                var bufferSpan = buffer.AsSpan(offset,count);
                
                int totalRead = 0;
                
                while (totalRead < buffer.Length)
                {
                    int bytesRead = stream.Read(bufferSpan.Slice(totalRead));
                    if (bytesRead == 0) break;
                    totalRead += bytesRead;
                }
    #else
                stream.Read(buffer, offset, count);
    #endif
    }
#endif
    }
}
