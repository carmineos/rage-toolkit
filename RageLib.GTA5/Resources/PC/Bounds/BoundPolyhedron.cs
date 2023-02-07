﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common.Collections;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    // phBoundPolyhedron
    public class BoundPolyhedron : Bound
    {
        public override long BlockLength => 0xF0;

        // structure data
        private uint Unknown_70h;
        private uint Unknown_74h;
        public ulong ShrunkVerticesPointer;
        private uint Unknown_80h;
        public uint VerticesCount1;
        public ulong PrimitivesPointer;
        public Vector3 Quantum;
        private float Unknown_9Ch;
        public Vector3 Offset;
        private float Unknown_ACh;
        public ulong VerticesPointer;
        public ulong VerticesColorsPointer;
        private ulong Unknown_C0h_Pointer;
        private ulong Unknown_C8h_Pointer;
        public uint VerticesCount2;
        public uint PrimitivesCount;
        private ulong Unknown_D8h; // 0x0000000000000000
        private ulong Unknown_E0h; // 0x0000000000000000
        private ulong Unknown_E8h; // 0x0000000000000000

        // reference data
        public SimpleArray<BoundVertex>? ShrunkVertices { get; set; }
        public SimpleArray<BoundPrimitive>? Primitives { get; set; }
        public SimpleArray<BoundVertex>? Vertices { get; set; }
        public SimpleArray<uint>? VerticesColors { get; set; }
        public SimpleArray<uint>? Unknown_C0h_Data { get; set; }
        public SimpleArrayArray64<uint>? Unknown_C8h_Data { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_70h = reader.ReadUInt32();
            this.Unknown_74h = reader.ReadUInt32();
            this.ShrunkVerticesPointer = reader.ReadUInt64();
            this.Unknown_80h = reader.ReadUInt32();
            this.VerticesCount1 = reader.ReadUInt32();
            this.PrimitivesPointer = reader.ReadUInt64();
            this.Quantum = reader.ReadVector3();
            this.Unknown_9Ch = reader.ReadSingle();
            this.Offset = reader.ReadVector3();
            this.Unknown_ACh = reader.ReadSingle();
            this.VerticesPointer = reader.ReadUInt64();
            this.VerticesColorsPointer = reader.ReadUInt64();
            this.Unknown_C0h_Pointer = reader.ReadUInt64();
            this.Unknown_C8h_Pointer = reader.ReadUInt64();
            this.VerticesCount2 = reader.ReadUInt32();
            this.PrimitivesCount = reader.ReadUInt32();
            this.Unknown_D8h = reader.ReadUInt64();
            this.Unknown_E0h = reader.ReadUInt64();
            this.Unknown_E8h = reader.ReadUInt64();

            // read reference data
            this.ShrunkVertices = reader.ReadBlockAt<SimpleArray<BoundVertex>>(
                this.ShrunkVerticesPointer, // offset
                this.VerticesCount2
            );
            this.Primitives = reader.ReadBlockAt<SimpleArray<BoundPrimitive>>(
                this.PrimitivesPointer, // offset
                this.PrimitivesCount
            );
            this.Vertices = reader.ReadBlockAt<SimpleArray<BoundVertex>>(
                this.VerticesPointer, // offset
                this.VerticesCount2
            );
            this.VerticesColors = reader.ReadBlockAt<SimpleArray<uint>>(
                this.VerticesColorsPointer, // offset
                this.VerticesCount2
            );
            this.Unknown_C0h_Data = reader.ReadBlockAt<SimpleArray<uint>>(
                this.Unknown_C0h_Pointer, // offset
                8
            );
            this.Unknown_C8h_Data = reader.ReadBlockAt<SimpleArrayArray64<uint>>(
                this.Unknown_C8h_Pointer, // offset
                8,
                this.Unknown_C0h_Data
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.ShrunkVerticesPointer = (ulong)(this.ShrunkVertices?.BlockPosition ?? 0);
            this.VerticesCount1 = (uint)(this.Vertices?.Count ?? 0);
            this.PrimitivesPointer = (ulong)(this.Primitives?.BlockPosition ?? 0);
            this.VerticesPointer = (ulong)(this.Vertices?.BlockPosition ?? 0);
            this.VerticesColorsPointer = (ulong)(this.VerticesColors?.BlockPosition ?? 0);
            this.Unknown_C0h_Pointer = (ulong)(this.Unknown_C0h_Data?.BlockPosition ?? 0);
            this.Unknown_C8h_Pointer = (ulong)(this.Unknown_C8h_Data?.BlockPosition ?? 0);
            this.VerticesCount2 = (uint)(this.Vertices?.Count ?? 0);
            this.PrimitivesCount = (uint)(this.Primitives?.Count ?? 0);

            // write structure data
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_74h);
            writer.Write(this.ShrunkVerticesPointer);
            writer.Write(this.Unknown_80h);
            writer.Write(this.VerticesCount1);
            writer.Write(this.PrimitivesPointer);
            writer.Write(this.Quantum);
            writer.Write(this.Unknown_9Ch);
            writer.Write(this.Offset);
            writer.Write(this.Unknown_ACh);
            writer.Write(this.VerticesPointer);
            writer.Write(this.VerticesColorsPointer);
            writer.Write(this.Unknown_C0h_Pointer);
            writer.Write(this.Unknown_C8h_Pointer);
            writer.Write(this.VerticesCount2);
            writer.Write(this.PrimitivesCount);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.Unknown_E0h);
            writer.Write(this.Unknown_E8h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (ShrunkVertices != null) list.Add(ShrunkVertices);
            if (Primitives != null) list.Add(Primitives);
            if (Vertices != null) list.Add(Vertices);
            if (VerticesColors != null) list.Add(VerticesColors);
            if (Unknown_C0h_Data != null) list.Add(Unknown_C0h_Data);
            if (Unknown_C8h_Data != null) list.Add(Unknown_C8h_Data);
            return list.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetVertex(BoundVertex quantizedVertex)
        {
            return new Vector3(quantizedVertex.X * Quantum.X, quantizedVertex.Y * Quantum.Y, quantizedVertex.Z * Quantum.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetVertex(int index)
        {
            var quantizedVertex = Vertices[index];
            return new Vector3(quantizedVertex.X * Quantum.X, quantizedVertex.Y * Quantum.Y, quantizedVertex.Z * Quantum.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetVertexOffset(int index)
        {
            return GetVertex(index) + Offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 GetVertexOffset(Vector3 vertex)
        {
            return vertex + Offset;
        }

        public Vector3 CalculateQuantum()
        {
            var aabbMin = new Vector3(float.MaxValue);
            var aabbMax = new Vector3(float.MinValue);

            for (int i = 0; i < VerticesCount2; i++)
            {
                var vertex = GetVertex(Vertices[i]);

                aabbMin = Vector3.Min(aabbMin, vertex);
                aabbMax = Vector3.Max(aabbMax, vertex);
            }

            var size = aabbMax - aabbMin;
            var quantum = size / 65535.0f;
            return quantum;
        }

        public BoundVertex GetQuantizedVertex(Vector3 vertex)
        {
            BoundVertex boundVertex = new BoundVertex();
            boundVertex.X = (short)MathF.Round(vertex.X / Quantum.X);
            boundVertex.Y = (short)MathF.Round(vertex.Y / Quantum.Y);
            boundVertex.Z = (short)MathF.Round(vertex.Z / Quantum.Z);
            return boundVertex;
        }

        public override void Rebuild()
        {
            base.Rebuild();

            if (Vertices is null || Primitives is null)
            {
                return;
            }

            Quantum = CalculateQuantum();
            ComputeTrianglesArea();
            ComputeTrianglesNeighbors();

            //ComputeShrunkVertices();
        }

        public void ComputeShrunkVertices()
        {
            if (ShrunkVertices is null)
                return;

            // Margin is used to shrink vertices
            // For triangles, they seem to be shrink along their average normal
            // OctantMap only seems to be present if ShrunkVertices are present too
            for (int i = 0; i < VerticesCount2; i++)
            {
                var vertex = GetVertexOffset(GetVertex(Vertices[i]));
                var shrunk = GetVertexOffset(GetVertex(ShrunkVertices[i]));

                var test = vertex - new Vector3(Margin);
            }
        }

        public void ComputeTrianglesArea()
        {
            var primitivesSpan = Primitives.AsSpan();

            for (int i = 0; i < PrimitivesCount; i++)
            {
                if (primitivesSpan[i].PrimitiveType != BoundPrimitiveType.Triangle)
                    continue;

                ref BoundPrimitiveTriangle triangle = ref primitivesSpan[i].AsTriangle();

                var vertex1 = GetVertex(Vertices[triangle.VertexIndex1]);
                var vertex2 = GetVertex(Vertices[triangle.VertexIndex2]);
                var vertex3 = GetVertex(Vertices[triangle.VertexIndex3]);

                triangle.Area = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).Length() * 0.5f;
            }
        }

        public void ComputeTrianglesNeighbors()
        {
            // Key: (vertex1, vertex2) 
            // Value: (triangle1, triangle2)
            Dictionary<ValueTuple<ushort, ushort>, ValueTuple<int, int>> edgesMap = new();
            ValueTuple<ushort, ushort>[] edges = new (ushort, ushort)[3];

            var primitivesSpan = Primitives.AsSpan();

            for (int i = 0; i < PrimitivesCount; i++)
            {
                if (primitivesSpan[i].PrimitiveType != BoundPrimitiveType.Triangle)
                    continue;

                ref BoundPrimitiveTriangle triangle = ref primitivesSpan[i].AsTriangle();

                edges[0] = GetEdge(triangle.VertexIndex1, triangle.VertexIndex2);
                edges[1] = GetEdge(triangle.VertexIndex2, triangle.VertexIndex3);
                edges[2] = GetEdge(triangle.VertexIndex3, triangle.VertexIndex1);

                for (int e = 0; e < 3; e++)
                {
                    if (edgesMap.ContainsKey(edges[e]))
                    {
                        var triangles = edgesMap[edges[e]];

                        edgesMap[edges[e]] = (triangles.Item1, i);
                    }
                    else
                        edgesMap[edges[e]] = (i, -1);
                }
            }

            for (int i = 0; i < PrimitivesCount; i++)
            {
                if (primitivesSpan[i].PrimitiveType != BoundPrimitiveType.Triangle)
                    continue;

                ref BoundPrimitiveTriangle triangle = ref primitivesSpan[i].AsTriangle();

                var edge1 = edgesMap[GetEdge(triangle.VertexIndex1, triangle.VertexIndex2)];
                var edge2 = edgesMap[GetEdge(triangle.VertexIndex2, triangle.VertexIndex3)];
                var edge3 = edgesMap[GetEdge(triangle.VertexIndex3, triangle.VertexIndex1)];

                triangle.NeighborIndex1 = (short)ChooseEdge(edge1, i);
                triangle.NeighborIndex2 = (short)ChooseEdge(edge2, i);
                triangle.NeighborIndex3 = (short)ChooseEdge(edge3, i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ValueTuple<ushort, ushort> GetEdge(ushort vertexIndex1, ushort vertexIndex2)
        {
            return vertexIndex1 < vertexIndex2 ? (vertexIndex1, vertexIndex2) : (vertexIndex2, vertexIndex1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ChooseEdge(ValueTuple<int, int> edge, int i)
        {
            return edge.Item2 != i ? edge.Item2 : edge.Item1 != i ? edge.Item1 : -1;
        }
    }
}
