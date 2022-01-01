// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crPropertyAttributeQuaternion
    public class PropertyAttributeQuaternion : PropertyAttribute
    {
        public override long BlockLength => 0x30;

        // structure data
        public float X;
        public float Y;
        public float Z;
        public float W;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.X = reader.ReadSingle();
            this.Y = reader.ReadSingle();
            this.Z = reader.ReadSingle();
            this.W = reader.ReadSingle();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.X);
            writer.Write(this.Y);
            writer.Write(this.Z);
            writer.Write(this.W);
        }
    }
}
