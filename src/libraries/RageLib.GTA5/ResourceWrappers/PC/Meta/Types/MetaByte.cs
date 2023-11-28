// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaByte : IMetaValue
    {
        public byte Value { get; set; }

        public MetaByte()
        { }

        public MetaByte(byte value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadByte();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
