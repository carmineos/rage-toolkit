// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoEnumInt32 : IPsoValue
    {
        public readonly PsoEnumInfo TypeInfo;

        public int Value { get; set; }

        public PsoEnumInt32(PsoEnumInfo typeInfo)
        {
            this.TypeInfo = typeInfo;
        }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
