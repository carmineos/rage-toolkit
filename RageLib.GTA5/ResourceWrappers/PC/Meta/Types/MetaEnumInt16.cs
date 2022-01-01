// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaEnumInt16 : IMetaValue
    {
        public EnumInfo info;
        public short Value { get; set; }

        public MetaEnumInt16()
        { }

        public MetaEnumInt16(short value)
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
