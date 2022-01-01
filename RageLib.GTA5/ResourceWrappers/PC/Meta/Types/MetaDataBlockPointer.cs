// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaDataBlockPointer : IMetaValue
    {
        public readonly StructureEntryInfo info;

        public int BlockIndex { get; set; }

        public byte[] Data { get; set; }

        public MetaDataBlockPointer(StructureEntryInfo info)
        {
            this.info = info;
        }

        public MetaDataBlockPointer(StructureEntryInfo info, byte[] data)
        {
            this.info = info;
            this.Data = data;
        }

        public void Read(DataReader reader)
        {
            this.BlockIndex = reader.ReadInt32();
            var unk1 = reader.ReadInt32();
            if (unk1 != 0)
                throw new System.Exception("4h should be 0");
        }

        public void Write(DataWriter writer)
        {
            writer.Write(BlockIndex);
            writer.Write((int)0);
        }
    }
}
