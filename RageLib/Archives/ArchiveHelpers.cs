// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;

namespace RageLib.Archives
{
    public class DataBlock
    {
        public long Offset { get; set; }
        public long Length { get; set; }
        public object Tag { get; set; }

        public DataBlock(long offset, long length, object tag = null)
        {
            this.Offset = offset;
            this.Length = length;
            this.Tag = tag;
        }
    }

    public static class ArchiveHelpers
    {
        private const int BUFFER_SIZE = 16384;

        /// <summary>
        /// Given a list of data blocks, this method finds the maximum length of a specified data block.
        /// </summary>
        public static long FindSpace(List<DataBlock> blocks, DataBlock item)
        {
            // sort list...
            blocks.Sort((a, b) => a.Offset.CompareTo(b.Offset));

            // find smallest follow element
            DataBlock next = null;
            foreach (var x in blocks)
            {
                if ((x != item) && (x.Offset >= item.Offset))
                {
                    if (next == null)
                    {
                        next = x;
                    }
                    else
                    {
                        if (x.Offset < next.Offset)
                            next = x;
                    }
                }

            }

            if (next == null)
                return long.MaxValue;
            
            return next.Offset - item.Offset;
        }

        /// <summary>
        /// Given a list of data blocks, this method finds an block of empty space of specified length.
        /// </summary>
        public static long FindOffset(List<DataBlock> blocks, long neededSpace, long blockSize = 1)
        {
            if (blocks.Count == 0)
                return 0;

            var lst = new List<DataBlock>(blocks);

            // sort list...
            lst.Sort((a, b) => a.Offset.CompareTo(b.Offset));
                
            // patch...
            for (int i = 0; i < lst.Count - 1; i++)
            {
                lst[i].Length = Math.Min(lst[i].Length, lst[i + 1].Offset - lst[i].Offset);
            }
            
            long offset = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                long len = lst[i].Offset - offset;
                if (len >= neededSpace)
                    return offset;

                offset = lst[i].Offset + lst[i].Length;
                offset = (long)Math.Ceiling((double)offset / (double)blockSize) * blockSize;
            }

            return offset;
        }

        public static void MoveBytes(Stream stream, long sourceOffset, long destinationOffset, long length)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(BUFFER_SIZE);
            while (length > 0)
            {
                // read...
                stream.Position = sourceOffset;
                int i = stream.Read(buffer, 0, (int)Math.Min(length, BUFFER_SIZE));

                // write...
                stream.Position = destinationOffset;
                stream.Write(buffer, 0, i);

                sourceOffset += i;
                destinationOffset += i;
                length -= i;
            }
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}
