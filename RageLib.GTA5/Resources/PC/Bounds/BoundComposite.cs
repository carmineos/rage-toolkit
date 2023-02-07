// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common.Collections;
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
        public ulong BoundsPointer;
        public ulong CurrentMatricesPointer;
        public ulong LastMatricesPointer;
        public ulong ChildBoundingBoxesPointer;
        public ulong TypeAndIncludeFlagsPointer;
        public ulong OwnedTypeAndIncludeFlagsPointer;
        public ushort MaxNumBounds;
        public ushort NumBounds;
        private uint Unknown_A4h; // 0x00000000
        public ulong BVHPointer;

        // reference data
        public ResourcePointerArray64<Bound>? Bounds { get; set; }
        public SimpleArray<Matrix4x4>? CurrentMatrices { get; set; }
        public SimpleArray<Matrix4x4>? LastMatrices { get; set; }
        public SimpleArray<Aabb>? ChildBoundingBoxes { get; set; }
        public SimpleArray<BoundCompositeFlags>? TypeAndIncludeFlags { get; set; }
        public SimpleArray<BoundCompositeFlags>? OwnedTypeAndIncludeFlags { get; set; }
        public BVH? BVH { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.BoundsPointer = reader.ReadUInt64();
            this.CurrentMatricesPointer = reader.ReadUInt64();
            this.LastMatricesPointer = reader.ReadUInt64();
            this.ChildBoundingBoxesPointer = reader.ReadUInt64();
            this.TypeAndIncludeFlagsPointer = reader.ReadUInt64();
            this.OwnedTypeAndIncludeFlagsPointer = reader.ReadUInt64();
            this.MaxNumBounds = reader.ReadUInt16();
            this.NumBounds = reader.ReadUInt16();
            this.Unknown_A4h = reader.ReadUInt32();
            this.BVHPointer = reader.ReadUInt64();

            // read reference data
            this.Bounds = reader.ReadBlockAt<ResourcePointerArray64<Bound>>(
                this.BoundsPointer, // offset
                this.MaxNumBounds
            );
            this.CurrentMatrices = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(
                this.CurrentMatricesPointer, // offset
                this.MaxNumBounds
            );
            this.LastMatrices = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(
                this.LastMatricesPointer, // offset
                this.MaxNumBounds
            );
            this.ChildBoundingBoxes = reader.ReadBlockAt<SimpleArray<Aabb>>(
                this.ChildBoundingBoxesPointer, // offset
                this.MaxNumBounds
            );
            this.TypeAndIncludeFlags = reader.ReadBlockAt<SimpleArray<BoundCompositeFlags>>(
                this.TypeAndIncludeFlagsPointer, // offset
                this.MaxNumBounds
            );
            this.OwnedTypeAndIncludeFlags = reader.ReadBlockAt<SimpleArray<BoundCompositeFlags>>(
                this.OwnedTypeAndIncludeFlagsPointer, // offset
                this.MaxNumBounds
            );
            this.BVH = reader.ReadBlockAt<BVH>(
                this.BVHPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.BoundsPointer = (ulong)(this.Bounds?.BlockPosition ?? 0);
            this.CurrentMatricesPointer = (ulong)(this.CurrentMatrices?.BlockPosition ?? 0);
            this.LastMatricesPointer = (ulong)(this.LastMatrices?.BlockPosition ?? 0);
            this.ChildBoundingBoxesPointer = (ulong)(this.ChildBoundingBoxes?.BlockPosition ?? 0);
            this.TypeAndIncludeFlagsPointer = (ulong)(this.TypeAndIncludeFlags?.BlockPosition ?? 0);
            this.OwnedTypeAndIncludeFlagsPointer = (ulong)(this.OwnedTypeAndIncludeFlags?.BlockPosition ?? 0);
            this.MaxNumBounds = (ushort)(this.Bounds?.Count ?? 0);
            this.NumBounds = (ushort)(this.Bounds?.Count ?? 0);
            this.BVHPointer = (ulong)(this.BVH?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.BoundsPointer);
            writer.Write(this.CurrentMatricesPointer);
            writer.Write(this.LastMatricesPointer);
            writer.Write(this.ChildBoundingBoxesPointer);
            writer.Write(this.TypeAndIncludeFlagsPointer);
            writer.Write(this.OwnedTypeAndIncludeFlagsPointer);
            writer.Write(this.MaxNumBounds);
            writer.Write(this.NumBounds);
            writer.Write(this.Unknown_A4h);
            writer.Write(this.BVHPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Bounds != null) list.Add(Bounds);
            if (CurrentMatrices != null) list.Add(CurrentMatrices);
            if (LastMatrices != null) list.Add(LastMatrices);
            if (ChildBoundingBoxes != null) list.Add(ChildBoundingBoxes);
            if (TypeAndIncludeFlags != null) list.Add(TypeAndIncludeFlags);
            if (OwnedTypeAndIncludeFlags != null) list.Add(OwnedTypeAndIncludeFlags);
            if (BVH != null) list.Add(BVH);
            return list.ToArray();
        }

        public override void Rebuild()
        {
            base.Rebuild();

            if (Bounds?.data_items is null)
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

            MaxNumBounds = (ushort)Bounds.Count;
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
                var bound = Bounds[i];
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
