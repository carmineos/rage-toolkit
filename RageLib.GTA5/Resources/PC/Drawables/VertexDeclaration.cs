// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class VertexDeclaration : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public VertexDeclarationFlags Flags;
        private ushort Unknown_2h;
        public ushort Stride;
        private byte Unknown_6h;
        public byte ComponentsCount;
        public VertexDeclarationTypes Types;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Flags = (VertexDeclarationFlags)reader.ReadUInt16();
            this.Unknown_2h = reader.ReadUInt16();
            this.Stride = reader.ReadUInt16();
            this.Unknown_6h = reader.ReadByte();
            this.ComponentsCount = reader.ReadByte();
            this.Types = (VertexDeclarationTypes)reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write((ushort)this.Flags);
            writer.Write(this.Unknown_2h);
            writer.Write(this.Stride);
            writer.Write(this.Unknown_6h);
            writer.Write(this.ComponentsCount);
            writer.Write((ulong)this.Types);
        }
    }

    [Flags]
    public enum VertexDeclarationFlags : ushort
    {
        None = 0x0,
        Position = 0x1,
        BlendWeights = 0x2,
        BlendIndices = 0x4,
        Normal = 0x8,
        Color0 = 0x10,
        Color1 = 0x20,
        TexCoord0 = 0x40,
        TexCoord1 = 0x80,
        TexCoord2 = 0x100,
        TexCoord3 = 0x200,
        TexCoord4 = 0x400,
        TexCoord5 = 0x800,
        TexCoord6 = 0x1000,
        TexCoord7 = 0x2000,
        Tangent = 0x4000,
        Binormal = 0x8000,
    }

    public enum VertexDeclarationTypes : ulong
    {
        /// <summary>
        /// Used in most of GTA5 drawables
        /// </summary>
        GTA5_1 = 0x7755555555996996,

        /// <summary>
        /// Used in GTA5 cloth drawables
        /// </summary>
        GTA5_2 = 0x030000000199A006,

        /// <summary>
        /// Used in GTA5 cloth drawables
        /// </summary>
        GTA5_3 = 0x0300000001996006,

        /// <summary>
        /// Used in GTA5 vehicle glass windows drawables
        /// </summary>
        GTA5_4 = 0x7655555555996996,
    }

    // FVFType - D3DDECLTYPE
    public enum VertexComponentTypes : byte // actually a nibble
    {
        Nothing = 0,
        Float16_2 = 1,
        Float = 2,
        Float16_4 = 3,
        FloatUnk = 4,
        Float2 = 5,
        Float3 = 6,
        Float4 = 7,
        UByte4 = 8,
        Color = 9,
        Dec3N = 10,
        Unk1 = 11,
        Unk2 = 12,
        Unk3 = 13,
        Unk4 = 14,
        Unk5 = 15,
    }
}
