// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaInt32 : IMetaValue
    {
        public int Value { get; set; }

        public MetaInt32()
        { }

        public MetaInt32(int value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
