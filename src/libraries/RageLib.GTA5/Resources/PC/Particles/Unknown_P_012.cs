// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Drawables;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public class Unknown_P_012 : ResourceSystemBlock
    {
        public override long BlockLength => 0x30;

        // structure data
        public Vector4 Unknown_0h;
        public ulong NamePointer;
        public ulong DrawablePointer;
        public uint NameHash;
        public uint Unknown_24h; // 0x00000000
        public ulong Unknown_28h; // 0x0000000000000000

        // reference data
        public string_r? Name { get; set; }
        public Drawable? Drawable { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadVector4();
            this.NamePointer = reader.ReadUInt64();
            this.DrawablePointer = reader.ReadUInt64();
            this.NameHash = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt64();

            // read reference data
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
            this.Drawable = reader.ReadBlockAt<Drawable>(
                this.DrawablePointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.NamePointer = (ulong)(this.Name?.BlockPosition ?? 0);
            this.DrawablePointer = (ulong)(this.Drawable?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.NamePointer);
            writer.Write(this.DrawablePointer);
            writer.Write(this.NameHash);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            if (Drawable != null) list.Add(Drawable);
            return list.ToArray();
        }
    }
}
