using System;
using System.IO.Compression;

namespace RageLib.Compression
{
    // Breaking Change in .NET6 https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/partial-byte-reads-in-streams
    public static class DeflateStreamExtensions
    {
        public static int ReadAll(this DeflateStream stream, byte[] buffer, int offset, int count)
        {
            return ReadAll(stream, buffer.AsSpan(offset, count));
        }

        public static int ReadAll(this DeflateStream stream, Span<byte> buffer)
        {
            int totalRead = 0;

            while (totalRead < buffer.Length)
            {
                int bytesRead = stream.Read(buffer.Slice(totalRead));
                if (bytesRead == 0) break;
                totalRead += bytesRead;
            }

            return totalRead;
        }
    }
}
