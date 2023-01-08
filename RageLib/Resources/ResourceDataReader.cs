﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace RageLib.Resources
{
    /// <summary>
    /// Represents a resource data reader.
    /// </summary>
    public class ResourceDataReader : DataReader
    {
        private const long VIRTUAL_BASE = 0x50000000;
        private const long PHYSICAL_BASE = 0x60000000;

        private readonly Stream virtualStream;
        private readonly Stream physicalStream;

        // this is a dictionary that contains all the resource blocks
        // which were read from this resource reader
        private readonly Dictionary<long, List<IResourceBlock>> blockPool;

        /// <summary>
        /// Gets the length of the underlying stream.
        /// </summary>
        public override long Length => -1;

        /// <summary>
        /// Gets or sets the position within the underlying stream.
        /// </summary>
        public override long Position { get; set; }

        /// <summary>
        /// Initializes a new resource data reader for the specified virtual- and physical-stream.
        /// </summary>
        public ResourceDataReader(Stream virtualStream, Stream physicalStream, Endianness endianness = Endianness.LittleEndian)
            : base((Stream)null, endianness)
        {
            this.virtualStream = virtualStream;
            this.physicalStream = physicalStream;
            this.blockPool = new Dictionary<long, List<IResourceBlock>>();
        }

        protected override void ReadFromStreamRaw(Span<byte> span)
        {
            Stream stream;
            long basePosition;

            if ((Position & VIRTUAL_BASE) == VIRTUAL_BASE)
            {
                // read from virtual stream...
                stream = virtualStream;
                basePosition = VIRTUAL_BASE;
            }
            else if ((Position & PHYSICAL_BASE) == PHYSICAL_BASE)
            {
                // read from physical stream...
                stream = physicalStream;
                basePosition = PHYSICAL_BASE;
            }
            else
                throw new Exception("illegal position!");

            stream.Position = Position & ~basePosition;
            stream.Read(span);
            Position = stream.Position | basePosition;
        }

        protected override byte ReadByteFromStreamRaw()
        {
            Stream stream;
            long basePosition;

            if ((Position & VIRTUAL_BASE) == VIRTUAL_BASE)
            {
                // read from virtual stream...
                stream = virtualStream;
                basePosition = VIRTUAL_BASE;
            }
            else if ((Position & PHYSICAL_BASE) == PHYSICAL_BASE)
            {
                // read from physical stream...
                stream = physicalStream;
                basePosition = PHYSICAL_BASE;
            }
            else
                throw new Exception("illegal position!");

            stream.Position = Position & ~basePosition;
            var b = (byte)stream.ReadByte();
            Position = stream.Position | basePosition;
            return b;
        }

        /// <summary>
        /// Reads a block.
        /// </summary>
        public T ReadBlock<T>(params object[] parameters) where T : IResourceBlock, new()
        {
            // make sure to return the same object if the same
            // block is read again...
            if (blockPool.ContainsKey(Position))
            {
                var blocks = blockPool[Position];
                foreach (var block in blocks)
                    if (block is T)
                    {
                        Position += block.BlockLength;

                        // since a resource block of the same type
                        // has been found at the same address, return it
                        return (T)block;
                    }
#if DEBUG
                    else
                    {
                        // TODO:    Be sure we aren't reading a base class of an object that
                        //          we have already read at the same position as but derived type
                        //          This shouldn't happen, so far only happened if we read
                        //          ShaderParameter Texture data blocks before and then after
                        //          we read TextureDX11 from a texture dictionary embedded in the ShaderGroup
                        //          as the if check above will return false since Texture (in the pool) is not TextureDX11 (but the opposite)
                        //          the result is that the pool will create another block instead of returning the same
                        //          We have to workaround limitation of dealing with managed objects (so we can't just recast the pointer)
                        //          We want to replace it with the new most derived block, and update all the references to it
                        //          Maybe Texture has a field to which specifies which type of Texture it is?
                        //          see https://github.com/carmineos/gta-toolkit/issues/11

                        System.Diagnostics.Debug.Assert(!block.GetType().IsAssignableFrom(typeof(T)));
                    }
#endif
            }

            var result = new T();


            // replace with correct type...
            if (result is IResourceXXSystemBlock)
                result = (T)((IResourceXXSystemBlock)result).GetType(this, parameters);


            // add block to the block pool...
            if (blockPool.ContainsKey(Position))
            {
                blockPool[Position].Add(result);
            }
            else
            {
                var blocks = new List<IResourceBlock>();
                blocks.Add(result);
                blockPool.Add(Position, blocks);
            }

            var classPosition = Position;            
            result.Read(this, parameters);
            result.BlockPosition = classPosition;
            return (T)result;
        }

        /// <summary>
        /// Reads a block at a specified position.
        /// </summary>
        public T ReadBlockAt<T>(ulong position, params object[] parameters) where T : IResourceBlock, new()
        {
            if (position != 0)
            {
                var positionBackup = Position;

                Position = (long)position;
                var result = ReadBlock<T>(parameters);
                Position = positionBackup;

                return result;
            }
            else
            {
                return default(T);
            }
        }
        
        public SimpleList64<T> ReadValueList<T>(params object[] parameters) where T : unmanaged
        {
            var list = new SimpleList64<T>();
            list.Read(this, parameters);
            return list;
        }

        public SimpleBigList64<T> ReadBigValueList<T>(params object[] parameters) where T : unmanaged
        {
            var list = new SimpleBigList64<T>();
            list.Read(this, parameters);
            return list;
        }

        public ResourceSimpleList64<T> ReadList<T>(params object[] parameters) where T : IResourceSystemBlock, new()
        {
            var list = new ResourceSimpleList64<T>();
            list.Read(this, parameters);
            return list;
        }

        public ResourcePointerList64<T> ReadPointerList<T>(params object[] parameters) where T : IResourceSystemBlock, new()
        {
            var list = new ResourcePointerList64<T>();
            list.Read(this, parameters);
            return list;
        }
    }
}