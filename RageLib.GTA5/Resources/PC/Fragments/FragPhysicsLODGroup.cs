// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // pgBase
    // fragPhysicsLODGroup
    public class FragPhysicsLODGroup : PgBase64
    {
        public override long BlockLength => 0x30;

        // structure data
        private ulong PhysicsLOD1Pointer;
        private ulong PhysicsLOD2Pointer;
        private ulong PhysicsLOD3Pointer;
        public ulong Unknown_28h; // 0x0000000000000000

        // reference data
        public FragPhysicsLOD? PhysicsLOD1 { get; set; }
        public FragPhysicsLOD? PhysicsLOD2 { get; set; }
        public FragPhysicsLOD? PhysicsLOD3 { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.PhysicsLOD1Pointer = reader.ReadUInt64();
            this.PhysicsLOD2Pointer = reader.ReadUInt64();
            this.PhysicsLOD3Pointer = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();

            // read reference data
            this.PhysicsLOD1 = reader.ReadBlockAt<FragPhysicsLOD>(
                this.PhysicsLOD1Pointer // offset
            );
            this.PhysicsLOD2 = reader.ReadBlockAt<FragPhysicsLOD>(
                this.PhysicsLOD2Pointer // offset
            );
            this.PhysicsLOD3 = reader.ReadBlockAt<FragPhysicsLOD>(
                this.PhysicsLOD3Pointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.PhysicsLOD1Pointer = (ulong)(this.PhysicsLOD1?.BlockPosition ?? 0);
            this.PhysicsLOD2Pointer = (ulong)(this.PhysicsLOD2?.BlockPosition ?? 0);
            this.PhysicsLOD3Pointer = (ulong)(this.PhysicsLOD3?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.PhysicsLOD1Pointer);
            writer.Write(this.PhysicsLOD2Pointer);
            writer.Write(this.PhysicsLOD3Pointer);
            writer.Write(this.Unknown_28h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (PhysicsLOD1 != null) list.Add(PhysicsLOD1);
            if (PhysicsLOD2 != null) list.Add(PhysicsLOD2);
            if (PhysicsLOD3 != null) list.Add(PhysicsLOD3);
            return list.ToArray();
        }
    }
}
