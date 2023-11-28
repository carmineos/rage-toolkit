// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA4.PC.Textures;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class ShaderGroup : DatBase32
    {
        public override long BlockLength => 0x50;

        // structure data
        private uint TextureDictionaryPointer;
        public ResourcePointerList32<ShaderFX> Shaders;
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        private uint Unknown_18h; // 0x00000000
        private uint Unknown_1Ch; // 0x00000000
        private uint Unknown_20h; // 0x00000000
        private uint Unknown_24h; // 0x00000000
        private uint Unknown_28h; // 0x00000000
        private uint Unknown_2Ch; // 0x00000000
        private uint Unknown_30h; // 0x00000000
        private uint Unknown_34h; // 0x00000000
        private uint Unknown_38h; // 0x00000000
        private uint Unknown_3Ch; // 0x00000000
        public SimpleList32<uint> VertexDeclarationUsageFlags;
        public SimpleList32<uint> Unknown_48h;

        // reference data
        public PgDictionary32<GrcTexturePC>? TextureDictionary { get; set; }

        public ShaderGroup()
        {
            Shaders = new ResourcePointerList32<ShaderFX>();
            VertexDeclarationUsageFlags = new SimpleList32<uint>();
            Unknown_48h = new SimpleList32<uint>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            TextureDictionaryPointer = reader.ReadUInt32();

            // HACK:    read texture dictionary first!
            //          this will make sure ShaderParameter Data will point to the already read TextureDX11
            //          instead of creating duplicated Texture blocks

            // TODO:    edit ResourceDataReader block pool to handle these scenarios!
            //          see https://github.com/carmineos/gta-toolkit/issues/11

            // read reference data
            TextureDictionary = reader.ReadBlockAt<PgDictionary32<GrcTexturePC>>(TextureDictionaryPointer);

            Shaders = reader.ReadBlock<ResourcePointerList32<ShaderFX>>();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            Unknown_20h = reader.ReadUInt32();
            Unknown_24h = reader.ReadUInt32();
            Unknown_28h = reader.ReadUInt32();
            Unknown_2Ch = reader.ReadUInt32();
            Unknown_30h = reader.ReadUInt32();
            Unknown_34h = reader.ReadUInt32();
            Unknown_38h = reader.ReadUInt32();
            Unknown_3Ch = reader.ReadUInt32();
            VertexDeclarationUsageFlags = reader.ReadBlock<SimpleList32<uint>>();
            Unknown_48h = reader.ReadBlock<SimpleList32<uint>>(); 
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            TextureDictionaryPointer = (uint)(TextureDictionary?.BlockPosition ?? 0);

            // write structure data
            writer.Write(TextureDictionaryPointer);
            writer.WriteBlock(Shaders);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(Unknown_20h);
            writer.Write(Unknown_24h);
            writer.Write(Unknown_28h);
            writer.Write(Unknown_2Ch);
            writer.Write(Unknown_30h);
            writer.Write(Unknown_34h);
            writer.Write(Unknown_38h);
            writer.Write(Unknown_3Ch);
            writer.WriteBlock(VertexDeclarationUsageFlags);
            writer.WriteBlock(Unknown_48h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x8, Shaders),
                new Tuple<long, IResourceBlock>(0x40, VertexDeclarationUsageFlags),
                new Tuple<long, IResourceBlock>(0x48, Unknown_48h),
            };
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (TextureDictionary is not null) list.Add(TextureDictionary);
            return list.ToArray();
        }
    }
}
