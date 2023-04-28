// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Expressions
{
    public class Unknown_E_002 : ResourceSystemBlock
    {
        public override long BlockLength => 0xA0;

        // structure data
        private float Unknown_0h;
        private float Unknown_4h;
        private float Unknown_8h;
        private uint Unknown_Ch;
        private float Unknown_10h;
        private float Unknown_14h;
        private float Unknown_18h;
        private uint Unknown_1Ch;
        private float Unknown_20h;
        private float Unknown_24h;
        private float Unknown_28h;
        private uint Unknown_2Ch;
        private float Unknown_30h;
        private float Unknown_34h;
        private float Unknown_38h;
        private uint Unknown_3Ch;
        private float Unknown_40h;
        private float Unknown_44h;
        private float Unknown_48h;
        private uint Unknown_4Ch;
        private float Unknown_50h;
        private float Unknown_54h;
        private float Unknown_58h;
        private uint Unknown_5Ch;
        private float Unknown_60h;
        private float Unknown_64h;
        private float Unknown_68h;
        private uint Unknown_6Ch;
        private float Unknown_70h;
        private float Unknown_74h;
        private float Unknown_78h;
        private uint Unknown_7Ch;
        private float Unknown_80h;
        private float Unknown_84h;
        private float Unknown_88h;
        private uint Unknown_8Ch;
        private float Unknown_90h;
        private float Unknown_94h;
        private float Unknown_98h;
        private uint Unknown_9Ch;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadSingle();
            this.Unknown_4h = reader.ReadSingle();
            this.Unknown_8h = reader.ReadSingle();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Unknown_10h = reader.ReadSingle();
            this.Unknown_14h = reader.ReadSingle();
            this.Unknown_18h = reader.ReadSingle();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadSingle();
            this.Unknown_24h = reader.ReadSingle();
            this.Unknown_28h = reader.ReadSingle();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadSingle();
            this.Unknown_34h = reader.ReadSingle();
            this.Unknown_38h = reader.ReadSingle();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.Unknown_40h = reader.ReadSingle();
            this.Unknown_44h = reader.ReadSingle();
            this.Unknown_48h = reader.ReadSingle();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h = reader.ReadSingle();
            this.Unknown_54h = reader.ReadSingle();
            this.Unknown_58h = reader.ReadSingle();
            this.Unknown_5Ch = reader.ReadUInt32();
            this.Unknown_60h = reader.ReadSingle();
            this.Unknown_64h = reader.ReadSingle();
            this.Unknown_68h = reader.ReadSingle();
            this.Unknown_6Ch = reader.ReadUInt32();
            this.Unknown_70h = reader.ReadSingle();
            this.Unknown_74h = reader.ReadSingle();
            this.Unknown_78h = reader.ReadSingle();
            this.Unknown_7Ch = reader.ReadUInt32();
            this.Unknown_80h = reader.ReadSingle();
            this.Unknown_84h = reader.ReadSingle();
            this.Unknown_88h = reader.ReadSingle();
            this.Unknown_8Ch = reader.ReadUInt32();
            this.Unknown_90h = reader.ReadSingle();
            this.Unknown_94h = reader.ReadSingle();
            this.Unknown_98h = reader.ReadSingle();
            this.Unknown_9Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
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
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_74h);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_7Ch);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_84h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_8Ch);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_94h);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_9Ch);
        }
    }
}
