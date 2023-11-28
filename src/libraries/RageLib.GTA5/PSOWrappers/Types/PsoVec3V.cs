// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System.Numerics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoVec3V : IPsoValue
    {
        public Vector3 Value { get; set; }
        public float Pad_Ch { get; set; }

        public void Read(PsoDataReader reader)
        {
            Value = reader.ReadVector3();
            Pad_Ch = reader.ReadSingle();
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
