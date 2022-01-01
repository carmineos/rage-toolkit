// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    public class BVH : ResourceSystemBlock
    {
        public override long BlockLength => 0x80;

        // structure data
        public SimpleBigList64<BVHNode> Nodes;
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h; // 0x0000000000000000
        public Vector4 BoundingBoxMin;
        public Vector4 BoundingBoxMax;
        public Vector4 BoundingBoxCenter;
        public Vector4 QuantumInverse;
        public Vector4 Quantum; // bounding box dimension / 2^16
        public SimpleList64<BVHTreeInfo> Trees;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Nodes = reader.ReadBlock<SimpleBigList64<BVHNode>>();
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.BoundingBoxMin = reader.ReadVector4();
            this.BoundingBoxMax = reader.ReadVector4();
            this.BoundingBoxCenter = reader.ReadVector4();
            this.QuantumInverse = reader.ReadVector4();
            this.Quantum = reader.ReadVector4();
            this.Trees = reader.ReadBlock<SimpleList64<BVHTreeInfo>>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.WriteBlock(this.Nodes);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.BoundingBoxMin);
            writer.Write(this.BoundingBoxMax);
            writer.Write(this.BoundingBoxCenter);
            writer.Write(this.QuantumInverse);
            writer.Write(this.Quantum);
            writer.WriteBlock(this.Trees);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x00, Nodes),
                new Tuple<long, IResourceBlock>(0x70, Trees)
            };
        }
    }
}
