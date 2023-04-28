// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.Common.Simple;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // pgBase
    // crClip
    public class Clip : PgBase64, IResourceXXSystemBlock
    {
        public override long BlockLength => 0x50;

        // structure data
        public byte Type;
        private byte Unknown_11h;
        private ushort Unknown_12h;
        private uint Unknown_14h; // 0x00000000
        public ulong NamePointer;
        public ushort NameLength1;
        public ushort NameLength2;
        private uint Unknown_24h; // 0x00000000
        private uint Unknown_28h; // 0x50000000
        private uint Unknown_2Ch; // 0x00000000
        private uint Unknown_30h;
        private uint Unknown_34h; // 0x00000000
        public ulong TagsPointer;
        public ulong PropertiesPointer;
        private uint Unknown_48h; // 0x00000001
        private uint Unknown_4Ch; // 0x00000000       

        // reference data
        public string_r? Name { get; set; }
        public Tags? Tags { get; set; }
        public ResourceHashMap<Property>? Properties { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Type = reader.ReadByte();
            this.Unknown_11h = reader.ReadByte();
            this.Unknown_12h = reader.ReadUInt16();
            this.Unknown_14h = reader.ReadUInt32();
            this.NamePointer = reader.ReadUInt64();
            this.NameLength1 = reader.ReadUInt16();
            this.NameLength2 = reader.ReadUInt16();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.TagsPointer = reader.ReadUInt64();
            this.PropertiesPointer = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();

            // read reference data
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
            this.Tags = reader.ReadBlockAt<Tags>(
                this.TagsPointer // offset
            );
            this.Properties = reader.ReadBlockAt<ResourceHashMap<Property>>(
                this.PropertiesPointer // offset
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
            this.NameLength1 = (ushort)(this.Name != null ? this.Name.Value.Length : 0);
            this.NameLength2 = (ushort)(this.Name != null ? this.Name.Value.Length + 1 : 0);
            this.TagsPointer = (ulong)(this.Tags?.BlockPosition ?? 0);
            this.PropertiesPointer = (ulong)(this.Properties?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Type);
            writer.Write(this.Unknown_11h);
            writer.Write(this.Unknown_12h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.NamePointer);
            writer.Write(this.NameLength1);
            writer.Write(this.NameLength2);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.TagsPointer);
            writer.Write(this.PropertiesPointer);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            if (Tags != null) list.Add(Tags);
            if (Properties != null) list.Add(Properties);
            return list.ToArray();
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 16;
            var type = reader.ReadByte();
            reader.Position -= 17;

            switch (type)
            {
                case 1: return new ClipAnimation();
                case 2: return new ClipAnimations();
                default: throw new Exception("Unknown clip type");
            }
        }
    }
}
