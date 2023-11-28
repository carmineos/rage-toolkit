// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public abstract class MetaArrayLocal : IMetaValue
    {
        public readonly StructureEntryInfo entryInfo;

        public MetaArrayLocal(StructureEntryInfo entryInfo)
        {
            this.entryInfo = entryInfo;
        }

        public abstract void Read(DataReader reader);

        public abstract void Write(DataWriter writer);
    }

    public class MetaArrayLocal<T> : MetaArrayLocal where T : unmanaged
    {
        public T[] Value { get; set; }

        public MetaArrayLocal(StructureEntryInfo entryInfo) : base(entryInfo)
        {

        }

        public override void Read(DataReader reader)
        {
            Value = reader.ReadArray<T>(entryInfo.ReferenceKey);
        }

        public override void Write(DataWriter writer)
        {
            writer.WriteArray<T>(Value);
        }
    }
}
