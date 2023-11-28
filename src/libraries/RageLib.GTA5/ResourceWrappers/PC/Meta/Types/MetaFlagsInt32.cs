// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaFlagsInt32 : IMetaValue
    {
        public EnumInfo info;
        public uint Value { get; set; }

        public MetaFlagsInt32()
        { }

        public MetaFlagsInt32(EnumInfo info, uint value)
        {
            this.info = info;
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadUInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
