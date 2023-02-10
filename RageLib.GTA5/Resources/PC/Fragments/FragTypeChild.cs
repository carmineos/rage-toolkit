// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // datBase
    // fragTypeChild
    public class FragTypeChild : DatBase64
    {
        public override long BlockLength => 0x100;

        // structure data
        public float PristineMass;
        public float DamagedMass;
        public ushort GroupIndex;
        public ushort BoneId;
        private uint Unknown_14h; // 0x00000000
        private ulong Unknown_18h; // 0x0000000000000000
        private ulong Unknown_20h; // 0x0000000000000000
        private ulong Unknown_28h; // 0x0000000000000000
        private ulong Unknown_30h; // 0x0000000000000000
        private ulong Unknown_38h; // 0x0000000000000000
        private ulong Unknown_40h; // 0x0000000000000000
        private ulong Unknown_48h; // 0x0000000000000000
        private ulong Unknown_50h; // 0x0000000000000000
        private ulong Unknown_58h; // 0x0000000000000000
        private ulong Unknown_60h; // 0x0000000000000000
        private ulong Unknown_68h; // 0x0000000000000000
        private ulong Unknown_70h; // 0x0000000000000000
        private ulong Unknown_78h; // 0x0000000000000000
        private ulong Unknown_80h; // 0x0000000000000000
        private ulong Unknown_88h; // 0x0000000000000000
        private ulong Unknown_90h; // 0x0000000000000000
        private ulong Unknown_98h; // 0x0000000000000000
        public ulong PristineDrawablePointer;
        public ulong DamagedDrawablePointer;
        public ulong EvtSetPointer;
        private ulong Unknown_B8h; // 0x0000000000000000
        private ulong Unknown_C0h; // 0x0000000000000000
        private ulong Unknown_C8h; // 0x0000000000000000
        private ulong Unknown_D0h; // 0x0000000000000000
        private ulong Unknown_D8h; // 0x0000000000000000
        private ulong Unknown_E0h; // 0x0000000000000000
        private ulong Unknown_E8h; // 0x0000000000000000
        private ulong Unknown_F0h; // 0x0000000000000000
        private ulong Unknown_F8h; // 0x0000000000000000

        // reference data
        public FragDrawable? PristineDrawable { get; set; }
        public FragDrawable? DamagedDrawable { get; set; }
        public EvtSet? EvtSet { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.PristineMass = reader.ReadSingle();
            this.DamagedMass = reader.ReadSingle();
            this.GroupIndex = reader.ReadUInt16();
            this.BoneId = reader.ReadUInt16();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.Unknown_58h = reader.ReadUInt64();
            this.Unknown_60h = reader.ReadUInt64();
            this.Unknown_68h = reader.ReadUInt64();
            this.Unknown_70h = reader.ReadUInt64();
            this.Unknown_78h = reader.ReadUInt64();
            this.Unknown_80h = reader.ReadUInt64();
            this.Unknown_88h = reader.ReadUInt64();
            this.Unknown_90h = reader.ReadUInt64();
            this.Unknown_98h = reader.ReadUInt64();
            this.PristineDrawablePointer = reader.ReadUInt64();
            this.DamagedDrawablePointer = reader.ReadUInt64();
            this.EvtSetPointer = reader.ReadUInt64();
            this.Unknown_B8h = reader.ReadUInt64();
            this.Unknown_C0h = reader.ReadUInt64();
            this.Unknown_C8h = reader.ReadUInt64();
            this.Unknown_D0h = reader.ReadUInt64();
            this.Unknown_D8h = reader.ReadUInt64();
            this.Unknown_E0h = reader.ReadUInt64();
            this.Unknown_E8h = reader.ReadUInt64();
            this.Unknown_F0h = reader.ReadUInt64();
            this.Unknown_F8h = reader.ReadUInt64();

            // read reference data
            this.PristineDrawable = reader.ReadBlockAt<FragDrawable>(
                this.PristineDrawablePointer // offset
            );
            this.DamagedDrawable = reader.ReadBlockAt<FragDrawable>(
                this.DamagedDrawablePointer // offset
            );
            this.EvtSet = reader.ReadBlockAt<EvtSet>(
                this.EvtSetPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.PristineDrawablePointer = (ulong)(this.PristineDrawable?.BlockPosition ?? 0);
            this.DamagedDrawablePointer = (ulong)(this.DamagedDrawable?.BlockPosition ?? 0);
            this.EvtSetPointer = (ulong)(this.EvtSet?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.PristineMass);
            writer.Write(this.DamagedMass);
            writer.Write(this.GroupIndex);
            writer.Write(this.BoneId);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_98h);
            writer.Write(this.PristineDrawablePointer);
            writer.Write(this.DamagedDrawablePointer);
            writer.Write(this.EvtSetPointer);
            writer.Write(this.Unknown_B8h);
            writer.Write(this.Unknown_C0h);
            writer.Write(this.Unknown_C8h);
            writer.Write(this.Unknown_D0h);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.Unknown_E0h);
            writer.Write(this.Unknown_E8h);
            writer.Write(this.Unknown_F0h);
            writer.Write(this.Unknown_F8h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (PristineDrawable != null) list.Add(PristineDrawable);
            if (DamagedDrawable != null) list.Add(DamagedDrawable);
            if (EvtSet != null) list.Add(EvtSet);
            return list.ToArray();
        }
    }
}
