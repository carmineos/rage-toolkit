// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBoundGeometry : PhBoundPolyhedron
    {
        public override long BlockLength => 0xE0;

        // structure data
        private uint MaterialsPointer;
        public uint Unknown_D0h;
        public byte MaterialsCount;
        private byte Unknown_D6h; // 0x00
        private ushort Unknown_D8h; // 0x0000
        private uint Unknown_DCh; // 0x00000000

        // reference data
        public SimpleArray<PhBoundMaterial>? Materials { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            MaterialsPointer = reader.ReadUInt32();
            Unknown_D0h = reader.ReadUInt32();
            MaterialsCount = reader.ReadByte();
            Unknown_D6h = reader.ReadByte();
            Unknown_D8h = reader.ReadUInt16();
            Unknown_DCh = reader.ReadUInt32();

            // read reference data
            Materials = reader.ReadBlockAt<SimpleArray<PhBoundMaterial>>(MaterialsPointer, MaterialsCount);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            MaterialsPointer = (uint)(Materials?.BlockPosition ?? 0);
            MaterialsCount = (byte)(Materials?.Count ?? 0);

            // write structure data
            writer.Write(MaterialsPointer);
            writer.Write(Unknown_D0h);
            writer.Write(MaterialsCount);
            writer.Write(Unknown_D6h);
            writer.Write(Unknown_D8h);
            writer.Write(Unknown_DCh);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Materials is not null) list.Add(Materials);
            return list.ToArray();
        }
    }
}
