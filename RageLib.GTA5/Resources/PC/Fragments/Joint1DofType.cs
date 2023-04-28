// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // phJoint1DofType
    public class Joint1DofType : JointType
    {
        public override long BlockLength => 0xB0;

        // structure data
        private Vector4 Unknown_20h;
        private Vector4 Unknown_30h;
        private Vector4 Unknown_40h;
        private Vector4 Unknown_50h;
        private Vector4 Unknown_60h;
        private Vector4 Unknown_70h;
        private Vector4 Unknown_80h;
        private Vector4 Unknown_90h;
        private Vector2 Unknown_A0h;
        private Vector2 Unknown_A8h;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_20h = reader.ReadVector4();
            this.Unknown_30h = reader.ReadVector4();
            this.Unknown_40h = reader.ReadVector4();
            this.Unknown_50h = reader.ReadVector4();
            this.Unknown_60h = reader.ReadVector4();
            this.Unknown_70h = reader.ReadVector4();
            this.Unknown_80h = reader.ReadVector4();
            this.Unknown_90h = reader.ReadVector4();
            this.Unknown_A0h = reader.ReadVector2();
            this.Unknown_A8h = reader.ReadVector2();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_A0h);
            writer.Write(this.Unknown_A8h);
        }
    }
}
