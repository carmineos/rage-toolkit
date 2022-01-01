// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaEnumInt8 : IMetaValue
    {
        public EnumInfo info;
        public sbyte Value { get; set; }

        public MetaEnumInt8()
        { }

        public MetaEnumInt8(sbyte value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = (sbyte)reader.ReadByte();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
