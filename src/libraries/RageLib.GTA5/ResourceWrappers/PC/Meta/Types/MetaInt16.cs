// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaInt16 : IMetaValue
    {
        public short Value { get; set; }

        public MetaInt16()
        { }

        public MetaInt16(short value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadInt16();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
