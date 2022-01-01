// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaBool : IMetaValue
    {
        public bool Value { get; set; }

        public MetaBool()
        { }

        public MetaBool(bool value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadByte() != 0;
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value ? (byte)1 : (byte)0);
        }
    }
}
