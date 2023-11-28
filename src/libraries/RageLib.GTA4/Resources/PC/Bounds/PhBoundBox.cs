// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBoundBox : PhBoundPolyhedron 
    {
        public override long BlockLength => 0x160;

        // structure data
        public Vector4 Unknown_0D0h;
        public Vector4 Unknown_0E0h;
        public Vector4 Unknown_0F0h;
        public Vector4 Unknown_100h;
        public Vector4 Unknown_110h;
        public Vector4 Unknown_120h;
        public Vector4 Unknown_130h;
        public Vector4 Unknown_140h;
        public Vector4 Unknown_150h;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_0D0h = reader.ReadVector4();
            Unknown_0E0h = reader.ReadVector4();
            Unknown_0F0h = reader.ReadVector4();
            Unknown_100h = reader.ReadVector4();
            Unknown_110h = reader.ReadVector4();
            Unknown_120h = reader.ReadVector4();
            Unknown_130h = reader.ReadVector4();
            Unknown_140h = reader.ReadVector4();
            Unknown_150h = reader.ReadVector4();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(Unknown_0D0h);
            writer.Write(Unknown_0E0h);
            writer.Write(Unknown_0F0h);
            writer.Write(Unknown_100h);
            writer.Write(Unknown_110h);
            writer.Write(Unknown_120h);
            writer.Write(Unknown_130h);
            writer.Write(Unknown_140h);
            writer.Write(Unknown_150h);
        }
    }
}
