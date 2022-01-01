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
        public ulong NamePointer;
        public ResourceSimpleList64<LightAttributes> LightAttributes;
        public ulong Unknown_C0h; // 0x0000000000000000
        public ulong BoundPointer;

        // reference data
        public string_r Name;
        public Bound Bound;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.NamePointer = reader.ReadUInt64();
            this.LightAttributes = reader.ReadBlock<ResourceSimpleList64<LightAttributes>>();
            this.Unknown_C0h = reader.ReadUInt64();
            this.BoundPointer = reader.ReadUInt64();

            // read reference data
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
            this.Bound = reader.ReadBlockAt<Bound>(
                this.BoundPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.NamePointer = (ulong)(this.Name != null ? this.Name.BlockPosition : 0);
            this.BoundPointer = (ulong)(this.Bound != null ? this.Bound.BlockPosition : 0);

            // write structure data
            writer.Write(this.NamePointer);
            writer.WriteBlock(this.LightAttributes);
            writer.Write(this.Unknown_C0h);
            writer.Write(this.BoundPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            if (Bound != null) list.Add(Bound);
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
