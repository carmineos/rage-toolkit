// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // pgBase
    // crJointData
    public class Joints : PgBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        private ulong RotationLimitsPointer;
        private ulong TranslationLimitsPointer;
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000
        public ushort RotationLimitsCount;
        public ushort TranslationLimitsCount;
        public ushort Unknown_34h; // 0x0000
        public ushort Unknown_36h; // 0x0001
        public uint Unknown_38h; // 0x00000000
        public uint Unknown_3Ch; // 0x00000000

        // reference data
        public ResourceSimpleArray<JointRotationLimit>? RotationLimits { get; set; }
        public ResourceSimpleArray<JointTranslationLimit>? TranslationLimits { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.RotationLimitsPointer = reader.ReadUInt64();
            this.TranslationLimitsPointer = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.RotationLimitsCount = reader.ReadUInt16();
            this.TranslationLimitsCount = reader.ReadUInt16();
            this.Unknown_34h = reader.ReadUInt16();
            this.Unknown_36h = reader.ReadUInt16();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();

            // read reference data
            this.RotationLimits = reader.ReadBlockAt<ResourceSimpleArray<JointRotationLimit>>(
                this.RotationLimitsPointer, // offset
                this.RotationLimitsCount
            );
            this.TranslationLimits = reader.ReadBlockAt<ResourceSimpleArray<JointTranslationLimit>>(
                this.TranslationLimitsPointer, // offset
                this.TranslationLimitsCount
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
            this.RotationLimitsCount = (ushort)(this.RotationLimits?.Count ?? 0);
            this.TranslationLimitsCount = (ushort)(this.TranslationLimits?.Count ?? 0);

            // write structure data
            writer.Write(this.RotationLimitsPointer);
            writer.Write(this.TranslationLimitsPointer);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.RotationLimitsCount);
            writer.Write(this.TranslationLimitsCount);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_36h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (RotationLimits != null) list.Add(RotationLimits);
            if (TranslationLimits != null) list.Add(TranslationLimits);
            return list.ToArray();
        }
    }
}
