// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaFlagsInt8 : IMetaValue
    {
        public EnumInfo info;
        public uint Value { get; set; }

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
