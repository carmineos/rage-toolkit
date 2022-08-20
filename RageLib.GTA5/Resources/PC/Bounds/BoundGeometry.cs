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
        private ulong MaterialsPointer;
        private ulong MaterialColoursPointer;
        public ulong Unknown_100h; // 0x0000000000000000
        public ulong Unknown_108h; // 0x0000000000000000
        public ulong Unknown_110h; // 0x0000000000000000
        private ulong PolygonMaterialIndicesPointer;
        public byte MaterialsCount;
        public byte MaterialColoursCount;
        public ushort Unknown_122h; // 0x0000
        public uint Unknown_124h; // 0x00000000
        public ulong Unknown_128h; // 0x0000000000000000

        // reference data
        public SimpleArray<BoundMaterial>? Materials { get; set; }
        public SimpleArray<uint>? MaterialColours { get; set; }
        public SimpleArray<byte>? PolygonMaterialIndices { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.MaterialsPointer = reader.ReadUInt64();
            this.MaterialColoursPointer = reader.ReadUInt64();
            this.Unknown_100h = reader.ReadUInt64();
            this.Unknown_108h = reader.ReadUInt64();
            this.Unknown_110h = reader.ReadUInt64();
            this.PolygonMaterialIndicesPointer = reader.ReadUInt64();
            this.MaterialsCount = reader.ReadByte();
            this.MaterialColoursCount = reader.ReadByte();
            this.Unknown_122h = reader.ReadUInt16();
            this.Unknown_124h = reader.ReadUInt32();
            this.Unknown_128h = reader.ReadUInt64();

            // read reference data
            this.Materials = reader.ReadBlockAt<SimpleArray<BoundMaterial>>(
                this.MaterialsPointer, // offset
                this.MaterialsCount
            );
            this.MaterialColours = reader.ReadBlockAt<SimpleArray<uint>>(
                this.MaterialColoursPointer, // offset
                this.MaterialColoursCount
            );
            this.PolygonMaterialIndices = reader.ReadBlockAt<SimpleArray<byte>>(
                this.PolygonMaterialIndicesPointer, // offset
                this.PrimitivesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.MaterialsPointer = (ulong)(this.Materials?.BlockPosition ?? 0);
            this.MaterialColoursPointer = (ulong)(this.MaterialColours?.BlockPosition ?? 0);
            this.PolygonMaterialIndicesPointer = (ulong)(this.PolygonMaterialIndices?.BlockPosition ?? 0);
            this.MaterialsCount = (byte)(this.Materials?.Count ?? 0);
            this.MaterialColoursCount = (byte)(this.MaterialColours?.Count ?? 0);

            // write structure data
            writer.Write(this.MaterialsPointer);
            writer.Write(this.MaterialColoursPointer);
            writer.Write(this.Unknown_100h);
            writer.Write(this.Unknown_108h);
            writer.Write(this.Unknown_110h);
            writer.Write(this.PolygonMaterialIndicesPointer);
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
            if (Materials != null) list.Add(Materials);
            if (MaterialColours != null) list.Add(MaterialColours);
            if (PolygonMaterialIndices != null) list.Add(PolygonMaterialIndices);
            return list.ToArray();
        }
    }
}
