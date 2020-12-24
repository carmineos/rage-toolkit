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
        public Ref<ResourcePointerList64<DrawableModel>> DrawableModelsHigh;
        public Ref<ResourcePointerList64<DrawableModel>> DrawableModelsMedium;
        public Ref<ResourcePointerList64<DrawableModel>> DrawableModelsLow;
        public Ref<ResourcePointerList64<DrawableModel>> DrawableModelsVeryLow;
        public float LodDistanceHigh;
        public float LodDistanceMedium;
        public float LodDistanceLow;
        public float LodDistanceVeryLow;
        public uint DrawBucketMaskHigh;
        public uint DrawBucketMaskMedium;
        public uint DrawBucketMaskLow;
        public uint DrawBucketMaskVeryLow;

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
            this.DrawableModelsHigh = reader.ReadUInt64();
            this.DrawableModelsMedium = reader.ReadUInt64();
            this.DrawableModelsLow = reader.ReadUInt64();
            this.DrawableModelsVeryLow = reader.ReadUInt64();
            this.LodDistanceHigh = reader.ReadSingle();
            this.LodDistanceMedium = reader.ReadSingle();
            this.LodDistanceLow = reader.ReadSingle();
            this.LodDistanceVeryLow = reader.ReadSingle();
            this.DrawBucketMaskHigh = reader.ReadUInt32();
            this.DrawBucketMaskMedium = reader.ReadUInt32();
            this.DrawBucketMaskLow = reader.ReadUInt32();
            this.DrawBucketMaskVeryLow = reader.ReadUInt32();

            // read reference data
            this.DrawableModelsHigh.ReadBlock(reader);
            this.DrawableModelsMedium.ReadBlock(reader);
            this.DrawableModelsLow.ReadBlock(reader);
            this.DrawableModelsVeryLow.ReadBlock(reader);
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
            writer.Write(this.DrawableModelsHigh);
            writer.Write(this.DrawableModelsMedium);
            writer.Write(this.DrawableModelsLow);
            writer.Write(this.DrawableModelsVeryLow);
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
            if (DrawableModelsHigh.Data != null) list.Add(DrawableModelsHigh.Data);
            if (DrawableModelsMedium.Data != null) list.Add(DrawableModelsMedium.Data);
            if (DrawableModelsLow.Data != null) list.Add(DrawableModelsLow.Data);
            if (DrawableModelsVeryLow.Data != null) list.Add(DrawableModelsVeryLow.Data);
            return list.ToArray();
        }
    }
}
