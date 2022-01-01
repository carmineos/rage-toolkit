// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // datBase
    // ptxShaderVar
    public class ShaderVar : DatBase64, IResourceXXSystemBlock
    {
        public override long BlockLength => 24;

        // structure data
        public ulong Unknown_8h; // 0x0000000000000000
        public uint Hash;
        public byte Type;
        public byte Unknown_15h;
        public ushort Unknown_16h;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_8h = reader.ReadUInt64();
            this.Hash = reader.ReadUInt32();
            this.Type = reader.ReadByte();
            this.Unknown_15h = reader.ReadByte();
            this.Unknown_16h = reader.ReadUInt16();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_8h);
            writer.Write(this.Hash);
            writer.Write(this.Type);
            writer.Write(this.Unknown_15h);
            writer.Write(this.Unknown_16h);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 20;
            var type = reader.ReadByte();
            reader.Position -= 21;

            switch (type)
            {
                case 2:
                case 4: return new ShaderVarVector();
                case 6: return new ShaderVarTexture();
                case 7: return new ShaderVarKeyframe();
                default: throw new Exception("Unknown shader var type");
            }
        }
    }
}
