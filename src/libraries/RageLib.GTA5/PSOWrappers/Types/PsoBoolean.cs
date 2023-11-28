// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoBoolean : IPsoValue
    {
        public bool Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadByte() != 0;
        }

        public void Write(DataWriter writer)
        {
        }
    }
}
