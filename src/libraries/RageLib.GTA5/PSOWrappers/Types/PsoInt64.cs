// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoInt64 : IPsoValue
    {
        public long Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadInt64();
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
