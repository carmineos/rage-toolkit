// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Drawables;
using System.Collections.Generic;
using System;
using RageLib.Resources.GTA5.PC.Clothes;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // fragType
    // gtaFragType
    public class FragType : PgBase64
    {
        public override long BlockLength => 0x130;

        // structure data
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h; // 0x0000000000000000
        public Vector3 BoundingSphereCenter;
        public float BoundingSphereRadius;
        public ulong PrimaryDrawablePointer;
        public ulong DamagedDrawablesPointer;
        public ulong DamagedDrawablesNamesPointer;
        public uint DrawablesCount;
        public uint Unknown_4Ch;
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong NamePointer;
        public ResourcePointerList64<EnvironmentCloth> Clothes;
        public ulong Unknown_70h; // 0x0000000000000000
        public ulong Unknown_78h; // 0x0000000000000000
        public ulong Unknown_80h; // 0x0000000000000000
        public ulong Unknown_88h; // 0x0000000000000000
        public ulong Unknown_90h; // 0x0000000000000000
        public ulong Unknown_98h; // 0x0000000000000000
        public ulong Unknown_A0h; // 0x0000000000000000
        public ulong MatrixSetPointer;
        public uint Unknown_B0h;
        public uint Unknown_B4h; // 0x00000000
        public uint Unknown_B8h;
        public uint Unknown_BCh;
        public uint Unknown_C0h;
        public uint Unknown_C4h;
        public uint Unknown_C8h; // 0xFFFFFFFF
        public uint Unknown_CCh;
        public float GravityMultiplier;
        public float BuoyancyMultiplier;
        public byte Unknown_D8h;
        public byte GlassPaneModelInfosCount;
        public ushort Unknown_DAh;
        public uint Unknown_DCh; // 0x00000000
        public ulong GlassPaneModelInfosPointer;
        public ulong Unknown_E8h; // 0x0000000000000000
        public ulong PhysicsLODGroupPointer;
        public ulong ClothDrawablePointer;
        public ulong Unknown_100h; // 0x0000000000000000
        public ulong Unknown_108h; // 0x0000000000000000
        public ResourceSimpleList64<LightAttributes> LightAttributes;
        public ulong VehicleGlassWindowDataPointer;
        public ulong Unknown_128h; // 0x0000000000000000

        // reference data
        public FragDrawable? PrimaryDrawable { get; set; }
        public ResourcePointerArray64<FragDrawable>? DamagedDrawables { get; set; }
        public ResourcePointerArray64<string_r>? DamagedDrawablesNames { get; set; }
        public string_r? Name { get; set; }
        public MatrixSet? MatrixSet { get; set; }
        public ResourcePointerArray64<GlassPaneModelInfo>? GlassPaneModelInfos { get; set; }
        public FragPhysicsLODGroup? PhysicsLODGroup { get; set; }
        public FragDrawable? ClothDrawable { get; set; }
        public VehicleGlassWindowData? VehicleGlassWindowData { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.BoundingSphereCenter = reader.ReadVector3();
            this.BoundingSphereRadius = reader.ReadSingle();
            this.PrimaryDrawablePointer = reader.ReadUInt64();
            this.DamagedDrawablesPointer = reader.ReadUInt64();
            this.DamagedDrawablesNamesPointer = reader.ReadUInt64();
            this.DrawablesCount = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h = reader.ReadUInt64();
            this.NamePointer = reader.ReadUInt64();
            this.Clothes = reader.ReadBlock<ResourcePointerList64<EnvironmentCloth>>();
            this.Unknown_70h = reader.ReadUInt64();
            this.Unknown_78h = reader.ReadUInt64();
            this.Unknown_80h = reader.ReadUInt64();
            this.Unknown_88h = reader.ReadUInt64();
            this.Unknown_90h = reader.ReadUInt64();
            this.Unknown_98h = reader.ReadUInt64();
            this.Unknown_A0h = reader.ReadUInt64();
            this.MatrixSetPointer = reader.ReadUInt64();
            this.Unknown_B0h = reader.ReadUInt32();
            this.Unknown_B4h = reader.ReadUInt32();
            this.Unknown_B8h = reader.ReadUInt32();
            this.Unknown_BCh = reader.ReadUInt32();
            this.Unknown_C0h = reader.ReadUInt32();
            this.Unknown_C4h = reader.ReadUInt32();
            this.Unknown_C8h = reader.ReadUInt32();
            this.Unknown_CCh = reader.ReadUInt32();
            this.GravityMultiplier = reader.ReadSingle();
            this.BuoyancyMultiplier = reader.ReadSingle();
            this.Unknown_D8h = reader.ReadByte();
            this.GlassPaneModelInfosCount = reader.ReadByte();
            this.Unknown_DAh = reader.ReadUInt16();
            this.Unknown_DCh = reader.ReadUInt32();
            this.GlassPaneModelInfosPointer = reader.ReadUInt64();
            this.Unknown_E8h = reader.ReadUInt64();
            this.PhysicsLODGroupPointer = reader.ReadUInt64();
            this.ClothDrawablePointer = reader.ReadUInt64();
            this.Unknown_100h = reader.ReadUInt64();
            this.Unknown_108h = reader.ReadUInt64();
            this.LightAttributes = reader.ReadBlock<ResourceSimpleList64<LightAttributes>>();
            this.VehicleGlassWindowDataPointer = reader.ReadUInt64();
            this.Unknown_128h = reader.ReadUInt64();

            // read reference data
            this.PrimaryDrawable = reader.ReadBlockAt<FragDrawable>(
                this.PrimaryDrawablePointer // offset
            );
            this.DamagedDrawables = reader.ReadBlockAt<ResourcePointerArray64<FragDrawable>>(
                this.DamagedDrawablesPointer, // offset
                this.DrawablesCount
            );
            this.DamagedDrawablesNames = reader.ReadBlockAt<ResourcePointerArray64<string_r>>(
                this.DamagedDrawablesNamesPointer, // offset
                this.DrawablesCount
            );
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
            this.MatrixSet = reader.ReadBlockAt<MatrixSet>(
                this.MatrixSetPointer // offset
            );
            this.GlassPaneModelInfos = reader.ReadBlockAt<ResourcePointerArray64<GlassPaneModelInfo>>(
                this.GlassPaneModelInfosPointer, // offset
                this.GlassPaneModelInfosCount
            );
            this.PhysicsLODGroup = reader.ReadBlockAt<FragPhysicsLODGroup>(
                this.PhysicsLODGroupPointer // offset
            );
            this.ClothDrawable = reader.ReadBlockAt<FragDrawable>(
                this.ClothDrawablePointer // offset
            );
            this.VehicleGlassWindowData = reader.ReadBlockAt<VehicleGlassWindowData>(
                this.VehicleGlassWindowDataPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.PrimaryDrawablePointer = (ulong)(this.PrimaryDrawable?.BlockPosition ?? 0);
            this.DamagedDrawablesPointer = (ulong)(this.DamagedDrawables?.BlockPosition ?? 0);
            this.DamagedDrawablesNamesPointer = (ulong)(this.DamagedDrawablesNames?.BlockPosition ?? 0);
            this.NamePointer = (ulong)(this.Name?.BlockPosition ?? 0);
            this.MatrixSetPointer = (ulong)(this.MatrixSet?.BlockPosition ?? 0);
            this.GlassPaneModelInfosPointer = (ulong)(this.GlassPaneModelInfos?.BlockPosition ?? 0);
            this.PhysicsLODGroupPointer = (ulong)(this.PhysicsLODGroup?.BlockPosition ?? 0);
            this.ClothDrawablePointer = (ulong)(this.ClothDrawable?.BlockPosition ?? 0);
            this.VehicleGlassWindowDataPointer = (ulong)(this.VehicleGlassWindowData?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.BoundingSphereCenter);
            writer.Write(this.BoundingSphereRadius);
            writer.Write(this.PrimaryDrawablePointer);
            writer.Write(this.DamagedDrawablesPointer);
            writer.Write(this.DamagedDrawablesNamesPointer);
            writer.Write(this.DrawablesCount);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.Unknown_50h);
            writer.Write(this.NamePointer);
            writer.WriteBlock(this.Clothes);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_A0h);
            writer.Write(this.MatrixSetPointer);
            writer.Write(this.Unknown_B0h);
            writer.Write(this.Unknown_B4h);
            writer.Write(this.Unknown_B8h);
            writer.Write(this.Unknown_BCh);
            writer.Write(this.Unknown_C0h);
            writer.Write(this.Unknown_C4h);
            writer.Write(this.Unknown_C8h);
            writer.Write(this.Unknown_CCh);
            writer.Write(this.GravityMultiplier);
            writer.Write(this.BuoyancyMultiplier);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.GlassPaneModelInfosCount);
            writer.Write(this.Unknown_DAh);
            writer.Write(this.Unknown_DCh);
            writer.Write(this.GlassPaneModelInfosPointer);
            writer.Write(this.Unknown_E8h);
            writer.Write(this.PhysicsLODGroupPointer);
            writer.Write(this.ClothDrawablePointer);
            writer.Write(this.Unknown_100h);
            writer.Write(this.Unknown_108h);
            writer.WriteBlock(this.LightAttributes);
            writer.Write(this.VehicleGlassWindowDataPointer);
            writer.Write(this.Unknown_128h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (PrimaryDrawable != null) list.Add(PrimaryDrawable);
            if (DamagedDrawables != null) list.Add(DamagedDrawables);
            if (DamagedDrawablesNames != null) list.Add(DamagedDrawablesNames);
            if (Name != null) list.Add(Name);
            if (MatrixSet != null) list.Add(MatrixSet);
            if (GlassPaneModelInfos != null) list.Add(GlassPaneModelInfos);
            if (PhysicsLODGroup != null) list.Add(PhysicsLODGroup);
            if (ClothDrawable != null) list.Add(ClothDrawable);
            if (VehicleGlassWindowData != null) list.Add(VehicleGlassWindowData);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x60, Clothes),
                new Tuple<long, IResourceBlock>(0x110, LightAttributes)
            };
        }
    }
}
