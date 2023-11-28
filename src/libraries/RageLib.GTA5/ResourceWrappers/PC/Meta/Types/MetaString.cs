// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Text;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaString : IMetaValue
    {
        public readonly StructureEntryInfo info;

        public string Value { get; set; }

        public MetaString(StructureEntryInfo info)
        {
            this.info = info;
        }

        public MetaString(StructureEntryInfo inf, string value)
        {
            this.info = inf;
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadString(info.ReferenceKey);
        }

        public void Write(DataWriter writer)
        {
            writer.Write(Value, info.ReferenceKey);
        }
    }
}
