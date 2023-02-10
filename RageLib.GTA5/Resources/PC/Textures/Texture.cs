// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Simple;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Textures
{
    // pgBase
    // grcTexture
    public class Texture : PgBase64
    {
        public override long BlockLength => 0x50;

        // structure data
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        private uint Unknown_18h; // 0x00000000
        private uint Unknown_1Ch; // 0x00000000
        private uint Unknown_20h; // 0x00000000
        private uint Unknown_24h; // 0x00000000
        public ulong NamePointer;
        private uint Unknown_30h;
        private uint Unknown_34h; // 0x00000000
        private uint Unknown_38h; // 0x00000000
        private uint Unknown_3Ch; // 0x00000000
        public uint Unknown_40h;
        private uint Unknown_44h; // 0x00000000
        public uint Unknown_48h;
        private uint Unknown_4Ch; // 0x00000000

        // reference data
        public string_r? Name { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            NamePointer = reader.ReadUInt64();
            Unknown_30h = reader.ReadUInt32();
            Unknown_34h = reader.ReadUInt32();
            Unknown_38h = reader.ReadUInt32();
            Unknown_3Ch = reader.ReadUInt32();
            this.Unknown_40h = reader.ReadUInt32();
            Unknown_44h = reader.ReadUInt32();
            this.Unknown_48h = reader.ReadUInt32();
            Unknown_4Ch = reader.ReadUInt32();


            // read reference data
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
            this.NamePointer = (ulong)(this.Name?.BlockPosition ?? 0);

            // write structure data
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(NamePointer);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_34h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
            writer.Write(this.Unknown_40h);
            writer.Write(Unknown_44h);
            writer.Write(this.Unknown_48h);
            writer.Write(Unknown_4Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }
    }
}
