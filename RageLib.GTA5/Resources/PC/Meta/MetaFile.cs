// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Meta
{
    // psoResourceData
    public class MetaFile : PgBase64
    {
        public override long BlockLength => 0x70;

        // structure data
        private int Unknown_10h = 0x50524430;
        private short Unknown_14h = 0x0079;
        private byte HasUselessData;
        private byte Unknown_17h = 0x00;
        private int Unknown_18h = 0x00000000;
        public int RootBlockIndex;
        private long StructureInfosPointer;
        private long EnumInfosPointer;
        private long DataBlocksPointer;
        private long NamePointer;
        private long UselessPointer;
        private short StructureInfosCount;
        private short EnumInfosCount;
        private short DataBlocksCount;
        private short Unknown_4Eh = 0x0000;
        private int Unknown_50h = 0x00000000;
        private int Unknown_54h = 0x00000000;
        private int Unknown_58h = 0x00000000;
        private int Unknown_5Ch = 0x00000000;
        private int Unknown_60h = 0x00000000;
        private int Unknown_64h = 0x00000000;
        private int Unknown_68h = 0x00000000;
        private int Unknown_6Ch = 0x00000000;

        // reference data
        public ResourceSimpleArray<StructureInfo> StructureInfos { get; set; }
        public ResourceSimpleArray<EnumInfo> EnumInfos { get; set; }
        public ResourceSimpleArray<DataBlock> DataBlocks { get; set; }
        public string_r Name { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadInt32();
            this.Unknown_14h = reader.ReadInt16();
            this.HasUselessData = reader.ReadByte();
            this.Unknown_17h = reader.ReadByte();
            this.Unknown_18h = reader.ReadInt32();
            this.RootBlockIndex = reader.ReadInt32();
            this.StructureInfosPointer = reader.ReadInt64();
            this.EnumInfosPointer = reader.ReadInt64();
            this.DataBlocksPointer = reader.ReadInt64();
            this.NamePointer = reader.ReadInt64();
            this.UselessPointer = reader.ReadInt64();
            this.StructureInfosCount = reader.ReadInt16();
            this.EnumInfosCount = reader.ReadInt16();
            this.DataBlocksCount = reader.ReadInt16();
            this.Unknown_4Eh = reader.ReadInt16();
            this.Unknown_50h = reader.ReadInt32();
            this.Unknown_54h = reader.ReadInt32();
            this.Unknown_58h = reader.ReadInt32();
            this.Unknown_5Ch = reader.ReadInt32();
            this.Unknown_60h = reader.ReadInt32();
            this.Unknown_64h = reader.ReadInt32();
            this.Unknown_68h = reader.ReadInt32();
            this.Unknown_6Ch = reader.ReadInt32();

            // read reference data
            this.StructureInfos = reader.ReadBlockAt<ResourceSimpleArray<StructureInfo>>(
                (ulong)this.StructureInfosPointer, // offset
                this.StructureInfosCount
            );
            this.EnumInfos = reader.ReadBlockAt<ResourceSimpleArray<EnumInfo>>(
                (ulong)this.EnumInfosPointer, // offset
                this.EnumInfosCount
            );
            this.DataBlocks = reader.ReadBlockAt<ResourceSimpleArray<DataBlock>>(
                (ulong)this.DataBlocksPointer, // offset
                this.DataBlocksCount
            );
            this.Name = reader.ReadBlockAt<string_r>(
                (ulong)this.NamePointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.StructureInfosPointer = this.StructureInfos?.BlockPosition ?? 0;
            this.EnumInfosPointer = this.EnumInfos?.BlockPosition ?? 0;
            this.DataBlocksPointer = this.DataBlocks?.BlockPosition ?? 0;
            this.NamePointer = this.Name?.BlockPosition ?? 0;
            this.UselessPointer = 0;
            this.StructureInfosCount = (short)(this.StructureInfos?.Count ?? 0);
            this.EnumInfosCount = (short)(this.EnumInfos?.Count ?? 0);
            this.DataBlocksCount = (short)(this.DataBlocks?.Count ?? 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.HasUselessData);
            writer.Write(this.Unknown_17h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.RootBlockIndex);
            writer.Write(this.StructureInfosPointer);
            writer.Write(this.EnumInfosPointer);
            writer.Write(this.DataBlocksPointer);
            writer.Write(this.NamePointer);
            writer.Write(this.UselessPointer);
            writer.Write(this.StructureInfosCount);
            writer.Write(this.EnumInfosCount);
            writer.Write(this.DataBlocksCount);
            writer.Write(this.Unknown_4Eh);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (StructureInfos != null) list.Add(StructureInfos);
            if (EnumInfos != null) list.Add(EnumInfos);
            if (DataBlocks != null) list.Add(DataBlocks);
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }
    }
}
