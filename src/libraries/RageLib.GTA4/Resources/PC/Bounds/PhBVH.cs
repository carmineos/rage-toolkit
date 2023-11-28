// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBVH : ResourceSystemBlock
    {
        public override long BlockLength => 0x58;

        // structure data
        public SimpleBigList32<BVHNode> Nodes;
        public Vector4 BoundingBoxMin;
        public Vector4 BoundingBoxMax;
        public Vector4 BoundingBoxCenter;
        public Vector4 Quantum;
        public SimpleList32<BVHTreeInfo> Trees;

        public PhBVH()
        {
            Nodes = new SimpleBigList32<BVHNode>();
            Trees = new SimpleList32<BVHTreeInfo>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Nodes = reader.ReadBlock<SimpleBigList32<BVHNode>>();
            BoundingBoxMin = reader.ReadVector4();
            BoundingBoxMax = reader.ReadVector4();
            BoundingBoxCenter = reader.ReadVector4();
            Quantum = reader.ReadVector4();
            Trees = reader.ReadBlock<SimpleList32<BVHTreeInfo>>();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(Nodes);
            writer.Write(BoundingBoxMin);
            writer.Write(BoundingBoxMax);
            writer.Write(BoundingBoxCenter);
            writer.Write(Quantum);
            writer.WriteBlock(Trees);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x0, Nodes),
                new Tuple<long, IResourceBlock>(0x50, Trees),
            };
        }
    }
}
