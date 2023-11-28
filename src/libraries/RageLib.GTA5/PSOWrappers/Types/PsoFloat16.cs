// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoFloat16 : IPsoValue
    {
        public Half Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadHalf();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(Value);
        }
    }
}
