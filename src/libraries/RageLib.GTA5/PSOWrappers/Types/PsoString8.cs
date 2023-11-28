// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoString8 : IPsoValue
    {
        public int Value { get; set; }

        public PsoString8()
        { }

        public PsoString8(int value)
        {
            this.Value = value;
        }

        public void Read(PsoDataReader reader)
        {
            int z1 = reader.ReadInt32();
            int z2 = reader.ReadInt32();
            Debug.Assert(z2 == 0);

            Value = z1;
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
