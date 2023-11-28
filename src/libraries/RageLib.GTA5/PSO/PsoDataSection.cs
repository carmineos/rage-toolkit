// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.PSO
{
    public class PsoDataSection
    {
        public byte[] Data { get; set; }

        public void Read(DataReader reader)
        {
            var Ident = reader.ReadUInt32();
            var Length = reader.ReadInt32();
            reader.Position -= 8;
            this.Data = reader.ReadBytes(Length);
        }

        public void Write(DataWriter writer)
        {      
            writer.Write(Data);
            writer.Position -= Data.Length;
            writer.Write((uint)0x5053494E);
            writer.Write((uint)(Data.Length));
            writer.Position += Data.Length - 8;
        }
    }
}
