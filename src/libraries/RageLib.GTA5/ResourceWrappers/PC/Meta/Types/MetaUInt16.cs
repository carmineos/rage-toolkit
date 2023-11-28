// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaUInt16 : IMetaValue
    {
        public ushort Value { get; set; }

        public MetaUInt16()
        { }

        public MetaUInt16(ushort value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadUInt16();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
