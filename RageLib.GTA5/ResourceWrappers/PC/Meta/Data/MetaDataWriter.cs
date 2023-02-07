// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.Common.Collections;
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

        public SimpleArray<byte> GetSimpleArray()
        {
            var buffer = new byte[Stream.Length];
            Stream.Position = 0;
            Stream.Read(buffer, 0, (int)Stream.Length);
            return new SimpleArray<byte>(buffer);
        }
    }

    public class MetaDataWriter : DataWriter
    {
        private readonly List<MetaDataBlock> blocks;
        public List<MetaDataBlock> Blocks => blocks;

        public int BlocksCount => blocks.Count;

        private int blockIndex;
        public int BlockIndex => blockIndex;

        public override long Length => blocks[blockIndex].Stream.Length;

        public override long Position
        {
            get => blocks[blockIndex].Stream.Position;
            set => blocks[blockIndex].Stream.Position = value;
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

        protected override void WriteToStreamRaw(ReadOnlySpan<byte> value)
        {
            var currentStream = blocks[blockIndex].Stream;
            currentStream.Write(value);
        }

        protected override void WriteToStreamRaw(byte value)
        {
            var currentStream = blocks[blockIndex].Stream;
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
