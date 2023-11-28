// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class GrmModel : DatBase32
    {
        public override long BlockLength => 0x1C;

        // structure data
        public ResourcePointerList32<GrmGeometry> Geometries;
        private uint GeometriesBoundsPointer;
        private uint ShaderMappingPointer;
        public ushort Unknown_14h;
        private byte Unknown_16h; // 0xCD
        public byte Unknown_17h;
        public ushort Unknown_18h;
        public ushort GeometriesCount;

        // reference data
        public SimpleArray<Aabb>? GeometriesBounds { get; set; }
        public SimpleArray<ushort>? ShaderMapping { get; set; }

        public GrmModel()
        {
            Geometries = new ResourcePointerList32<GrmGeometry>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Geometries = reader.ReadBlock<ResourcePointerList32<GrmGeometry>>();
            GeometriesBoundsPointer = reader.ReadUInt32();
            ShaderMappingPointer = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt16();
            Unknown_16h = reader.ReadByte();
            Unknown_17h = reader.ReadByte();
            Unknown_18h = reader.ReadUInt16();
            GeometriesCount = reader.ReadUInt16();

            // read reference data
            GeometriesBounds = reader.ReadBlockAt<SimpleArray<Aabb>>(GeometriesBoundsPointer, GeometriesCount);
            ShaderMapping = reader.ReadBlockAt<SimpleArray<ushort>>(ShaderMappingPointer, GeometriesCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            GeometriesBoundsPointer = (uint)(Geometries?.BlockPosition ?? 0);
            ShaderMappingPointer = (uint)(ShaderMapping?.BlockPosition ?? 0);
            GeometriesCount = Geometries?.EntriesCount ?? 0;

            // write structure data
            writer.WriteBlock(Geometries);
            writer.Write(GeometriesBoundsPointer);
            writer.Write(ShaderMappingPointer);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_16h);
            writer.Write(Unknown_17h);
            writer.Write(Unknown_18h);
            writer.Write(GeometriesCount);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x4, Geometries),
            };
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (GeometriesBounds is not null) list.Add(GeometriesBounds);
            if (ShaderMapping is not null) list.Add(ShaderMapping);
            return list.ToArray();
        }
    }
}
