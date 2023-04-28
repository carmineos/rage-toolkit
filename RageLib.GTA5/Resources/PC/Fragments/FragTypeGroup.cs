// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Simple;
using System;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    [Flags]
    public enum FragTypeGroupFlags : ushort
    {
        Unknown_1 = 0x1,
        Unknown_2 = 0x2,
        Unknown_3 = 0x4,
        Unknown_4 = 0x8,
        Unknown_5 = 0x10,
        Unknown_6 = 0x20,
        Unknown_7 = 0x40,
        Unknown_8 = 0x80,
        DisappearsWhenDead = 0x100,
        Unknown_10 = 0x200,
        Unknown_11 = 0x400,
        Unknown_12 = 0x800,
        Unknown_13 = 0x1000,
        Unknown_14 = 0x2000,
        Unknown_15 = 0x4000,
        Unknown_16 = 0x8000,
    }

    public class FragTypeGroup : ResourceSystemBlock
    {
        public override long BlockLength => 0xB0;

        // structure data
        private uint Unknown_0h; // 0x00000000
        private uint Unknown_4h; // 0x00000000
        private uint Unknown_8h; // 0x00000000
        private uint Unknown_Ch; // 0x00000000
        public float Strength;
        public float ForceTransmissionScaleUp;
        public float ForceTransmissionScaleDown;
        public float JointStiffness;
        public float MinSoftAngle1;
        public float MaxSoftAngle1;
        public float MaxSoftAngle2;
        public float MaxSoftAngle3;
        public float RotationSpeed;
        public float RotationStrength;
        public float RestoringStrength;
        public float RestoringMaxTorque;
        public float LatchStrength;
        public float Mass;
        private float Unknown_48h; // 0x00000000
        public byte FirstChildGroupIndex;
        public byte ParentIndex;
        public byte Index;
        public byte ChildrenCount;
        public byte ChildrenGroupCount;
        private byte Unknown_51h;
        public FragTypeGroupFlags GroupFlags;
        public float MinDamageForce;
        public float DamageHealth;
        private float Unknown_5Ch;
        private float Unknown_60h;
        private float Unknown_64h;
        private float Unknown_68h;
        private float Unknown_6Ch;
        private float Unknown_70h;
        private float Unknown_74h;
        private float Unknown_78h;
        private float Unknown_7Ch; // 0x00000000
        public string32_r Name;
        private float Unknown_A0h;
        private float Unknown_A4h;
        private float Unknown_A8h;
        private float Unknown_ACh; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.Unknown_8h = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Strength = reader.ReadSingle();
            this.ForceTransmissionScaleUp = reader.ReadSingle();
            this.ForceTransmissionScaleDown = reader.ReadSingle();
            this.JointStiffness = reader.ReadSingle();
            this.MinSoftAngle1 = reader.ReadSingle();
            this.MaxSoftAngle1 = reader.ReadSingle();
            this.MaxSoftAngle2 = reader.ReadSingle();
            this.MaxSoftAngle3 = reader.ReadSingle();
            this.RotationSpeed = reader.ReadSingle();
            this.RotationStrength = reader.ReadSingle();
            this.RestoringStrength = reader.ReadSingle();
            this.RestoringMaxTorque = reader.ReadSingle();
            this.LatchStrength = reader.ReadSingle();
            this.Mass = reader.ReadSingle();
            this.Unknown_48h = reader.ReadSingle();
            this.FirstChildGroupIndex = reader.ReadByte();
            this.ParentIndex = reader.ReadByte();
            this.Index = reader.ReadByte();
            this.ChildrenCount = reader.ReadByte();
            this.ChildrenGroupCount = reader.ReadByte();
            this.Unknown_51h = reader.ReadByte();
            this.GroupFlags = (FragTypeGroupFlags)reader.ReadUInt16();
            this.MinDamageForce = reader.ReadSingle();
            this.DamageHealth = reader.ReadSingle();
            this.Unknown_5Ch = reader.ReadSingle();
            this.Unknown_60h = reader.ReadSingle();
            this.Unknown_64h = reader.ReadSingle();
            this.Unknown_68h = reader.ReadSingle();
            this.Unknown_6Ch = reader.ReadSingle();
            this.Unknown_70h = reader.ReadSingle();
            this.Unknown_74h = reader.ReadSingle();
            this.Unknown_78h = reader.ReadSingle();
            this.Unknown_7Ch = reader.ReadSingle();
            this.Name = reader.ReadBlock<string32_r>();
            this.Unknown_A0h = reader.ReadSingle();
            this.Unknown_A4h = reader.ReadSingle();
            this.Unknown_A8h = reader.ReadSingle();
            this.Unknown_ACh = reader.ReadSingle();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Strength);
            writer.Write(this.ForceTransmissionScaleUp);
            writer.Write(this.ForceTransmissionScaleDown);
            writer.Write(this.JointStiffness);
            writer.Write(this.MinSoftAngle1);
            writer.Write(this.MaxSoftAngle1);
            writer.Write(this.MaxSoftAngle2);
            writer.Write(this.MaxSoftAngle3);
            writer.Write(this.RotationSpeed);
            writer.Write(this.RotationStrength);
            writer.Write(this.RestoringStrength);
            writer.Write(this.RestoringMaxTorque);
            writer.Write(this.LatchStrength);
            writer.Write(this.Mass);
            writer.Write(this.Unknown_48h);
            writer.Write(this.FirstChildGroupIndex);
            writer.Write(this.ParentIndex);
            writer.Write(this.Index);
            writer.Write(this.ChildrenCount);
            writer.Write(this.ChildrenGroupCount);
            writer.Write(this.Unknown_51h);
            writer.Write((ushort)this.GroupFlags);
            writer.Write(this.MinDamageForce);
            writer.Write(this.DamageHealth);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_74h);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_7Ch);
            writer.WriteBlock(this.Name);
            writer.Write(this.Unknown_A0h);
            writer.Write(this.Unknown_A4h);
            writer.Write(this.Unknown_A8h);
            writer.Write(this.Unknown_ACh);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x80, Name)
            };
        }
    }
}
