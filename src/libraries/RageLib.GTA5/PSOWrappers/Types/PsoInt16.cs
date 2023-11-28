// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoInt16 : IPsoValue
    {
        public short Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadInt16();
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
