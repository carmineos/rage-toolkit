// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.Common.Simple;
using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.Resources.GTA5.PC.Textures;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // pgBase
    // ptxFxList
    public class ParticleEffectsList : PgBase64
    {
        public override long BlockLength => 0x60;

        // structure data
        public ulong NamePointer;
        private uint Unknown_18h; // 0x00000000
        private uint Unknown_1Ch; // 0x00000000
        public ulong TextureDictionaryPointer;
        private uint Unknown_28h; // 0x00000000
        private uint Unknown_2Ch; // 0x00000000
        public ulong DrawableDictionaryPointer;
        public ulong ParticleRuleDictionaryPointer;
        private uint Unknown_40h; // 0x00000000
        private uint Unknown_44h; // 0x00000000
        public ulong EmitterRuleDictionaryPointer;
        public ulong EffectRuleDictionaryPointer;
        private uint Unknown_58h; // 0x00000000
        private uint Unknown_5Ch; // 0x00000000

        // reference data
        public string_r? Name;
        public PgDictionary64<TextureDX11>? TextureDictionary { get; set; }
        public PgDictionary64<Drawable>? DrawableDictionary { get; set; }
        public PgDictionary64<ParticleRule>? ParticleRuleDictionary { get; set; }
        public PgDictionary64<EffectRule>? EffectRuleDictionary { get; set; }
        public PgDictionary64<EmitterRule>? EmitterRuleDictionary { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.NamePointer = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.TextureDictionaryPointer = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.DrawableDictionaryPointer = reader.ReadUInt64();
            this.ParticleRuleDictionaryPointer = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt32();
            this.Unknown_44h = reader.ReadUInt32();
            this.EmitterRuleDictionaryPointer = reader.ReadUInt64();
            this.EffectRuleDictionaryPointer = reader.ReadUInt64();
            this.Unknown_58h = reader.ReadUInt32();
            this.Unknown_5Ch = reader.ReadUInt32();

            // read reference data
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
            this.TextureDictionary = reader.ReadBlockAt<PgDictionary64<TextureDX11>>(
                this.TextureDictionaryPointer // offset
            );
            this.DrawableDictionary = reader.ReadBlockAt<PgDictionary64<Drawable>>(
                this.DrawableDictionaryPointer // offset
            );
            this.ParticleRuleDictionary = reader.ReadBlockAt<PgDictionary64<ParticleRule>>(
                this.ParticleRuleDictionaryPointer // offset
            );
            this.EffectRuleDictionary = reader.ReadBlockAt<PgDictionary64<EffectRule>>(
                this.EmitterRuleDictionaryPointer // offset
            );
            this.EmitterRuleDictionary = reader.ReadBlockAt<PgDictionary64<EmitterRule>>(
                this.EffectRuleDictionaryPointer // offset
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
            this.TextureDictionaryPointer = (ulong)(this.TextureDictionary?.BlockPosition ?? 0);
            this.DrawableDictionaryPointer = (ulong)(this.DrawableDictionary?.BlockPosition ?? 0);
            this.ParticleRuleDictionaryPointer = (ulong)(this.ParticleRuleDictionary?.BlockPosition ?? 0);
            this.EmitterRuleDictionaryPointer = (ulong)(this.EffectRuleDictionary?.BlockPosition ?? 0);
            this.EffectRuleDictionaryPointer = (ulong)(this.EmitterRuleDictionary?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.NamePointer);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.TextureDictionaryPointer);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.DrawableDictionaryPointer);
            writer.Write(this.ParticleRuleDictionaryPointer);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_44h);
            writer.Write(this.EmitterRuleDictionaryPointer);
            writer.Write(this.EffectRuleDictionaryPointer);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            if (TextureDictionary != null) list.Add(TextureDictionary);
            if (DrawableDictionary != null) list.Add(DrawableDictionary);
            if (ParticleRuleDictionary != null) list.Add(ParticleRuleDictionary);
            if (EffectRuleDictionary != null) list.Add(EffectRuleDictionary);
            if (EmitterRuleDictionary != null) list.Add(EmitterRuleDictionary);
            return list.ToArray();
        }
    }
}
