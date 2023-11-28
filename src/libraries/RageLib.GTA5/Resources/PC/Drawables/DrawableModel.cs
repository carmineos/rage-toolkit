// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // datBase
    // grmModel
    public class DrawableModel : DatBase64
    {
        public override long BlockLength => 0x30;

        // structure data
        public ResourcePointerList64<DrawableGeometry> Geometries;
        public ulong GeometriesBoundsPointer;
        public ulong ShaderMappingPointer;
        public byte Unknown_28h;
        public byte IsSkinned;
        public byte Unknown_2Ah;
        public byte RootBoneIndex;
        public byte Mask;
        public byte Unknown_2Dh;
        public ushort GeometriesCount;

        // reference data
        public SimpleArray<Aabb>? GeometriesBounds { get; set; }
        public SimpleArray<ushort>? ShaderMapping { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Geometries = reader.ReadBlock<ResourcePointerList64<DrawableGeometry>>();
            this.GeometriesBoundsPointer = reader.ReadUInt64();
            this.ShaderMappingPointer = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadByte();
            this.IsSkinned = reader.ReadByte();
            this.Unknown_2Ah = reader.ReadByte();
            this.RootBoneIndex = reader.ReadByte();
            this.Mask = reader.ReadByte();
            this.Unknown_2Dh = reader.ReadByte();
            this.GeometriesCount = reader.ReadUInt16();

            // read reference data
            this.GeometriesBounds = reader.ReadBlockAt<SimpleArray<Aabb>>(
                this.GeometriesBoundsPointer, // offset
                this.GeometriesCount > 1 ? this.GeometriesCount + 1 : this.GeometriesCount
            );
            this.ShaderMapping = reader.ReadBlockAt<SimpleArray<ushort>>(
                this.ShaderMappingPointer, // offset
                this.GeometriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.GeometriesBoundsPointer = (ulong)(this.GeometriesBounds?.BlockPosition ?? 0);
            this.ShaderMappingPointer = (ulong)(this.ShaderMapping?.BlockPosition ?? 0);

            // write structure data
            writer.WriteBlock(this.Geometries);
            writer.Write(this.GeometriesBoundsPointer);
            writer.Write(this.ShaderMappingPointer);
            writer.Write(this.Unknown_28h);
            writer.Write(this.IsSkinned);
            writer.Write(this.Unknown_2Ah);
            writer.Write(this.RootBoneIndex);
            writer.Write(this.Mask);
            writer.Write(this.Unknown_2Dh);
            writer.Write(this.GeometriesCount);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (GeometriesBounds != null) list.Add(GeometriesBounds);
            if (ShaderMapping != null) list.Add(ShaderMapping);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x8, Geometries)
            };
        }
    }
}
