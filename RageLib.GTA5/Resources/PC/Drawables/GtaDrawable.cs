// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Bounds;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // gtaDrawable
    public class GtaDrawable : Drawable
    {
        public override long BlockLength => 0xD0;

        // structure data
        public PgRef64<string_r> Name;
        public SimpleList64<LightAttributes> LightAttributes;
        public ulong Unknown_C0h; // 0x0000000000000000
        public PgRef64<Bound> Bound;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Name = reader.ReadPointer<string_r>();
            this.LightAttributes = reader.ReadBlock<SimpleList64<LightAttributes>>();
            this.Unknown_C0h = reader.ReadUInt64();
            this.Bound = reader.ReadPointer<Bound>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Name);
            writer.Write(this.Bound);
            writer.WriteBlock(this.LightAttributes);
            writer.Write(this.Unknown_C0h); 
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name.Data != null) list.Add(Name.Data);
            if (Bound.Data != null) list.Add(Bound.Data);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            var list = new List<Tuple<long, IResourceBlock>>(base.GetParts());
            list.Add(new Tuple<long, IResourceBlock>(0xB0, LightAttributes));
            return list.ToArray();
        }
    }
}
