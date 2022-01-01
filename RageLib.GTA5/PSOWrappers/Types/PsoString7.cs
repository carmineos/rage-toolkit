// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoString7 : IPsoValue
    {
        public int Value { get; set; }

        public PsoString7()
        { }

        public PsoString7(int value)
        {
            this.Value = value;
        }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadInt32();          
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
