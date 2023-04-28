// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxShaderVarVector
    public class ShaderVarVector : ShaderVar
    {
        public override long BlockLength => 0x40;

        // structure data
        private uint Unknown_18h;
        private uint Unknown_1Ch;
        private uint Unknown_20h;
        private uint Unknown_24h;
        private uint Unknown_28h;
        private uint Unknown_2Ch;
        private Vector4 Unknown_30h;

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
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadVector4();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
        }
    }
}
