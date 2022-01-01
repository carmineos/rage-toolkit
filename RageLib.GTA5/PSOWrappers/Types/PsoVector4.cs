// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System.Numerics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoVector4 : IPsoValue
    {
        public Vector4 Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadVector4();
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
