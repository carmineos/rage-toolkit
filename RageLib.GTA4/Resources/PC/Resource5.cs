// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Compression;
using RageLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RageLib.Resources.GTA4
{
    public enum Resource5CompressionType : ushort
    {
        Deflate = 0xDA78,
    }

    public class Resource5 : IResourceFile
    {
        protected const int RESOURCE_IDENT = 0x05435352;

        public int Version { get; set; }
        public uint Flags { get; set; }
        public Resource5CompressionType CompressionType { get; set; }

        public byte[] VirtualData { get; set; }
        public byte[] PhysicalData { get; set; }

        protected uint VirtualSize => (Flags & 0x7FF) << (int)(((Flags >> 11) & 0xF) + 8);
        protected uint PhysicalSize => ((Flags >> 15) & 0x7FF) << (int)(((Flags >> 26) & 0xF) + 8);

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
            Version = reader.ReadInt32();
            Flags = reader.ReadUInt32();
            CompressionType = (Resource5CompressionType)reader.ReadUInt16();

            if (CompressionType != Resource5CompressionType.Deflate)
                throw new Exception($"Unsupported compression type {CompressionType}");

            uint virtualSize = VirtualSize;
            uint physicalSize = PhysicalSize;

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
            writer.Write(RESOURCE_IDENT);
            writer.Write(Version);
            writer.Write(Flags);
            writer.Write((ushort)CompressionType);

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

    public class Resource5<T> : Resource5, IResourceFile<T> where T : PgBase32, new()
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
            
            // TODO: Add a ResourceHeader struct 
            VirtualData = new byte[VirtualSize];
            PhysicalData = new byte[PhysicalSize];

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
            var pgBase = ResourceData;

            // If we are building a new resource
            if(pgBase.PageMap is null)
            {
                pgBase.PageMap = new DatResourceMap32();
                systemBlocks.Add(pgBase.PageMap);
            }

            // TODO: Assign position to RSC5 blocks
        }

        public void Rebuild()
        {
            ResourceHelpers.RebuildBlocks(ResourceData);
        }
    }
}