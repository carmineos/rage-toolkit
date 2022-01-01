// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoUInt32Hex : IPsoValue
    {
        public uint Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            this.Value = reader.ReadUInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(this.Value);
        }
    }
}
