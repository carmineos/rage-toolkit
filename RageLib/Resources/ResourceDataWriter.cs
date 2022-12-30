// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.Common;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RageLib.Resources
{
    /// <summary>
    /// Represents a resource data writer.
    /// </summary>
    public class ResourceDataWriter : DataWriter
    {
        private const long VIRTUAL_BASE = 0x50000000;
        private const long PHYSICAL_BASE = 0x60000000;

        private readonly Stream virtualStream;
        private readonly Stream physicalStream;

        /// <summary>
        /// Gets the length of the underlying stream.
        /// </summary>
        public override long Length => -1;

        /// <summary>
        /// Gets or sets the position within the underlying stream.
        /// </summary>
        public override long Position { get; set; }

        /// <summary>
        /// Initializes a new resource data reader for the specified system- and graphics-stream.
        /// </summary>
        public ResourceDataWriter(Stream virtualStream, Stream physicalStream, Endianness endianness = Endianness.LittleEndian)
            : base((Stream)null, endianness)
        {
            this.virtualStream = virtualStream;
            this.physicalStream = physicalStream;
        }

        /// <summary>
        /// Writes data to the underlying stream. This is the only method that directly accesses
        /// the data in the underlying stream.
        /// </summary>
        protected override void WriteToStreamRaw(ReadOnlySpan<byte> value)
        {
            Stream stream;
            long basePosition;

            if ((Position & VIRTUAL_BASE) == VIRTUAL_BASE)
            {
                // write to virtual stream...
                stream = virtualStream;
                basePosition = VIRTUAL_BASE;
            }
            else if ((Position & PHYSICAL_BASE) == PHYSICAL_BASE)
            {
                // write to physical stream...
                stream = physicalStream;
                basePosition = PHYSICAL_BASE;
            }
            else
                throw new Exception("illegal position!");

            stream.Position = Position & ~basePosition;

            stream.Write(value);

            Position = stream.Position | basePosition;
            return;
        }

        /// <summary>
        /// Writes data to the underlying stream. This is the only method that directly accesses
        /// the data in the underlying stream.
        /// </summary>
        protected override void WriteToStreamRaw(byte value)
        {
            Stream stream;
            long basePosition;

            if ((Position & VIRTUAL_BASE) == VIRTUAL_BASE)
            {
                // write to virtual stream...
                stream = virtualStream;
                basePosition = VIRTUAL_BASE;
            }
            else if ((Position & PHYSICAL_BASE) == PHYSICAL_BASE)
            {
                // write to physical stream...
                stream = physicalStream;
                basePosition = PHYSICAL_BASE;
            }
            else
                throw new Exception("illegal position!");

            stream.Position = Position & ~basePosition;

            stream.WriteByte(value);

            Position = stream.Position | basePosition;
            return;
        }

        /// <summary>
        /// Writes a block.
        /// </summary>
        public void WriteBlock(IResourceBlock value)
        {
            value.Write(this);
        }

        public void WriteValueList<T>(SimpleList64<T> valueList) where T : unmanaged
        {
            valueList.Write(this);
        }
    }
}