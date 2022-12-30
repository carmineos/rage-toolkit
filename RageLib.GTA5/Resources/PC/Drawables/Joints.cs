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
        public PgRef64<SimpleArray<JointRotationLimit>> RotationLimits;
        public PgRef64<SimpleArray<JointTranslationLimit>> TranslationLimits;
        public PgRef64<SimpleArray<JointScaleLimit>> ScaleLimits;
        public ulong Unknown_28h; // 0x0000000000000000
        public ushort RotationLimitsCount;
        public ushort TranslationLimitsCount;
        public ushort ScaleLimitsCount; // 0x0000
        public ushort Unknown_36h; // 0x0001
        public ulong Unknown_38h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.RotationLimits = reader.ReadPointer<SimpleArray<JointRotationLimit>>(false);
            this.TranslationLimits = reader.ReadPointer<SimpleArray<JointTranslationLimit>>(false);
            this.ScaleLimits= reader.ReadPointer<SimpleArray<JointScaleLimit>>(false);
            this.Unknown_28h = reader.ReadUInt64();
            this.RotationLimitsCount = reader.ReadUInt16();
            this.TranslationLimitsCount = reader.ReadUInt16();
            this.ScaleLimitsCount = reader.ReadUInt16();
            this.Unknown_36h = reader.ReadUInt16();
            this.Unknown_38h = reader.ReadUInt64();

            // read reference data
            this.RotationLimits.ReadReference(reader, this.RotationLimitsCount);
            this.TranslationLimits.ReadReference(reader, this.TranslationLimitsCount);
            this.ScaleLimits.ReadReference(reader, this.ScaleLimitsCount);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.RotationLimitsCount = (ushort)(this.RotationLimits.Data?.Count ?? 0);
            this.TranslationLimitsCount = (ushort)(this.TranslationLimits.Data?.Count ?? 0);
            this.ScaleLimitsCount = (ushort)(this.ScaleLimits.Data?.Count ?? 0);

            // write structure data
            writer.Write(this.RotationLimits);
            writer.Write(this.TranslationLimits);
            writer.Write(this.ScaleLimits);
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
            if (RotationLimits.Data != null) list.Add(RotationLimits.Data);
            if (TranslationLimits.Data != null) list.Add(TranslationLimits.Data);
            if (ScaleLimits.Data != null) list.Add(ScaleLimits.Data);
            return list.ToArray();
        }
    }
}
