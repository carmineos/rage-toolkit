﻿/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.IO;

namespace RageLib.Data
{
    public delegate long GetOffsetDelegate();
    public delegate long GetLengthDelegate();
    public delegate void SetLengthDelegate(long length);

    /// <summary>
    /// Represents a part of a stream.
    /// </summary>
    public class PartialStream : Stream
    {
        private readonly Stream baseStream;
        private readonly GetOffsetDelegate getOffsetDelegate;
        private readonly GetLengthDelegate getLengthDelegate;
        private readonly SetLengthDelegate setLengthDelegate;
        private long relativePositon;

        /// <summary>
        /// Gets a value indicating whether the stream supports seeking. 
        /// </summary>
        public override bool CanSeek => true;

        /// <summary>
        /// Gets a value indicating whether the stream supports reading. 
        /// </summary>
        public override bool CanRead => baseStream.CanRead;

        /// <summary>
        /// Gets a value indicating whether the stream supports writing. 
        /// </summary>
        public override bool CanWrite => baseStream.CanWrite;

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        public override long Length => getLengthDelegate();

        /// <summary>
        /// Gets or sets the position within the stream.
        /// </summary>
        public override long Position
        {
            get => relativePositon;
            set
            {
                //value = Math.Min(value, getLengthDelegate());
                //value = Math.Max(value, 0);
                if (Position > Length)
                    SetLength(Position);
                relativePositon = value;
            }
        }

        public PartialStream(
            Stream baseStream,
            GetOffsetDelegate getOffsetDelegate,
            GetLengthDelegate getLengthDelegate,
            SetLengthDelegate setLengthDelegate = null)
        {
            this.baseStream = baseStream;
            this.getOffsetDelegate = getOffsetDelegate;
            this.getLengthDelegate = getLengthDelegate;
            this.setLengthDelegate = setLengthDelegate;
        }

        /// <summary>
        /// Reads a sequence of bytes from the stream.
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            // backup position
            var positionBackup = baseStream.Position;

            int maxCount = (int)(getLengthDelegate() - relativePositon);
            int newcount = Math.Min(count, maxCount);

            baseStream.Position = getOffsetDelegate() + relativePositon;
            int r = baseStream.Read(buffer, offset, newcount);
            relativePositon += r;

            // restore position
            baseStream.Position = positionBackup;

            return r;
        }

        /// <summary>
        /// Writes a sequence of bytes to the stream.
        /// </summary>
        public override void Write(byte[] buffer, int offset, int count)
        {
            // backup position
            var positionBackup = baseStream.Position;

            var newlen = relativePositon + count;
            if (newlen > Length)
                setLengthDelegate(newlen);

            int maxCount = (int)(getLengthDelegate() - relativePositon);
            var newcount = Math.Min(count, maxCount);

            baseStream.Position = getOffsetDelegate() + relativePositon;
            baseStream.Write(buffer, offset, count);
            relativePositon += count;

            // restore position
            baseStream.Position = positionBackup;
        }

        /// <summary>
        /// Sets the position within the stream. 
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    {
                        relativePositon = offset;
                        break;
                    }
                case SeekOrigin.Current:
                    {
                        relativePositon += offset;
                        break;
                    }
                case SeekOrigin.End:
                    {
                        relativePositon = getLengthDelegate() + offset;
                        break;
                    }
            }

            return relativePositon;
        }

        /// <summary>
        /// Sets the length of the stream. 
        /// </summary>
        public override void SetLength(long value)
        {
            setLengthDelegate(value);
        }

        /// <summary>
        /// Clears all buffers for the stream.
        /// </summary>
        public override void Flush()
        {
            baseStream.Flush();
        }
    }
}