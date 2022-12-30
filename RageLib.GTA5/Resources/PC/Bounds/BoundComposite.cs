// Copyright � Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    // phBoundComposite
    public class BoundComposite : Bound
    {
        public override long BlockLength => 0xB0;

        // structure data
        public PgRef64<ResourcePointerArray64<Bound>> Bounds;
        public PgRef64<SimpleArray<Matrix4x4>> CurrentMatrices;
        public PgRef64<SimpleArray<Matrix4x4>> LastMatrices;
        public PgRef64<SimpleArray<Aabb>> ChildBoundingBoxes;
        public PgRef64<SimpleArray<BoundCompositeFlags>> TypeAndIncludeFlags;
        public PgRef64<SimpleArray<BoundCompositeFlags>> OwnedTypeAndIncludeFlags;
        public ushort MaxNumBounds;
        public ushort NumBounds;
        public uint Unknown_A4h; // 0x00000000
        public PgRef64<BVH> BVH;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Bounds = reader.ReadPointer<ResourcePointerArray64<Bound>>(false);
            this.CurrentMatrices = reader.ReadPointer<SimpleArray<Matrix4x4>>(false);
            this.LastMatrices = reader.ReadPointer<SimpleArray<Matrix4x4>>(false);
            this.ChildBoundingBoxes = reader.ReadPointer<SimpleArray<Aabb>>(false);
            this.TypeAndIncludeFlags = reader.ReadPointer<SimpleArray<BoundCompositeFlags>>(false);
            this.OwnedTypeAndIncludeFlags = reader.ReadPointer<SimpleArray<BoundCompositeFlags>>(false);
            this.MaxNumBounds = reader.ReadUInt16();
            this.NumBounds = reader.ReadUInt16();
            this.Unknown_A4h = reader.ReadUInt32();
            this.BVH = reader.ReadPointer<BVH>();

            // read reference data
            this.Bounds.ReadReference(reader, this.MaxNumBounds);
            this.CurrentMatrices.ReadReference(reader, this.MaxNumBounds);
            this.LastMatrices.ReadReference(reader, this.MaxNumBounds);
            this.ChildBoundingBoxes.ReadReference(reader, this.MaxNumBounds);
            this.TypeAndIncludeFlags.ReadReference(reader, this.MaxNumBounds);
            this.OwnedTypeAndIncludeFlags.ReadReference(reader, this.MaxNumBounds);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.MaxNumBounds = (ushort)(this.Bounds.Data?.Count ?? 0);
            this.NumBounds = (ushort)(this.Bounds.Data?.Count ?? 0);

            // write structure data
            writer.Write(this.Bounds);
            writer.Write(this.CurrentMatrices);
            writer.Write(this.LastMatrices);
            writer.Write(this.ChildBoundingBoxes);
            writer.Write(this.TypeAndIncludeFlags);
            writer.Write(this.OwnedTypeAndIncludeFlags);
            writer.Write(this.MaxNumBounds);
            writer.Write(this.NumBounds);
            writer.Write(this.Unknown_A4h);
            writer.Write(this.BVH);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Bounds.Data != null) list.Add(Bounds.Data);
            if (CurrentMatrices.Data != null) list.Add(CurrentMatrices.Data);
            if (LastMatrices.Data != null) list.Add(LastMatrices.Data);
            if (ChildBoundingBoxes.Data != null) list.Add(ChildBoundingBoxes.Data);
            if (TypeAndIncludeFlags.Data != null) list.Add(TypeAndIncludeFlags.Data);
            if (OwnedTypeAndIncludeFlags.Data != null) list.Add(OwnedTypeAndIncludeFlags.Data);
            if (BVH.Data != null) list.Add(BVH.Data);
            return list.ToArray();
        }

        public override void Rebuild()
        {
            base.Rebuild();

            if (Bounds.Data?.data_items is null)
            {
                MaxNumBounds = 0;
                NumBounds = 0;
                CurrentMatrices = null;
                LastMatrices = null;
                ChildBoundingBoxes = null;
                TypeAndIncludeFlags = null;
                OwnedTypeAndIncludeFlags = null;
                return;
            }

            MaxNumBounds = (ushort)Bounds.Data.Count;
            NumBounds = MaxNumBounds;

            // TODO:    Try to reuse existing arrays if already of the required size  
            UpdateBoundingBoxes();

            // This should be invoked only if we aren't editing an existing asset, which might contain actual transform data!
            // TODO:    Once we have bounds wrappers, we should cache transform on loading so we can retrieve it on saving!
            //UpdateMatrices();
            //UpdateFlags();
        }

        private void UpdateBoundingBoxes()
        {
            Aabb[] boundingBoxes = new Aabb[NumBounds];

            for (int i = 0; i < NumBounds; i++)
            {
                var bound = Bounds.Data[i];
                var min = new Vector4(bound.BoundingBoxMin, BitConverter.UInt32BitsToSingle(bound.RefCount));
                var max = new Vector4(bound.BoundingBoxMax, bound.Margin);
                boundingBoxes[i] = new Aabb(min, max);
            }

            ChildBoundingBoxes = new SimpleArray<Aabb>(boundingBoxes);
        }

        private void UpdateMatrices()
        {
            var mat = Matrix4x4.Identity;
            mat.M14 = FloatHelpers.SignalingNaN;
            mat.M24 = FloatHelpers.SignalingNaN;
            mat.M34 = FloatHelpers.SignalingNaN;
            mat.M44 = FloatHelpers.SignalingNaN;

            Matrix4x4[] matrices = new Matrix4x4[NumBounds];
            matrices.AsSpan().Fill(mat);

            CurrentMatrices = new SimpleArray<Matrix4x4>(matrices);
            LastMatrices = CurrentMatrices;
        }

        private void UpdateFlags()
        {
            BoundCompositeFlags flags = new BoundCompositeFlags()
            { 
                Flags1 = BoundFlags.NONE,
                Flags2 = BoundFlags.NONE 
            };

            BoundCompositeFlags[] flagsArray = new BoundCompositeFlags[NumBounds];
            flagsArray.AsSpan().Fill(flags);
            OwnedTypeAndIncludeFlags = new SimpleArray<BoundCompositeFlags>(flagsArray);
            TypeAndIncludeFlags = OwnedTypeAndIncludeFlags;
        }
    }
}
