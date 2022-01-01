// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoSByte : IPsoValue
    {
        public sbyte Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = (sbyte)reader.ReadByte();
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
