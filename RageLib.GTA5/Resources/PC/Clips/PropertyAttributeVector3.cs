// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crPropertyAttributeVector3
    public class PropertyAttributeVector3 : PropertyAttribute
    {
        public override long BlockLength => 0x30;

        // structure data
        public float X;
        public float Y;
        public float Z;
        private float Unknown_2Ch;

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
            this.Unknown_2Ch = reader.ReadSingle();
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
            writer.Write(this.Unknown_2Ch);
        }
    }
}
