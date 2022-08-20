// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Textures;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // datBase
    // grmShaderGroup
    public class ShaderGroup : DatBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        private ulong TextureDictionaryPointer;
        public ResourcePointerList64<ShaderFX> Shaders;
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000
        public uint Unknown_30h;
        public uint Unknown_34h; // 0x00000000
        public uint Unknown_38h; // 0x00000000
        public uint Unknown_3Ch; // 0x00000000

        // reference data
        public PgDictionary64<TextureDX11>? TextureDictionary { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.TextureDictionaryPointer = reader.ReadUInt64();

            // HACK:    read texture dictionary first!
            //          this will make sure ShaderParameter Data will point to the already read TextureDX11
            //          instead of creating duplicated Texture blocks

            // TODO:    edit ResourceDataReader block pool to handle these scenarios!
            //          see https://github.com/carmineos/gta-toolkit/issues/11

            // read reference data
            this.TextureDictionary = reader.ReadBlockAt<PgDictionary64<TextureDX11>>(
                this.TextureDictionaryPointer // offset
            );

            this.Shaders = reader.ReadBlock<ResourcePointerList64<ShaderFX>>();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.TextureDictionaryPointer = (ulong)(this.TextureDictionary?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.TextureDictionaryPointer);
            writer.WriteBlock(this.Shaders);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (TextureDictionary != null) list.Add(TextureDictionary);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x10, Shaders)
            };
        }
    }
}
