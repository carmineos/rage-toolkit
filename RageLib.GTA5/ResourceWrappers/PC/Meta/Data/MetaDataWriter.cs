// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Data
{
    public class MetaDataBlock
    {
        public int NameHash { get; private set; }
        public Stream Stream { get; private set; }

        public MetaDataBlock(int nameHash, Stream stream)
        {
            this.NameHash = nameHash;
            this.Stream = stream;
        }
    }

    public class MetaDataWriter : DataWriter
    {
        private List<MetaDataBlock> blocks;
        public List<MetaDataBlock> Blocks
        {
            get
            {
                return blocks;
            }
        }

        public int BlocksCount
        {
            get
            {
                return blocks.Count;
            }
        }

        private int blockIndex;
        public int BlockIndex
        {
            get
            {
                return blockIndex;
            }
        }

        public override long Length
        {
            get
            {
                return blocks[BlockIndex].Stream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return blocks[BlockIndex].Stream.Position;
            }

            set
            {
                blocks[BlockIndex].Stream.Position = value;
            }
        }

        public MetaDataWriter() : base(null, Endianness.LittleEndian)
        {
            this.blocks = new List<MetaDataBlock>();
            this.blockIndex = -1;
        }

        public MetaDataWriter(Endianness e) : base(null, e)
        {
            this.blocks = new List<MetaDataBlock>();
            this.blockIndex = -1;
        }

        protected override void WriteToStreamRaw(Span<byte> value)
        {
            var currentStream = blocks[BlockIndex].Stream;
            currentStream.Write(value);
        }

        protected override void WriteToStreamRaw(byte value)
        {
            var currentStream = blocks[BlockIndex].Stream;
            currentStream.WriteByte(value);
        }

        public void SelectBlockByNameHash(int nameHash)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].NameHash == nameHash && blocks[i].Stream.Length < 0x4000)
                {
                    SelectBlockByIndex(i);
                    return;
                }
            }

            CreateBlockByNameHash(nameHash);
        }

        public void CreateBlockByNameHash(int nameHash)
        {
            var newBlock = new MetaDataBlock(nameHash, new MemoryStream());
            blocks.Add(newBlock);
            SelectBlockByIndex(blocks.Count - 1);
        }

        public void SelectBlockByIndex(int index)
        {
            blockIndex = index;
        }
    }
}
