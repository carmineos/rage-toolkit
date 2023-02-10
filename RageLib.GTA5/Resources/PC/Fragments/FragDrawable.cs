// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.Common.Simple;
using RageLib.Resources.GTA5.PC.Bounds;
using RageLib.Resources.GTA5.PC.Drawables;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // fragDrawable
    public class FragDrawable : Drawable
    {
        public override long BlockLength => 0x150;

        // structure data
        private ulong Unknown_A8h; // 0x0000000000000000
        private Matrix4x4 Unknown_B0h;
        public ulong BoundPointer;
        private SimpleList64<ulong> Unknown_F8h_Data;
        private SimpleList64<Matrix4x4> Unknown_108h_Data;
        private ulong Unknown_118h; // 0x0000000000000000
        private ulong Unknown_120h; // 0x0000000000000000
        private ulong Unknown_128h; // 0x0000000000000000
        public ulong NamePointer;
        private ulong Unknown_138h; // 0x0000000000000000
        private ulong Unknown_140h; // 0x0000000000000000
        private ulong Unknown_148h; // 0x0000000000000000

        // reference data
        public Bound? Bound { get; set; }
        public string_r? Name { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_A8h = reader.ReadUInt64();
            this.Unknown_B0h = reader.ReadMatrix4x4();
            this.BoundPointer = reader.ReadUInt64();
            this.Unknown_F8h_Data = reader.ReadBlock<SimpleList64<ulong>>();
            this.Unknown_108h_Data = reader.ReadBlock<SimpleList64<Matrix4x4>>();
            this.Unknown_118h = reader.ReadUInt64();
            this.Unknown_120h = reader.ReadUInt64();
            this.Unknown_128h = reader.ReadUInt64();
            this.NamePointer = reader.ReadUInt64();
            this.Unknown_138h = reader.ReadUInt64();
            this.Unknown_140h = reader.ReadUInt64();
            this.Unknown_148h = reader.ReadUInt64();

            // read reference data
            this.Bound = reader.ReadBlockAt<Bound>(
                this.BoundPointer // offset
            );
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.BoundPointer = (ulong)(this.Bound?.BlockPosition ?? 0);
            this.NamePointer = (ulong)(this.Name?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Unknown_A8h);
            writer.Write(this.Unknown_B0h);
            writer.Write(this.BoundPointer);
            writer.WriteBlock(this.Unknown_F8h_Data);
            writer.WriteBlock(this.Unknown_108h_Data);
            writer.Write(this.Unknown_118h);
            writer.Write(this.Unknown_120h);
            writer.Write(this.Unknown_128h);
            writer.Write(this.NamePointer);
            writer.Write(this.Unknown_138h);
            writer.Write(this.Unknown_140h);
            writer.Write(this.Unknown_148h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Bound != null) list.Add(Bound);
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            var list = new List<Tuple<long, IResourceBlock>>(base.GetParts());
            list.Add(new Tuple<long, IResourceBlock>(0xF8, Unknown_F8h_Data));
            list.Add(new Tuple<long, IResourceBlock>(0x108, Unknown_108h_Data));
            return list.ToArray();
        }
    }
}
