// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // grmModel
    // VFT - 0x0000000140912C58
    public class GrmModel : DatBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        public ResourcePointerList64<GrmGeometry> Geometries;
        private ulong GeometriesBoundsPointer;
        private ulong ShaderMappingPointer;
        public ulong Unknown_28h; // 0x0000000000000000
        public uint Unknown_30h;
        public ushort Unknown_34h;
        public ushort GeometriesCount;
        public ulong Unknown_38h; // 0x0000000000000000

        // reference data
        public SimpleArray<Aabb>? GeometriesBounds { get; set; }
        public SimpleArray<ushort>? ShaderMapping { get; set; }

        public GrmModel()
        {
            Geometries = new ResourcePointerList64<GrmGeometry>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Geometries = reader.ReadBlock<ResourcePointerList64<GrmGeometry>>();
            GeometriesBoundsPointer = reader.ReadUInt64();
            ShaderMappingPointer = reader.ReadUInt64();
            Unknown_28h = reader.ReadUInt64();
            Unknown_30h = reader.ReadUInt32();
            Unknown_34h = reader.ReadUInt16();
            GeometriesCount = reader.ReadUInt16();
            Unknown_38h = reader.ReadUInt64();

            // read reference data
            GeometriesBounds = reader.ReadBlockAt<SimpleArray<Aabb>>(GeometriesBoundsPointer, GeometriesCount > 1 ? GeometriesCount + 1 : GeometriesCount);
            ShaderMapping = reader.ReadBlockAt<SimpleArray<ushort>>(ShaderMappingPointer, GeometriesCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            GeometriesBoundsPointer = (ulong)(GeometriesBounds?.BlockPosition ?? 0);
            ShaderMappingPointer = (ulong)(ShaderMapping?.BlockPosition ?? 0);
            GeometriesCount = Geometries?.EntriesCount ?? 0;

            // write structure data
            writer.WriteBlock(Geometries);
            writer.Write(GeometriesBoundsPointer);
            writer.Write(ShaderMappingPointer);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_34h);
            writer.Write(GeometriesCount);
            writer.Write(Unknown_38h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x8, Geometries),
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
