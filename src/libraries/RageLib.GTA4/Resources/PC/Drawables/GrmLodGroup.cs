// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class GrmLodGroup : ResourceSystemBlock
    {
        public override long BlockLength => 0x50;

        // structure data
        public Vector4 BoundingCenter;
        public Vector4 BoundingBoxMin;
        public Vector4 BoundingBoxMax;
        private uint LodHighPointer;
        private uint LodMediumPointer;
        private uint LodLowPointer;
        private uint LodVeryLowPointer;
        public float LodDistanceHigh;
        public float LodDistanceMedium;
        public float LodDistanceLow;
        public float LodDistanceVeryLow;
        public int DrawBucketMaskHigh;
        public int DrawBucketMaskMedium;
        public int DrawBucketMaskLow;
        public int DrawBucketMaskVeryLow;

        // reference data
        public RmcLod? LodHigh { get; set; }
        public RmcLod? LodMedium { get; set; }
        public RmcLod? LodLow { get; set; }
        public RmcLod? LodVeryLow { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            BoundingCenter = reader.ReadVector4();
            BoundingBoxMin = reader.ReadVector4();
            BoundingBoxMax = reader.ReadVector4();
            LodHighPointer = reader.ReadUInt32();
            LodMediumPointer = reader.ReadUInt32();
            LodLowPointer = reader.ReadUInt32();
            LodVeryLowPointer = reader.ReadUInt32();
            LodDistanceHigh = reader.ReadSingle();
            LodDistanceMedium = reader.ReadSingle();
            LodDistanceLow = reader.ReadSingle();
            LodDistanceVeryLow = reader.ReadSingle();
            DrawBucketMaskHigh = reader.ReadInt32();
            DrawBucketMaskMedium = reader.ReadInt32();
            DrawBucketMaskLow = reader.ReadInt32();
            DrawBucketMaskVeryLow = reader.ReadInt32();

            // read reference data
            LodHigh = reader.ReadBlockAt<RmcLod>(LodHighPointer);
            LodMedium = reader.ReadBlockAt<RmcLod>(LodMediumPointer);
            LodLow = reader.ReadBlockAt<RmcLod>(LodLowPointer);
            LodVeryLow = reader.ReadBlockAt<RmcLod>(LodVeryLowPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            LodHighPointer = (uint)(LodHigh?.BlockPosition ?? 0);
            LodMediumPointer = (uint)(LodMedium?.BlockPosition ?? 0);
            LodLowPointer = (uint)(LodLow?.BlockPosition ?? 0);
            LodVeryLowPointer = (uint)(LodVeryLow?.BlockPosition ?? 0);

            // write structure data
            writer.Write(BoundingCenter);
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

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (LodHigh is not null) list.Add(LodHigh);
            if (LodMedium is not null) list.Add(LodMedium);
            if (LodLow is not null) list.Add(LodLow);
            if (LodVeryLow is not null) list.Add(LodVeryLow);
            return list.ToArray();
        }
    }
}
