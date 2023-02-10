// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxd_Sprite
    public class BehaviourSprite : Behaviour
    {
        public override long BlockLength => 0x70;

        // structure data
        private ulong Unknown_10h; // 0x0000000000000000
        private ulong Unknown_18h; // 0x0000000000000000
        private ulong Unknown_20h; // 0x0000000000000000
        private ulong Unknown_28h; // 0x0000000000000000
        private Vector4 Unknown_30h;
        private uint Unknown_40h;
        private float Unknown_44h;
        private float Unknown_48h;
        private float Unknown_4Ch;
        private uint Unknown_50h;
        private float Unknown_54h;
        private float Unknown_58h;
        private uint Unknown_5Ch;
        private uint Unknown_60h;
        private uint Unknown_64h; // 0x00000000
        private ulong Unknown_68h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadVector4();
            this.Unknown_40h = reader.ReadUInt32();
            this.Unknown_44h = reader.ReadSingle();
            this.Unknown_48h = reader.ReadSingle();
            this.Unknown_4Ch = reader.ReadSingle();
            this.Unknown_50h = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadSingle();
            this.Unknown_58h = reader.ReadSingle();
            this.Unknown_5Ch = reader.ReadUInt32();
            this.Unknown_60h = reader.ReadUInt32();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_44h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
        }
    }
}
