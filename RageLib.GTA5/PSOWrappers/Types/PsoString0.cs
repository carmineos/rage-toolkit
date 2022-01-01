// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoString0 : IPsoValue
    {
        private readonly int length;
        public string Value { get; set; }

        public PsoString0(int length)
        {
            this.length = length;
        }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadString(length);
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
