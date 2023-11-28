// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBoundPolyhedron : PhBound 
    {
        public override long BlockLength => 0xD0;

        // structure data
        public uint Unknown_80h;
        private uint ShrunkVerticesPointer;
        public uint Unknown_88h;
        private uint PrimitivesPointer;
        public Vector4 Quantum;
        public Vector4 Offset;
        private uint VerticesPointer;
        public uint Unknown_B4h;
        public uint Unknown_B8h;
        public uint Unknown_BCh;
        public uint Unknown_C0h;
        public uint Unknown_C4h;
        public uint VerticesCount;
        public uint PrimitivesCount;

        // reference data
        public SimpleArray<BoundVertex>? ShrunkVertices { get; set; }
        public SimpleArray<BoundVertex>? Vertices { get; set; }
        public SimpleArray<PhBoundPrimitive>? Primitives { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_80h = reader.ReadUInt32();
            ShrunkVerticesPointer = reader.ReadUInt32();
            Unknown_88h = reader.ReadUInt32();
            PrimitivesPointer = reader.ReadUInt32();
            Quantum = reader.ReadVector4();
            Offset = reader.ReadVector4();
            VerticesPointer = reader.ReadUInt32();
            Unknown_B4h = reader.ReadUInt32();
            Unknown_B8h = reader.ReadUInt32();
            Unknown_BCh = reader.ReadUInt32();
            Unknown_C0h = reader.ReadUInt32();
            Unknown_C4h = reader.ReadUInt32();
            VerticesCount = reader.ReadUInt32();
            PrimitivesCount = reader.ReadUInt32();

            // read reference data
            ShrunkVertices = reader.ReadBlockAt<SimpleArray<BoundVertex>>(ShrunkVerticesPointer, VerticesCount);
            Vertices = reader.ReadBlockAt<SimpleArray<BoundVertex>>(VerticesPointer, VerticesCount);
            Primitives = reader.ReadBlockAt<SimpleArray<PhBoundPrimitive>>(PrimitivesPointer, PrimitivesCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            ShrunkVerticesPointer = (uint)(ShrunkVertices?.BlockPosition ?? 0);
            VerticesPointer = (uint)(Vertices?.BlockPosition ?? 0);
            PrimitivesPointer = (uint)(Primitives?.BlockPosition ?? 0);
            VerticesCount = (uint)(Vertices?.Count ?? 0);
            PrimitivesCount = (uint)(Primitives?.Count ?? 0);

            // write structure data
            writer.Write(Unknown_80h);
            writer.Write(ShrunkVerticesPointer);
            writer.Write(Unknown_88h);
            writer.Write(PrimitivesPointer);
            writer.Write(Quantum);
            writer.Write(Offset);
            writer.Write(VerticesPointer);
            writer.Write(Unknown_B4h);
            writer.Write(Unknown_B8h);
            writer.Write(Unknown_BCh);
            writer.Write(Unknown_C0h);
            writer.Write(Unknown_C4h);
            writer.Write(VerticesCount);
            writer.Write(PrimitivesCount);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (ShrunkVertices is not null) list.Add(ShrunkVertices);
            if (Vertices is not null) list.Add(Vertices);
            if (Primitives is not null) list.Add(Primitives);
            return list.ToArray();
        }
    }
}
