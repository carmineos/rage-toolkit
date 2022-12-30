// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
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
        public uint Unknown_70h;
        public uint Unknown_74h;
        public PgRef64<SimpleArray<BoundVertex>> ShrunkVertices;
        public uint Unknown_80h;
        public uint VerticesCount1;
        public PgRef64<SimpleArray<BoundPrimitive>> Primitives;
        public Vector3 Quantum;
        public float Unknown_9Ch;
        public Vector3 Offset;
        public float Unknown_ACh;
        public PgRef64<SimpleArray<BoundVertex>> Vertices;
        public PgRef64<SimpleArray<uint>> VerticesColors;
        public PgRef64<SimpleArray<uint>> Unknown_C0h_Data;
        public PgRef64<SimpleArrayArray64<uint>> Unknown_C8h_Data;
        public uint VerticesCount2;
        public uint PrimitivesCount;
        public ulong Unknown_D8h; // 0x0000000000000000
        public ulong Unknown_E0h; // 0x0000000000000000
        public ulong Unknown_E8h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_70h = reader.ReadUInt32();
            this.Unknown_74h = reader.ReadUInt32();
            this.ShrunkVertices = reader.ReadPointer<SimpleArray<BoundVertex>>(false);
            this.Unknown_80h = reader.ReadUInt32();
            this.VerticesCount1 = reader.ReadUInt32();
            this.Primitives = reader.ReadPointer<SimpleArray<BoundPrimitive>>(false);
            this.Quantum = reader.ReadVector3();
            this.Unknown_9Ch = reader.ReadSingle();
            this.Offset = reader.ReadVector3();
            this.Unknown_ACh = reader.ReadSingle();
            this.Vertices = reader.ReadPointer<SimpleArray<BoundVertex>>(false);
            this.VerticesColors = reader.ReadPointer<SimpleArray<uint>>(false);
            this.Unknown_C0h_Data = reader.ReadPointer<SimpleArray<uint>>(false);
            this.Unknown_C8h_Data = reader.ReadPointer<SimpleArrayArray64<uint>>(false);
            this.VerticesCount2 = reader.ReadUInt32();
            this.PrimitivesCount = reader.ReadUInt32();
            this.Unknown_D8h = reader.ReadUInt64();
            this.Unknown_E0h = reader.ReadUInt64();
            this.Unknown_E8h = reader.ReadUInt64();

            // read reference data
            this.ShrunkVertices.ReadReference(reader, this.VerticesCount2);
            this.Primitives.ReadReference(reader, this.PrimitivesCount);
            this.Vertices.ReadReference(reader, this.VerticesCount2);
            this.VerticesColors.ReadReference(reader, this.VerticesCount2);
            this.Unknown_C0h_Data.ReadReference(reader, 8);
            this.Unknown_C8h_Data.ReadReference(reader, 8, this.Unknown_C0h_Data);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.VerticesCount1 = (uint)(this.Vertices.Data?.Count ?? 0);
            this.VerticesCount2 = (uint)(this.Vertices.Data?.Count ?? 0);
            this.PrimitivesCount = (uint)(this.Primitives.Data?.Count ?? 0);

            // write structure data
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_74h);
            writer.Write(this.ShrunkVertices);
            writer.Write(this.Unknown_80h);
            writer.Write(this.VerticesCount1);
            writer.Write(this.Primitives);
            writer.Write(this.Quantum);
            writer.Write(this.Unknown_9Ch);
            writer.Write(this.Offset);
            writer.Write(this.Unknown_ACh);
            writer.Write(this.Vertices);
            writer.Write(this.VerticesColors);
            writer.Write(this.Unknown_C0h_Data);
            writer.Write(this.Unknown_C8h_Data);
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
            if (ShrunkVertices.Data != null) list.Add(ShrunkVertices.Data);
            if (Primitives.Data != null) list.Add(Primitives.Data);
            if (Vertices.Data != null) list.Add(Vertices.Data);
            if (VerticesColors.Data != null) list.Add(VerticesColors.Data);
            if (Unknown_C0h_Data.Data != null) list.Add(Unknown_C0h_Data.Data);
            if (Unknown_C8h_Data.Data != null) list.Add(Unknown_C8h_Data.Data);
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
            var quantizedVertex = Vertices.Data[index];
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
                var vertex = GetVertex(Vertices.Data[i]);

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

            if(Vertices.Data is null || Primitives.Data is null)
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
            if (ShrunkVertices.Data is null)
                return;

            // Margin is used to shrink vertices
            // For triangles, they seem to be shrink along their average normal
            // OctantMap only seems to be present if ShrunkVertices are present too
            for (int i = 0; i < VerticesCount2; i++)
            {
                var vertex = GetVertexOffset(GetVertex(Vertices.Data[i]));
                var shrunk = GetVertexOffset(GetVertex(ShrunkVertices.Data[i]));

                var test = vertex - new Vector3(Margin);
            }
        }

        public void ComputeTrianglesArea()
        {
            var primitivesSpan = Primitives.Data.AsSpan();

            for (int i = 0; i < PrimitivesCount; i++)
            {
                if (primitivesSpan[i].PrimitiveType != BoundPrimitiveType.Triangle)
                    continue;

                ref BoundPrimitiveTriangle triangle = ref primitivesSpan[i].AsTriangle();
                
                var vertex1 = GetVertex(Vertices.Data[triangle.VertexIndex1]);
                var vertex2 = GetVertex(Vertices.Data[triangle.VertexIndex2]);
                var vertex3 = GetVertex(Vertices.Data[triangle.VertexIndex3]);

                triangle.Area = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).Length() * 0.5f;
            }
        }

        public void ComputeTrianglesNeighbors()
        {
            // Key: (vertex1, vertex2) 
            // Value: (triangle1, triangle2)
            Dictionary <ValueTuple<ushort, ushort>, ValueTuple<int, int>> edgesMap = new();
            ValueTuple<ushort, ushort>[] edges = new (ushort, ushort)[3];

            var primitivesSpan = Primitives.Data.AsSpan();

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
