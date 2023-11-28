// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class GrcVertexFormat : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public VertexDeclarationFlags Flags;
        public ushort Stride;
        public byte Unknown_07h;
        public byte ComponentsCount;
        public VertexDeclarationTypes Types;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Flags = (VertexDeclarationFlags)reader.ReadUInt32();
            Stride = reader.ReadUInt16();
            Unknown_07h = reader.ReadByte();
            ComponentsCount = reader.ReadByte();
            Types = (VertexDeclarationTypes)reader.ReadUInt64();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write((ushort)Flags);
            writer.Write(Stride);
            writer.Write(Unknown_07h);
            writer.Write(ComponentsCount);
            writer.Write((ulong)Types);
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
        GTA4_1 = 0x6755555555996996,
    }
}
