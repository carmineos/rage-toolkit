// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Bounds
{
    // phBoundPolyhedron
    // phBoundGeometry
    public class BoundGeometry : BoundPolyhedron
    {
        public override long BlockLength => 0x130;

        // structure data
        public PgRef64<SimpleArray<BoundMaterial>> Materials;
        public PgRef64<SimpleArray<uint>> MaterialColours;
        public ulong Unknown_100h; // 0x0000000000000000
        public ulong Unknown_108h; // 0x0000000000000000
        public ulong Unknown_110h; // 0x0000000000000000
        public PgRef64<SimpleArray<byte>> PolygonMaterialIndices;
        public byte MaterialsCount;
        public byte MaterialColoursCount;
        public ushort Unknown_122h; // 0x0000
        public uint Unknown_124h; // 0x00000000
        public ulong Unknown_128h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Materials = reader.ReadPointer<SimpleArray<BoundMaterial>>(false);
            this.MaterialColours = reader.ReadPointer<SimpleArray<uint>>(false);
            this.Unknown_100h = reader.ReadUInt64();
            this.Unknown_108h = reader.ReadUInt64();
            this.Unknown_110h = reader.ReadUInt64();
            this.PolygonMaterialIndices = reader.ReadPointer<SimpleArray<byte>>(false);
            this.MaterialsCount = reader.ReadByte();
            this.MaterialColoursCount = reader.ReadByte();
            this.Unknown_122h = reader.ReadUInt16();
            this.Unknown_124h = reader.ReadUInt32();
            this.Unknown_128h = reader.ReadUInt64();

            // read reference data
            this.Materials.ReadReference(reader, this.MaterialsCount);
            this.MaterialColours.ReadReference(reader, this.MaterialColoursCount);
            this.PolygonMaterialIndices.ReadReference(reader, this.PrimitivesCount);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.MaterialsCount = (byte)(this.Materials.Data?.Count ?? 0);
            this.MaterialColoursCount = (byte)(this.MaterialColours.Data?.Count ?? 0);

            // write structure data
            writer.Write(this.Materials);
            writer.Write(this.MaterialColours);
            writer.Write(this.Unknown_100h);
            writer.Write(this.Unknown_108h);
            writer.Write(this.Unknown_110h);
            writer.Write(this.PolygonMaterialIndices);
            writer.Write(this.MaterialsCount);
            writer.Write(this.MaterialColoursCount);
            writer.Write(this.Unknown_122h);
            writer.Write(this.Unknown_124h);
            writer.Write(this.Unknown_128h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Materials.Data != null) list.Add(Materials.Data);
            if (MaterialColours.Data != null) list.Add(MaterialColours.Data);
            if (PolygonMaterialIndices.Data != null) list.Add(PolygonMaterialIndices.Data);
            return list.ToArray();
        }
    }
}
