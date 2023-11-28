// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.Numerics;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaVector4 : IMetaValue
    {
        public Vector4 Value { get; set; }

        public MetaVector4()
        { }

        public MetaVector4(float x, float y, float z, float w)
        {
            this.Value = new Vector4(x, y, z, w);
        }

        public void Read(DataReader reader)
        {
            this.Value = reader.ReadVector4();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(Value);
        }
    }
}
