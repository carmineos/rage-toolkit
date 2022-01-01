// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Expressions
{
    public class Unknown_E_001 : ResourceSystemBlock
    {
        public override long BlockLength
        {
            get { return 16 + Data1.Length + Data2.Length + Data3.Length; }
        }

        // structure data
        public uint Unknown_0h;
        public uint len1;
        public uint len2;
        public ushort len3;
        public ushort Unknown_Eh;
        public byte[] Data1;
        public byte[] Data2;
        public byte[] Data3;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt32();
            this.len1 = reader.ReadUInt32();
            this.len2 = reader.ReadUInt32();
            this.len3 = reader.ReadUInt16();
            this.Unknown_Eh = reader.ReadUInt16();
            this.Data1 = reader.ReadBytes((int)len1);
            this.Data2 = reader.ReadBytes((int)len2);
            this.Data3 = reader.ReadBytes((int)len3);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.len1);
            writer.Write(this.len2);
            writer.Write(this.len3);
            writer.Write(this.Unknown_Eh);
            writer.Write(this.Data1);
            writer.Write(this.Data2);
            writer.Write(this.Data3);
        }
    }
}
