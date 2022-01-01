// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Compression;
using RageLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RageLib.Resources.GTA5
{
    public class ResourceFile_GTA5_pc : IResourceFile
    {
        protected const int RESOURCE_IDENT = 0x37435352;

        public DatResourceFileHeader ResourceFileHeader;
        
        public int Version { get; set; }

        public byte[] VirtualData { get; set; }
        public byte[] PhysicalData { get; set; }

        public void Load(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                Load(fileStream);
        }

        public virtual void Load(Stream stream)
        {
            var reader = new DataReader(stream);
            reader.Position = 0;

            // read the header
            var ident = reader.ReadUInt32();
            var flags = reader.ReadUInt32();
            uint virtualPageFlags = reader.ReadUInt32();
            uint physicalPageFlags = reader.ReadUInt32();

            ResourceFileHeader = new DatResourceFileHeader(ident, flags, virtualPageFlags, physicalPageFlags);
            Version = (int)ResourceFileHeader.Flags & 0xFF;

            var virtualSize = ((ResourceChunkFlags)virtualPageFlags).Size;
            var physicalSize = ((ResourceChunkFlags)physicalPageFlags).Size;

            VirtualData = new byte[virtualSize];
            PhysicalData = new byte[physicalSize];

            var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true);
            deflateStream.ReadAll(VirtualData, 0, (int)virtualSize);
            deflateStream.ReadAll(PhysicalData, 0, (int)physicalSize);
            deflateStream.Close();
        }

        public void Save(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
                Save(fileStream);
        }

        public virtual void Save(Stream stream)
        {
            var writer = new DataWriter(stream);

            // write the header
            writer.Write(ResourceFileHeader.Id);
            writer.Write(ResourceFileHeader.Flags);
            writer.Write(ResourceFileHeader.ResourceInfo.VirtualFlags);
            writer.Write(ResourceFileHeader.ResourceInfo.PhysicalFlags);

            var deflateStream = new DeflateStream(stream, CompressionMode.Compress, true);
            deflateStream.Write(VirtualData, 0, VirtualData.Length);
            deflateStream.Write(PhysicalData, 0, PhysicalData.Length);
            deflateStream.Flush();
            deflateStream.Close();
        }

        public static bool IsResourceFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return IsResourceFile(fileStream);
        }

        public static bool IsResourceFile(Stream stream)
        {
            var reader = new DataReader(stream);
            var ident = reader.ReadInt32();
            stream.Position -= 4;
            return ident == RESOURCE_IDENT;
        }
    }

    public class ResourceFile_GTA5_pc<T> : ResourceFile_GTA5_pc, IResourceFile<T> where T : IResourceBlock, new()
    {
        public T ResourceData { get; set; }

        public override void Load(Stream stream)
        {
            base.Load(stream);

            var systemStream = new MemoryStream(VirtualData);
            var graphicsStream = new MemoryStream(PhysicalData);
            var resourceStream = new ResourceDataReader(systemStream, graphicsStream);
            resourceStream.Position = 0x50000000;

            ResourceData = resourceStream.ReadBlock<T>();           
        }

        public override void Save(Stream stream)
        {
            ResourceHelpers.GetBlocks(ResourceData, out IList<IResourceBlock> systemBlocks, out IList<IResourceBlock> graphicBlocks);

            RemapBlocks(systemBlocks, graphicBlocks);

            ////////////////////////////////////////////////////////////////////////////
            // data to byte-array
            ////////////////////////////////////////////////////////////////////////////
            
            var resourceInfo = ResourceFileHeader.ResourceInfo;

            var virtualSize = (int)((ResourceChunkFlags)resourceInfo.VirtualFlags).Size;
            var physicalSize = (int)((ResourceChunkFlags)resourceInfo.PhysicalFlags).Size;

            VirtualData = new byte[virtualSize];
            PhysicalData = new byte[physicalSize];

            var virtualStream = new MemoryStream(VirtualData);
            var physicalStream = new MemoryStream(PhysicalData);
            var resourceWriter = new ResourceDataWriter(virtualStream, physicalStream);

            resourceWriter.Position = 0x50000000;
            foreach (var block in systemBlocks)
            {
                resourceWriter.Position = block.BlockPosition;

                var pos_before = resourceWriter.Position;
                block.Write(resourceWriter);
                var pos_after = resourceWriter.Position;

                if ((pos_after - pos_before) != block.BlockLength)
                {
                    throw new Exception("error in system length");
                }
            }

            resourceWriter.Position = 0x60000000;
            foreach (var block in graphicBlocks)
            {
                resourceWriter.Position = block.BlockPosition;

                var pos_before = resourceWriter.Position;
                block.Write(resourceWriter);
                var pos_after = resourceWriter.Position;

                if ((pos_after - pos_before) != block.BlockLength)
                {
                    throw new Exception("error in graphics length");
                }
            }

            virtualStream.Flush();
            virtualStream.Position = 0;

            physicalStream.Flush();
            physicalStream.Position = 0;

            base.Save(stream);
        }        

        public void RemapBlocks(IList<IResourceBlock> systemBlocks, IList<IResourceBlock> graphicBlocks)
        {
            var rootBlock = (IResourceBlock)ResourceData;
            var pgBase = (PgBase64)rootBlock;

            // If we are building a new resource
            if(pgBase.PageMap is null)
            {
                pgBase.PageMap = new DatResourceMap(64, 64);
                systemBlocks.Add(pgBase.PageMap);
            }
            else
            {
                pgBase.PageMap.VirtualPagesCount = 64;
                pgBase.PageMap.PhysicalPagesCount = 64;
            }

            ResourceHelpers.AssignPositions(systemBlocks, 0x50000000, out ResourceChunkFlags virtualPageFlags, 0);
            ResourceHelpers.AssignPositions(graphicBlocks, 0x60000000, out ResourceChunkFlags physicalPageFlags, virtualPageFlags.Count);
            pgBase.PageMap.VirtualPagesCount = (byte)virtualPageFlags.Count;
            pgBase.PageMap.PhysicalPagesCount = (byte)physicalPageFlags.Count;

            // Add version to the flags
            virtualPageFlags |= (((uint)Version >> 4) & 0xF) << 28;
            physicalPageFlags |= (((uint)Version >> 0) & 0xF) << 28;

            // create new a header
            ResourceFileHeader = new DatResourceFileHeader(RESOURCE_IDENT, (uint)Version, virtualPageFlags, physicalPageFlags);
        }

        public void Rebuild()
        {
            ResourceHelpers.RebuildBlocks(ResourceData);
        }
    }
}