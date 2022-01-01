// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    public class RmcLodGroup : ResourceSystemBlock
    {
        public override long BlockLength => 0x70;

        // structure data
        public Vector3 BoundingCenter;
        public float BoundingSphereRadius;
        public Vector4 BoundingBoxMin;
        public Vector4 BoundingBoxMax;
        public ulong LodHighPointer;
        public ulong LodMediumPointer;
        public ulong LodLowPointer;
        public ulong LodVeryLowPointer;
        public float LodDistanceHigh;
        public float LodDistanceMedium;
        public float LodDistanceLow;
        public float LodDistanceVeryLow;
        public uint DrawBucketMaskHigh;
        public uint DrawBucketMaskMedium;
        public uint DrawBucketMaskLow;
        public uint DrawBucketMaskVeryLow;

        // reference data
        public RmcLod? LodHigh { get; set; }
        public RmcLod? LodMedium { get; set; }
        public RmcLod? LodLow { get; set; }
        public RmcLod? LodVeryLow { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            BoundingCenter = reader.ReadVector3();
            BoundingSphereRadius = reader.ReadSingle();
            BoundingBoxMin = reader.ReadVector4();
            BoundingBoxMax = reader.ReadVector4();
            LodHighPointer = reader.ReadUInt64();
            LodMediumPointer = reader.ReadUInt64();
            LodLowPointer = reader.ReadUInt64();
            LodVeryLowPointer = reader.ReadUInt64();
            LodDistanceHigh = reader.ReadSingle();
            LodDistanceMedium = reader.ReadSingle();
            LodDistanceLow = reader.ReadSingle();
            LodDistanceVeryLow = reader.ReadSingle();
            DrawBucketMaskHigh = reader.ReadUInt32();
            DrawBucketMaskMedium = reader.ReadUInt32();
            DrawBucketMaskLow = reader.ReadUInt32();
            DrawBucketMaskVeryLow = reader.ReadUInt32();

            // read reference data
            LodHigh = reader.ReadBlockAt<RmcLod>(LodHighPointer);
            LodMedium = reader.ReadBlockAt<RmcLod>(LodMediumPointer);
            LodLow = reader.ReadBlockAt<RmcLod>(LodLowPointer);
            LodVeryLow = reader.ReadBlockAt<RmcLod>(LodVeryLowPointer);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            LodHighPointer = (ulong)(LodHigh?.BlockPosition ?? 0);
            LodMediumPointer = (ulong)(LodMedium?.BlockPosition ?? 0);
            LodLowPointer = (ulong)(LodLow?.BlockPosition ?? 0);
            LodVeryLowPointer = (ulong)(LodVeryLow?.BlockPosition ?? 0);

            // write structure data
            writer.Write(BoundingCenter);
            writer.Write(BoundingSphereRadius);
            writer.Write(BoundingBoxMin);
            writer.Write(BoundingBoxMax);
            writer.Write(LodHighPointer);
            writer.Write(LodMediumPointer);
            writer.Write(LodLowPointer);
            writer.Write(LodVeryLowPointer);
            writer.Write(LodDistanceHigh);
            writer.Write(LodDistanceMedium);
            writer.Write(LodDistanceLow);
            writer.Write(LodDistanceVeryLow);
            writer.Write(DrawBucketMaskHigh);
            writer.Write(DrawBucketMaskMedium);
            writer.Write(DrawBucketMaskLow);
            writer.Write(DrawBucketMaskVeryLow);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (LodHigh is not null) list.Add(LodHigh);
            if (LodMedium is not null) list.Add(LodMedium);
            if (LodLow is not null) list.Add(LodLow);
            if (LodVeryLow is not null) list.Add(LodVeryLow);
            return list.ToArray();
        }
    }
}
