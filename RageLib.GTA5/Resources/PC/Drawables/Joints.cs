// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // pgBase
    // crJointData
    public class Joints : PgBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong RotationLimitsPointer;
        public ulong TranslationLimitsPointer;
        public ulong ScaleLimitsPointer; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ushort RotationLimitsCount;
        public ushort TranslationLimitsCount;
        public ushort ScaleLimitsCount; // 0x0000
        public ushort Unknown_36h; // 0x0001
        public ulong Unknown_38h; // 0x0000000000000000

        // reference data
        public SimpleArray<JointRotationLimit>? RotationLimits { get; set; }
        public SimpleArray<JointTranslationLimit>? TranslationLimits { get; set; }
        public SimpleArray<JointScaleLimit>? ScaleLimits { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.RotationLimitsPointer = reader.ReadUInt64();
            this.TranslationLimitsPointer = reader.ReadUInt64();
            this.ScaleLimitsPointer = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.RotationLimitsCount = reader.ReadUInt16();
            this.TranslationLimitsCount = reader.ReadUInt16();
            this.ScaleLimitsCount = reader.ReadUInt16();
            this.Unknown_36h = reader.ReadUInt16();
            this.Unknown_38h = reader.ReadUInt64();

            // read reference data
            this.RotationLimits = reader.ReadBlockAt<SimpleArray<JointRotationLimit>>(
                this.RotationLimitsPointer, // offset
                this.RotationLimitsCount
            );
            this.TranslationLimits = reader.ReadBlockAt<SimpleArray<JointTranslationLimit>>(
                this.TranslationLimitsPointer, // offset
                this.TranslationLimitsCount
            );
            this.ScaleLimits = reader.ReadBlockAt<SimpleArray<JointScaleLimit>>(
                this.ScaleLimitsPointer, // offset
                this.ScaleLimitsCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.RotationLimitsPointer = (ulong)(this.RotationLimits?.BlockPosition ?? 0);
            this.TranslationLimitsPointer = (ulong)(this.TranslationLimits?.BlockPosition ?? 0);
            this.ScaleLimitsPointer = (ulong)(this.ScaleLimits?.BlockPosition ?? 0);
            this.RotationLimitsCount = (ushort)(this.RotationLimits?.Count ?? 0);
            this.TranslationLimitsCount = (ushort)(this.TranslationLimits?.Count ?? 0);
            this.ScaleLimitsCount = (ushort)(this.ScaleLimits?.Count ?? 0);

            // write structure data
            writer.Write(this.RotationLimitsPointer);
            writer.Write(this.TranslationLimitsPointer);
            writer.Write(this.ScaleLimitsPointer);
            writer.Write(this.Unknown_28h);
            writer.Write(this.RotationLimitsCount);
            writer.Write(this.TranslationLimitsCount);
            writer.Write(this.ScaleLimitsCount);
            writer.Write(this.Unknown_36h);
            writer.Write(this.Unknown_38h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (RotationLimits != null) list.Add(RotationLimits);
            if (TranslationLimits != null) list.Add(TranslationLimits);
            if (ScaleLimits != null) list.Add(ScaleLimits);
            return list.ToArray();
        }
    }
}
