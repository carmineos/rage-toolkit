// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaSByte : IMetaValue
    {
        public sbyte Value { get; set; }

        public MetaSByte()
        { }

        public MetaSByte(sbyte value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = unchecked((sbyte)reader.ReadByte());
        }

        public void Write(DataWriter writer)
        {
            writer.Write(unchecked((byte)this.Value));
        }
    }
}
