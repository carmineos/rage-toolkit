// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBoundBVH : PhBoundGeometry 
    {
        public override long BlockLength => 0xF0;

        // structure data
        private uint BvhPointer;
        private uint Unknown_E4h; // 0x00000000
        private uint Unknown_E8h; // 0x00000000
        private uint Unknown_ECh; // 0x00000000

        // reference data
        public PhBVH? BVH { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);
            
            // read structure data
            BvhPointer = reader.ReadUInt32();
            Unknown_E4h = reader.ReadUInt32();
            Unknown_E8h = reader.ReadUInt32();
            Unknown_ECh = reader.ReadUInt32();

            // read reference data
            BVH = reader.ReadBlockAt<PhBVH>(BvhPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            BvhPointer = (uint)(BVH?.BlockPosition ?? 0);

            // write structure data
            writer.Write(BvhPointer);
            writer.Write(Unknown_E4h);
            writer.Write(Unknown_E8h);
            writer.Write(Unknown_ECh);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (BVH is not null) list.Add(BVH);
            return list.ToArray();
        }
    }
}
