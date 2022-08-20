// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using RageLib.GTA5.Resources.PC.Drawables;
using RageLib.Resources.Common;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // pgBase
    // crSkeletonData
    public class SkeletonData : PgBase64
    {
        public override long BlockLength => 0x70;

        // structure data
        public HashMap BoneMap;
        private ulong BoneDataPointer; // why this points to the array directly ?
        private ulong InverseBindPoseMatricesPointer;
        private ulong DefaultPoseMatricesPointer;
        private ulong ParentIndicesPointer;
        private ulong ChildrenIndicesPointer;
        public ulong Unknown_48h; // 0x0000000000000000
        public uint Unknown_50h;
        public uint Unknown_54h;
        public uint DataCRC;
        public ushort Unknown_5Ch; // 0x0001
        public ushort BonesCount;
        public ushort ChildrenIndicesCount;
        public ushort Unknown_62h; // 0x0000
        public uint Unknown_64h; // 0x00000000
        public ulong Unknown_68h; // 0x0000000000000000

        // reference data
        public BoneData? BoneData { get; set; }
        public SimpleArray<Matrix4x4>? InverseBindPoseMatrices { get; set; }
        public SimpleArray<Matrix4x4>? DefaultPoseMatrices { get; set; }
        public SimpleArray<short>? ParentIndices { get; set; }
        public SimpleArray<short>? ChildrenIndices { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.BoneMap = reader.ReadBlock<HashMap>();
            this.BoneDataPointer = reader.ReadUInt64();
            this.InverseBindPoseMatricesPointer = reader.ReadUInt64();
            this.DefaultPoseMatricesPointer = reader.ReadUInt64();
            this.ParentIndicesPointer = reader.ReadUInt64();
            this.ChildrenIndicesPointer = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadUInt32();
            this.DataCRC = reader.ReadUInt32();
            this.Unknown_5Ch = reader.ReadUInt16();
            this.BonesCount = reader.ReadUInt16();
            this.ChildrenIndicesCount = reader.ReadUInt16();
            this.Unknown_62h = reader.ReadUInt16();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt64();

            // read reference data
            this.BoneData = reader.ReadBlockAt<BoneData>(
                this.BoneDataPointer - 16, // offset
                this.BonesCount
            );
            this.InverseBindPoseMatrices = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(
                this.InverseBindPoseMatricesPointer, // offset
                this.BonesCount
            );
            this.DefaultPoseMatrices = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(
                this.DefaultPoseMatricesPointer, // offset
                this.BonesCount
            );
            this.ParentIndices = reader.ReadBlockAt<SimpleArray<short>>(
                this.ParentIndicesPointer, // offset
                this.BonesCount
            );
            this.ChildrenIndices = reader.ReadBlockAt<SimpleArray<short>>(
                this.ChildrenIndicesPointer, // offset
                this.ChildrenIndicesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.BoneDataPointer = (ulong)(this.BoneData != null ? this.BoneData.BlockPosition + 16 : 0);
            this.InverseBindPoseMatricesPointer = (ulong)(this.InverseBindPoseMatrices?.BlockPosition ?? 0);
            this.DefaultPoseMatricesPointer = (ulong)(this.DefaultPoseMatrices?.BlockPosition ?? 0);
            this.ParentIndicesPointer = (ulong)(this.ParentIndices?.BlockPosition ?? 0);
            this.ChildrenIndicesPointer = (ulong)(this.ChildrenIndices?.BlockPosition ?? 0);
            this.BonesCount = (ushort)(this.BoneData?.BonesCount ?? 0);
            this.ChildrenIndicesCount = (ushort)(this.ChildrenIndices?.Count ?? 0);

            // write structure data
            writer.WriteBlock(this.BoneMap);
            writer.Write(this.BoneDataPointer);
            writer.Write(this.InverseBindPoseMatricesPointer);
            writer.Write(this.DefaultPoseMatricesPointer);
            writer.Write(this.ParentIndicesPointer);
            writer.Write(this.ChildrenIndicesPointer);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.DataCRC);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.BonesCount);
            writer.Write(this.ChildrenIndicesCount);
            writer.Write(this.Unknown_62h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (BoneData != null) list.Add(BoneData);
            if (InverseBindPoseMatrices != null) list.Add(InverseBindPoseMatrices);
            if (DefaultPoseMatrices != null) list.Add(DefaultPoseMatrices);
            if (ParentIndices != null) list.Add(ParentIndices);
            if (ChildrenIndices != null) list.Add(ChildrenIndices);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            var list = new List<Tuple<long, IResourceBlock>>(base.GetParts());
            list.Add(new Tuple<long, IResourceBlock>(0x10, BoneMap));
            return list.ToArray();
        }

        public override void Rebuild()
        {
            base.Rebuild();

            if(BoneData?.Bones is null)
            {
                BonesCount = 0;
                ChildrenIndicesCount = 0;
                InverseBindPoseMatrices = null;
                DefaultPoseMatrices = null;
                ParentIndices = null;
                ChildrenIndices = null;
                return;
            }

            ComputeBonesId();
            ComputeBoneMap();
            ComputeIndices();
            ComputeBoneTransformations();
        }

        private void ComputeBonesId()
        {
            if (BoneData?.Bones is null)
                return;

            foreach (var bone in BoneData.Bones)
            {
                // id of root bone seems to always be 0 
                if (bone.ParentIndex == -1)
                {
                    bone.BoneId = 0;
                    continue;
                }

                string name = bone.Name?.Value;

                if (string.IsNullOrEmpty(name))
                    bone.BoneId = 0;
                else
                    bone.BoneId = Enum.TryParse(name, false, out PedBoneId id) ? (ushort)id : Bone.CalculateBoneIdFromName(name);
            }
        }

        private void ComputeBoneMap()
        {
            if (BoneData?.Bones is null)
                return;

            List<KeyValuePair<uint, uint>> bonesIndexId = new List<KeyValuePair<uint, uint>>((int)BoneData.BonesCount);

            foreach (var bone in BoneData.Bones)
                bonesIndexId.Add(new KeyValuePair<uint, uint>(bone.BoneId, (uint)bone.Index));

            BoneMap = new HashMap(bonesIndexId);
        }

        public void ComputeIndices()
        {
            if (BoneData?.Bones is null)
                return;

            // Build ParentIndices array
            var parentIndices = new short[BonesCount];

            for (int i = 0; i < BonesCount; i++)
            {
                var bone = BoneData.Bones[i];
                parentIndices[i] = bone.ParentIndex;
                Debug.Assert(parentIndices[i] == ParentIndices[i]);
            }

            ParentIndices = new SimpleArray<short>(parentIndices);

            // Build ChildrenIndices array
            // TODO: try to replicate original order
            //       does order matter or they are just (short, short) tuples?
            List<short> childrenIndices = new List<short>();
            for (int i = 0; i < BonesCount; i++)
            {
                if(ParentIndices[i] != -1)
                {
                    childrenIndices.Add((short)i);
                    childrenIndices.Add(ParentIndices[i]);
                }
            }

            ChildrenIndices = new SimpleArray<short>(childrenIndices.ToArray());
        }

        public void ComputeBoneTransformations()
        {
            if (BoneData?.Bones is null)
                return;

            var localTransformations = new Matrix4x4[BonesCount];
            var worldTransformationsInverted = new Matrix4x4[BonesCount];

            for (int i = 0; i < BonesCount; i++)
            {
                var bone = BoneData.Bones[i];

                // Get Local Transform Matrix
                var localMatrix = GetLocalMatrix(bone);
                
                // Get World Transform Matrix
                var worldMatrix = GetWorldMatrix(bone);
                Matrix4x4.Invert(worldMatrix, out Matrix4x4 worldMatrixInverted);

                // TODO: Find out how 4th column is calculated
                //       In some cases M24 and M34 aren't accurate
                // EDIT: This might be just junk data which comes
                //       out of SIMD instructions with the data being just a Matrix4x3
                //       since Translation and Scale are padded to Vector4 in Bone
                localMatrix.M14 = 0f;
                localMatrix.M24 = 4f;
                localMatrix.M34 = -3f;
                localMatrix.M44 = 0f;
                worldMatrixInverted.M14 = 0f;
                worldMatrixInverted.M24 = 0f;
                worldMatrixInverted.M34 = 0f;
                worldMatrixInverted.M44 = 0f;

                localTransformations[i] = localMatrix;
                worldTransformationsInverted[i] = worldMatrixInverted;

                //var oldMat = DefaultPoseMatrices[i];
                //var oldMatInv = InverseBindMatrices[i];
                //Debug.Assert(MatrixAlmostEquals(localMatrix, oldMat));
                //Debug.Assert(MatrixAlmostEquals(worldMatrix, oldMat));
                //Debug.Assert(MatrixAlmostEquals(worldMatrixInverted, oldMatInv));

                //bool MatrixAlmostEquals(Matrix4x4 m1, Matrix4x4 m2, float epsilon = 0.001f)
                //{
                //    return
                //        (MathF.Abs(m1.M11 - m2.M11) < epsilon) &&
                //        (MathF.Abs(m1.M12 - m2.M12) < epsilon) &&
                //        (MathF.Abs(m1.M13 - m2.M13) < epsilon) &&
                //        (MathF.Abs(m1.M14 - m2.M14) < epsilon) &&
                //        (MathF.Abs(m1.M21 - m2.M21) < epsilon) &&
                //        (MathF.Abs(m1.M22 - m2.M22) < epsilon) &&
                //        (MathF.Abs(m1.M23 - m2.M23) < epsilon) &&
                //        (MathF.Abs(m1.M24 - m2.M24) < epsilon) &&
                //        (MathF.Abs(m1.M31 - m2.M31) < epsilon) &&
                //        (MathF.Abs(m1.M32 - m2.M32) < epsilon) &&
                //        (MathF.Abs(m1.M33 - m2.M33) < epsilon) &&
                //        (MathF.Abs(m1.M34 - m2.M34) < epsilon) &&
                //        (MathF.Abs(m1.M41 - m2.M41) < epsilon) &&
                //        (MathF.Abs(m1.M42 - m2.M42) < epsilon) &&
                //        (MathF.Abs(m1.M43 - m2.M43) < epsilon) &&
                //        (MathF.Abs(m1.M44 - m2.M44) < epsilon);
                //}
            }

            DefaultPoseMatrices = new SimpleArray<Matrix4x4>(localTransformations);
            InverseBindPoseMatrices = new SimpleArray<Matrix4x4>(worldTransformationsInverted);
        }

        // TODO: Use a wrapper class to cache transforms and parent of each Bone
        public Matrix4x4 GetLocalMatrix(Bone bone)
        {
            return Matrix4x4.CreateScale(bone.Scale) * Matrix4x4.CreateFromQuaternion(bone.Rotation) * Matrix4x4.CreateTranslation(bone.Translation);
        }

        public Matrix4x4 GetWorldMatrix(Bone bone)
        {
            var localMatrix = GetLocalMatrix(bone);

            var parentIndex = bone.ParentIndex;

            if (parentIndex != -1)
            {
                var parentBone = BoneData?.Bones[parentIndex];
                var parentWorldMatrix = GetWorldMatrix(parentBone);
                return localMatrix * parentWorldMatrix;
            }

            return localMatrix;
        }
    }
}
