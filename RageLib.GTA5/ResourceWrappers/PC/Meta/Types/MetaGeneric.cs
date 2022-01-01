// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaGeneric : IMetaValue
    {
        public int BlockIndex { get; set; }
        public int Offset { get; set; }

        // Reference values
        public IMetaValue Value { get; set; }

        public void Read(DataReader reader)
        {
            this.BlockIndex = reader.ReadUInt16();
            this.Offset = reader.ReadUInt16();
            var zero_4h = reader.ReadUInt32();
            if (zero_4h != 0)
            {
                throw new Exception("zero_4h should be 0");
            }
        }

        public void Write(DataWriter writer)
        {
            writer.Write((ushort)BlockIndex);
            writer.Write((ushort)Offset);
            writer.Write((uint)0);
        }
    }
}
