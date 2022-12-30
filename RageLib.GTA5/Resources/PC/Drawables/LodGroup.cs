// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

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
        public PgRef64<Lod> LodHigh;
        public PgRef64<Lod> LodMedium;
        public PgRef64<Lod> LodLow;
        public PgRef64<Lod> LodVeryLow;
        public float LodDistanceHigh;
        public float LodDistanceMedium;
        public float LodDistanceLow;
        public float LodDistanceVeryLow;
        public LodDrawBucketMask DrawBucketMaskHigh;
        public LodDrawBucketMask DrawBucketMaskMedium;
        public LodDrawBucketMask DrawBucketMaskLow;
        public LodDrawBucketMask DrawBucketMaskVeryLow;

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
            this.LodHigh = reader.ReadPointer<Lod>();
            this.LodMedium = reader.ReadPointer<Lod>();
            this.LodLow = reader.ReadPointer<Lod>();
            this.LodVeryLow = reader.ReadPointer<Lod>();
            this.LodDistanceHigh = reader.ReadSingle();
            this.LodDistanceMedium = reader.ReadSingle();
            this.LodDistanceLow = reader.ReadSingle();
            this.LodDistanceVeryLow = reader.ReadSingle();
            this.DrawBucketMaskHigh = reader.ReadUInt32();
            this.DrawBucketMaskMedium = reader.ReadUInt32();
            this.DrawBucketMaskLow = reader.ReadUInt32();
            this.DrawBucketMaskVeryLow = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.BoundingCenter);
            writer.Write(this.BoundingSphereRadius);
            writer.Write(this.BoundingBoxMin);
            writer.Write(this.BoundingBoxMax);
            writer.Write(this.LodHigh);
            writer.Write(this.LodMedium);
            writer.Write(this.LodLow);
            writer.Write(this.LodVeryLow);
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
            if (LodHigh.Data is not null) list.Add(LodHigh.Data);
            if (LodMedium.Data is not null) list.Add(LodMedium.Data);
            if (LodLow.Data is not null) list.Add(LodLow.Data);
            if (LodVeryLow.Data is not null) list.Add(LodVeryLow.Data);
            return list.ToArray();
        }

        public override void Rebuild()
        {
            base.Rebuild();

            ComputeDrawBucketMasks();
        }

        private void ComputeDrawBucketMasks()
        {
            DrawBucketMaskHigh.Mask = GetDrawBucketMaskForLod(LodHigh);
            DrawBucketMaskMedium.Mask = GetDrawBucketMaskForLod(LodMedium);
            DrawBucketMaskLow.Mask = GetDrawBucketMaskForLod(LodLow);
            DrawBucketMaskVeryLow.Mask = GetDrawBucketMaskForLod(LodVeryLow);
        }

        private byte GetDrawBucketMaskForLod(Lod? lod)
        {
            byte mask = 0x0;

            var models = lod?.Models?.Entries;

            if (models is null)
                return mask;

            foreach(var model in models)
            {
                mask |= model.Mask;
            }

            return mask;
        }
    }
}
