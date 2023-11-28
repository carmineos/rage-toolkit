﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Compression;
using RageLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RageLib.Resources.RDR2
{
    public class Resource8 : IResource
    {
        protected const int RESOURCE_IDENT = 0x38435352;

        public DatResourceFileHeader ResourceFileHeader;
        
        public int Version { get; set; }

        public byte[] VirtualData { get; set; }
        public byte[] PhysicalData { get; set; }

        public void Load(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
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

            ResourceFileHeader = new DatResourceFileHeader()
            {   
                Id = ident,
                Flags = flags,
                
                ResourceInfo = new DatResourceInfo()
                {
                    VirtualFlags = virtualPageFlags,
                    PhysicalFlags = physicalPageFlags
                },
            };
            
            Version = (int)ResourceFileHeader.Flags & 0xFF;

            if (((ResourceFileHeader.Flags >> 24) & 1) != 1 || ((ResourceFileHeader.Flags >> 8 & 0xF)) != 0)
                throw new Exception("oodle compression isn't supported!");

            var virtualSize = virtualPageFlags & 0x7FFFFFF0;
            var physicalSize = physicalPageFlags & 0x7FFFFFF0;

            VirtualData = new byte[virtualSize];
            PhysicalData = new byte[physicalSize];

            var deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true);
            deflateStream.ReadExactly(VirtualData, 0, (int)virtualSize);
            deflateStream.ReadExactly(PhysicalData, 0, (int)physicalSize);
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
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var reader = new DataReader(fileStream);
                var ident = reader.ReadInt32();
                return ident == RESOURCE_IDENT;
            }
        }
    }

    public class Resource8<T> : Resource8, IResource<T> where T : IResourceBlock, new()
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
            var resBlock = (IResourceBlock)ResourceData;
            var fileBase = (PgBase64)resBlock;

            // Create a temp datResourceMap
            fileBase.PageMap = new DatResourceMap(64, 64);


            ResourceHelpers.GetBlocks(ResourceData, out IList<IResourceBlock> systemBlocks, out IList<IResourceBlock> graphicBlocks);

            ResourceHelpers.AssignPositions(systemBlocks, 0x50000000, out ResourceChunkFlags virtualPageFlags, 0);
            
            ResourceHelpers.AssignPositions(graphicBlocks, 0x60000000, out ResourceChunkFlags physicalPageFlags, virtualPageFlags.Count);

            fileBase.PageMap.VirtualPagesCount = (byte)virtualPageFlags.Count;
            fileBase.PageMap.PhysicalPagesCount = (byte)physicalPageFlags.Count;

            // Add version to the flags
            virtualPageFlags += ((((uint)Version >> 4) & 0xF) << 28);
            physicalPageFlags += ((((uint)Version >> 0) & 0xF) << 28);

            // create a header
            ResourceFileHeader = new DatResourceFileHeader
            {
                Id = RESOURCE_IDENT,
                Flags = (uint)Version,
                ResourceInfo = new DatResourceInfo()
                {
                    VirtualFlags = virtualPageFlags,
                    PhysicalFlags = physicalPageFlags
                }
            };

            ////////////////////////////////////////////////////////////////////////////
            // data to byte-array
            ////////////////////////////////////////////////////////////////////////////

            var systemStream = new MemoryStream();
            var graphicsStream = new MemoryStream();
            var resourceWriter = new ResourceDataWriter(systemStream, graphicsStream);

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

            var sysBuf = new byte[virtualPageFlags.Size];
            systemStream.Flush();
            systemStream.Position = 0;
            systemStream.Read(sysBuf, 0, (int)systemStream.Length);
            VirtualData = sysBuf;

            var gfxBuf = new byte[physicalPageFlags.Size];
            graphicsStream.Flush();
            graphicsStream.Position = 0;
            graphicsStream.Read(gfxBuf, 0, (int)graphicsStream.Length);
            PhysicalData = gfxBuf;

            base.Save(stream);
        }

        public void LoadUncompressed(string sysFileName, string gfxFileName)
        {
            if(File.Exists(sysFileName))
            {
                using (var stream = new FileStream(sysFileName, FileMode.Open))
                {
                    var reader = new DataReader(stream);
                    reader.Position = 0;

                    VirtualData = new byte[(int)stream.Length];
                    stream.Read(VirtualData, 0, (int)stream.Length);
                }
            }

            if (File.Exists(gfxFileName))
            {
                using (var stream = new FileStream(gfxFileName, FileMode.Open))
                {
                    var reader = new DataReader(stream);
                    reader.Position = 0;

                    PhysicalData = new byte[(int)stream.Length];
                    stream.Read(PhysicalData, 0, (int)stream.Length);
                }
            }

            var systemStream = new MemoryStream(VirtualData);
            var graphicsStream = new MemoryStream(PhysicalData);
            var resourceStream = new ResourceDataReader(systemStream, graphicsStream);
            resourceStream.Position = 0x50000000;

            ResourceData = resourceStream.ReadBlock<T>();
        }
    }
}