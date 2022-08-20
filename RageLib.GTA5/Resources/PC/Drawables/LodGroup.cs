﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // rmcLodGroup
    public class LodGroup : ResourceSystemBlock
    {
        public override long BlockLength => 0x70;

        // structure data
        public Vector3 BoundingCenter;
        public float BoundingSphereRadius;
        public Vector4 BoundingBoxMin;
        public Vector4 BoundingBoxMax;
        private ulong LodHighPointer;
        private ulong LodMediumPointer;
        private ulong LodLowPointer;
        private ulong LodVeryLowPointer;
        public float LodDistanceHigh;
        public float LodDistanceMedium;
        public float LodDistanceLow;
        public float LodDistanceVeryLow;
        public uint DrawBucketMaskHigh;
        public uint DrawBucketMaskMedium;
        public uint DrawBucketMaskLow;
        public uint DrawBucketMaskVeryLow;

        // reference data
        public Lod? LodHigh { get; set; }
        public Lod? LodMedium { get; set; }
        public Lod? LodLow { get; set; }
        public Lod? LodVeryLow { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.BoundingCenter = reader.ReadVector3();
            this.BoundingSphereRadius = reader.ReadSingle();
            this.BoundingBoxMin = reader.ReadVector4();
            this.BoundingBoxMax = reader.ReadVector4();
            this.LodHighPointer = reader.ReadUInt64();
            this.LodMediumPointer = reader.ReadUInt64();
            this.LodLowPointer = reader.ReadUInt64();
            this.LodVeryLowPointer = reader.ReadUInt64();
            this.LodDistanceHigh = reader.ReadSingle();
            this.LodDistanceMedium = reader.ReadSingle();
            this.LodDistanceLow = reader.ReadSingle();
            this.LodDistanceVeryLow = reader.ReadSingle();
            this.DrawBucketMaskHigh = reader.ReadUInt32();
            this.DrawBucketMaskMedium = reader.ReadUInt32();
            this.DrawBucketMaskLow = reader.ReadUInt32();
            this.DrawBucketMaskVeryLow = reader.ReadUInt32();

            // read reference data
            this.LodHigh = reader.ReadBlockAt<Lod>(this.LodHighPointer);
            this.LodMedium = reader.ReadBlockAt<Lod>(this.LodMediumPointer);
            this.LodLow = reader.ReadBlockAt<Lod>(this.LodLowPointer);
            this.LodVeryLow = reader.ReadBlockAt<Lod>(this.LodVeryLowPointer);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.LodHighPointer = (ulong)(this.LodHigh?.BlockPosition ?? 0);
            this.LodMediumPointer = (ulong)(this.LodMedium?.BlockPosition ?? 0);
            this.LodLowPointer = (ulong)(this.LodLow?.BlockPosition ?? 0);
            this.LodVeryLowPointer = (ulong)(this.LodVeryLow?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.BoundingCenter);
            writer.Write(this.BoundingSphereRadius);
            writer.Write(this.BoundingBoxMin);
            writer.Write(this.BoundingBoxMax);
            writer.Write(this.LodHighPointer);
            writer.Write(this.LodMediumPointer);
            writer.Write(this.LodLowPointer);
            writer.Write(this.LodVeryLowPointer);
            writer.Write(this.LodDistanceHigh);
            writer.Write(this.LodDistanceMedium);
            writer.Write(this.LodDistanceLow);
            writer.Write(this.LodDistanceVeryLow);
            writer.Write(this.DrawBucketMaskHigh);
            writer.Write(this.DrawBucketMaskMedium);
            writer.Write(this.DrawBucketMaskLow);
            writer.Write(this.DrawBucketMaskVeryLow);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (LodHigh != null) list.Add(LodHigh);
            if (LodMedium != null) list.Add(LodMedium);
            if (LodLow != null) list.Add(LodLow);
            if (LodVeryLow != null) list.Add(LodVeryLow);
            return list.ToArray();
        }
    }
}
