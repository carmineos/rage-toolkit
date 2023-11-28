// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaVector3 : IMetaValue
    {
        public Vector3 Value { get; set; }
        public float Pad { get; set; }

        public MetaVector3()
        { }

        public MetaVector3(float x, float y, float z)
        {
            this.Value = new Vector3(x, y, z);
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadVector3();
            this.Pad = reader.ReadSingle();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(Value);
            writer.Write(this.Pad);
        }
    }
}
