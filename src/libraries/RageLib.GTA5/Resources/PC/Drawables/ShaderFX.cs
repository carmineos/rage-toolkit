// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Textures;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class ShaderFX : ResourceSystemBlock
    {
        public override long BlockLength => 0x30;

        // structure data
        public ulong ParametersPointer;
        public uint ShaderHash;
        public uint Unknown_Ch; // 0x00000000
        public byte ParameterCount;
        public byte DrawBucket;
        public ushort Unknown_12h;
        public ushort ParametersSize; // Header + Data size
        public ushort ParametersTotalSize; // Header + Data + Hashes(aligned to 16) + 32 size
        public uint SpsHash;
        public uint Unknown_1Ch; // 0x00000000
        public uint Unknown_20h;
        public ushort Unknown_24h;
        public byte Unknown_26h;
        public byte TextureParametersCount;
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000

        // reference data
        public ShaderParametersBlock_GTA5_pc? ParametersList { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.ParametersPointer = reader.ReadUInt64();
            this.ShaderHash = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.ParameterCount = reader.ReadByte();
            this.DrawBucket = reader.ReadByte();
            this.Unknown_12h = reader.ReadUInt16();
            this.ParametersSize = reader.ReadUInt16();
            this.ParametersTotalSize = reader.ReadUInt16();
            this.SpsHash = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt16();
            this.Unknown_26h = reader.ReadByte();
            this.TextureParametersCount = reader.ReadByte();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();

            // read reference data
            //this.Parameters = reader.ReadBlockAt<ResourceSimpleArray<ShaderParameter_GTA5_pc>>(
            //	this.ParametersPointer, // offset
            //	this.ParameterCount
            //);
            //this.ParameterHashes = reader.ReadBlockAt<SimpleArrayOFFSET<uint_r>>(
            //	this.ParametersPointer, // offset
            //	this.ParameterCount,
            //	this.TextureParametersCount
            //);


            this.ParametersList = reader.ReadBlockAt<ShaderParametersBlock_GTA5_pc>(
                this.ParametersPointer, // offset
                this.ParameterCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.ParametersPointer = (ulong)(this.ParametersList?.BlockPosition ?? 0);
            //this.ParametersPointer = (ulong)(this.Parameters != null ? this.Parameters.Position : 0);
            //this.ParameterCount = (byte)(this.Parameters != null ? this.Parameters.Count : 0);

            // write structure data
            writer.Write(this.ParametersPointer);
            writer.Write(this.ShaderHash);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.ParameterCount);
            writer.Write(this.DrawBucket);
            writer.Write(this.Unknown_12h);
            writer.Write(this.ParametersSize);
            writer.Write(this.ParametersTotalSize);
            writer.Write(this.SpsHash);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_26h);
            writer.Write(this.TextureParametersCount);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (ParametersList != null) list.Add(ParametersList);
            //		if (ParameterHashes != null) list.Add(ParameterHashes);
            return list.ToArray();
        }

    }

    public class ShaderParametersBlock_GTA5_pc : ResourceSystemBlock
    {

        public override long BlockLength
        {
            get
            {
                return BaseSize * (1 + 4);
            }
        }

        public long BaseSize
        {
            get
            {
                long size = 0;

                foreach (var x in Parameters)
                {
                    // Size of Parameters definitions
                    size += 16;

                    // Size of Parameters data
                    size += 16 * x.DataType;
                }

                // Size of Parameters Hashes (aligned to 16)
                var hashesSize = Parameters.Count * 4;
                hashesSize += (16 - (hashesSize % 16)) % 16;

                size += hashesSize;

                // Extra 32 bytes
                size += 32;

                return size;
            }
        }

        public ResourceSimpleArray<ShaderParameter> Parameters;
        public ResourceSimpleArray<SimpleArray<Vector4>> Data;
        public SimpleArray<uint> Hashes;
        // Hashes alignment pad
        // Extra 32 bytes 
        // Extra 4 * ParametersTotalSize bytes

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int cnt = Convert.ToInt32(parameters[0]);

            Parameters = reader.ReadBlock<ResourceSimpleArray<ShaderParameter>>(cnt);
            Data = new ResourceSimpleArray<SimpleArray<Vector4>>();
            int dataBlockSize = 0;
            for (int i = 0; i < cnt; i++)
            {
                var p = Parameters[i];

                // read reference data
                switch (p.DataType)
                {
                    case 0:
                        dataBlockSize += 0;
                        p.Data = reader.ReadBlockAt<Texture>(p.DataPointer);
                        break;
                    //case 1:
                    //    dataBlockSize += 16;
                    //    p.Data = reader.ReadBlockAt<RAGE_Vector4>(p.DataPointer);
                    //    break;
                    //case 2:
                    //    offset += 32;
                    //    p.Data = reader.ReadBlockAt<ResourceSimpleArray<RAGE_Vector4>>(
                    //         p.DataPointer, // offset
                    //         2
                    //     );
                    //    break;
                    //case 4:
                    //    offset += 64;
                    //    p.Data = reader.ReadBlockAt<ResourceSimpleArray<RAGE_Vector4>>(
                    //        p.DataPointer, // offset
                    //        4
                    //    );
                    //    break;

                    default:
                        dataBlockSize += 16 * p.DataType;
                        var data = reader.ReadBlockAt<SimpleArray<Vector4>>(p.DataPointer, p.DataType);
                        p.Data = data;
                        Data.Add(data);
                        break;
                }
            }

            // Skip Data among Parameters and Hashes which we have already read
            reader.Position += dataBlockSize;

            Hashes = reader.ReadBlock<SimpleArray<uint>>(cnt);

            // Read hashes alignment pad
            //reader.Position += (16 - (reader.Position % 16)) % 16;

            // Read extra 32 bytes
            //reader.Position += 32;

            // Read extra 4 * ParametersTotalSize bytes
            //reader.Position += 4 * BaseSize;
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update pointers...
            foreach (var f in Parameters)
                if (f.Data != null)
                    f.DataPointer = (ulong)f.Data.BlockPosition;
                else
                    f.DataPointer = 0;


            // write parameter infos
            foreach (var f in Parameters)
                writer.WriteBlock(f); 
            //writer.WriteBlock(Parameters);

            // write vector data
            foreach (var f in Parameters)
            {
                if (f.DataType != 0)
                    writer.WriteBlock(f.Data);
            }

            // write hashes
            //foreach (var h in Hashes)
            //    writer.WriteBlock(h); 
            writer.WriteBlock(Hashes);

            // Write hashes alignment pad
            var pad = (16 - (writer.Position % 16)) % 16;
            writer.Write(new byte[pad]);

            // Write extra 32 bytes
            writer.Write(new byte[32]);

            // Write extra 4 * ParametersTotalSize bytes
            writer.Write(new byte[4 * BaseSize]);
        }




        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());

            foreach (var x in Parameters)
                if (x.DataType == 0)
                    list.Add(x.Data);

            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            var list = new List<Tuple<long, IResourceBlock>>(base.GetParts());
            list.Add(new Tuple<long, IResourceBlock>(0x0, Parameters));

            long offset = Parameters.Count * 16;
            //foreach (var x in Parameters)
            //{
            //    list.Add(new Tuple<long, IResourceBlock>(offset, x));
            //    offset += 16;
            //}

            list.Add(new Tuple<long, IResourceBlock>(offset, Data));
            foreach (var x in Parameters)
            {
                if (x.DataType != 0)
                    offset += 16 * x.DataType;
            }
            list.Add(new Tuple<long, IResourceBlock>(offset, Hashes));

            return list.ToArray();
        }
    }
}
