// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaFloat : IMetaValue
    {
        public float Value { get; set; }

        public MetaFloat()
        { }

        public MetaFloat(float value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadSingle();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(Value);
        }
    }
}
