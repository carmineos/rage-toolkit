// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5.PC.Drawables;
using System;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    public class GlassPaneModelInfo : ResourceSystemBlock
    {
        public override long BlockLength => 0x70;

        // structure data
        private Vector4 Unknown_0h;
        private Vector4 Unknown_10h;
        private Vector4 Unknown_20h;
        private Vector2 Unknown_30h;
        private Vector2 Unknown_38h;
        public VertexDeclaration VertexDeclaration;
        private float Unknown_50h;
        private ushort Unknown_54h;
        private ushort Unknown_56h;
        private Vector2 Unknown_58h;
        private Vector4 Unknown_60h;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadVector4();
            this.Unknown_10h = reader.ReadVector4();
            this.Unknown_20h = reader.ReadVector4();
            this.Unknown_30h = reader.ReadVector2();
            this.Unknown_38h = reader.ReadVector2();
            this.VertexDeclaration = reader.ReadBlock<VertexDeclaration>();
            this.Unknown_50h = reader.ReadSingle();
            this.Unknown_54h = reader.ReadUInt16();
            this.Unknown_56h = reader.ReadUInt16();
            this.Unknown_58h = reader.ReadVector2();
            this.Unknown_60h = reader.ReadVector4();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
            writer.WriteBlock(this.VertexDeclaration);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.Unknown_56h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_60h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x40, VertexDeclaration)
            };
        }
    }
}
