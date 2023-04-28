// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Simple;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // pgBase
    // ptxEffectSpawner
    public class EffectSpawner : PgBase64
    {
        public override long BlockLength => 0x70;

        // structure data
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        private uint Unknown_18h;
        private uint Unknown_1Ch;
        private uint Unknown_20h;
        private uint Unknown_24h;
        private uint Unknown_28h;
        private uint Unknown_2Ch; // 0x00000000
        private uint Unknown_30h; // 0x00000000
        private uint Unknown_34h; // 0x00000000
        private uint Unknown_38h;
        private uint Unknown_3Ch;
        private uint Unknown_40h;
        private uint Unknown_44h;
        private uint Unknown_48h;
        private uint Unknown_4Ch; // 0x00000000
        private uint Unknown_50h; // 0x00000000
        private uint Unknown_54h; // 0x00000000
        public ulong EmitterRulePointer;
        public ulong NamePointer;
        private uint Unknown_68h;
        private uint Unknown_6Ch;

        // reference data
        public EffectRule? EmitterRule { get; set; }
        public string_r? Name { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.Unknown_40h = reader.ReadUInt32();
            this.Unknown_44h = reader.ReadUInt32();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadUInt32();
            this.EmitterRulePointer = reader.ReadUInt64();
            this.NamePointer = reader.ReadUInt64();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();

            // read reference data
            this.EmitterRule = reader.ReadBlockAt<EffectRule>(
                this.EmitterRulePointer // offset
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
            this.EmitterRulePointer = (ulong)(this.EmitterRule?.BlockPosition ?? 0);
            this.NamePointer = (ulong)(this.Name?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_44h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.EmitterRulePointer);
            writer.Write(this.NamePointer);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (EmitterRule != null) list.Add(EmitterRule);
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }
    }
}
