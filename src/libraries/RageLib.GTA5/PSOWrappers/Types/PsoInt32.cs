// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoInt32 : IPsoValue
    {
        public int Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
