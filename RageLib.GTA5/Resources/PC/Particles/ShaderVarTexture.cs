// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Textures;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxShaderVarTexture
    public class ShaderVarTexture : ShaderVar
    {
        public override long BlockLength => 0x40;

        // structure data
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public uint Unknown_20h;
        public uint Unknown_24h;
        public ulong TexturePointer;
        public ulong NamePointer;
        public uint NameHash;
        public uint Unknown_3Ch;

        // reference data
        public TextureDX11 Texture;
        public string_r Name;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.TexturePointer = reader.ReadUInt64();
            this.NamePointer = reader.ReadUInt64();
            this.NameHash = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();

            // read reference data
            this.Texture = reader.ReadBlockAt<TextureDX11>(
                this.TexturePointer // offset
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
            this.TexturePointer = (ulong)(this.Texture != null ? this.Texture.BlockPosition : 0);
            this.NamePointer = (ulong)(this.Name != null ? this.Name.BlockPosition : 0);

            // write structure data
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.TexturePointer);
            writer.Write(this.NamePointer);
            writer.Write(this.NameHash);
            writer.Write(this.Unknown_3Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Texture != null) list.Add(Texture);
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }
    }
}
