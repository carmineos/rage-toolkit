// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Expressions
{
    // pgBase
    // crExpressions
    public class Expression : PgBase64
    {
        public override long BlockLength => 0x90;

        // structure data
        public uint Unknown_10h;
        public uint Unknown_14h;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public ResourcePointerList64<Unknown_E_001> Unknown_20h;
        public SimpleList64<uint> Unknown_30h;
        public ResourceSimpleList64<Unknown_E_002> Unknown_40h;
        public SimpleList64<uint> Unknown_50h;
        public ulong NamePointer;
        public ushort NameLength1;
        public ushort NameLength2;
        public uint Unknown_6Ch;
        public uint Unknown_70h;
        public uint Unknown_74h;
        public ushort len;
        public ushort Unknown_7Ah;
        public uint Unknown_7Ch;
        public uint Unknown_80h;
        public uint Unknown_84h;
        public uint Unknown_88h;
        public uint Unknown_8Ch;

        // reference data
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
            this.Unknown_20h = reader.ReadBlock<ResourcePointerList64<Unknown_E_001>>();
            this.Unknown_30h = reader.ReadValueList<uint>();
            this.Unknown_40h = reader.ReadList<Unknown_E_002>();
            this.Unknown_50h = reader.ReadValueList<uint>();
            this.NamePointer = reader.ReadUInt64();
            this.NameLength1 = reader.ReadUInt16();
            this.NameLength2 = reader.ReadUInt16();
            this.Unknown_6Ch = reader.ReadUInt32();
            this.Unknown_70h = reader.ReadUInt32();
            this.Unknown_74h = reader.ReadUInt32();
            this.len = reader.ReadUInt16();
            this.Unknown_7Ah = reader.ReadUInt16();
            this.Unknown_7Ch = reader.ReadUInt32();
            this.Unknown_80h = reader.ReadUInt32();
            this.Unknown_84h = reader.ReadUInt32();
            this.Unknown_88h = reader.ReadUInt32();
            this.Unknown_8Ch = reader.ReadUInt32();

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
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.WriteBlock(this.Unknown_20h);
            writer.WriteValueList(this.Unknown_30h);
            writer.WriteList(this.Unknown_40h);
            writer.WriteValueList(this.Unknown_50h);
            writer.Write(this.NamePointer);
            writer.Write(this.NameLength1);
            writer.Write(this.NameLength2);
            writer.Write(this.Unknown_6Ch);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_74h);
            writer.Write(this.len);
            writer.Write(this.Unknown_7Ah);
            writer.Write(this.Unknown_7Ch);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_84h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_8Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            if (Unknown_30h.Entries != null) list.Add(Unknown_30h.Entries);
            if (Unknown_40h.Entries != null) list.Add(Unknown_40h.Entries);
            if (Unknown_50h.Entries != null) list.Add(Unknown_50h.Entries);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, Unknown_20h)
            };
        }
    }
}
