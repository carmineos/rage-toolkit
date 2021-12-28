// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBoundSphere : PhBound 
    {
        public override long BlockLength => 0xA0;

        // structure data
        public Vector4 Unknown_80h;
        private uint Unknown_90h; // 0x00000000
        private uint Unknown_94h; // 0x00000000
        private uint Unknown_98h; // 0x00000000
        private uint Unknown_9Ch; // 0x00000000

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Unknown_80h = reader.ReadVector4();
            Unknown_90h = reader.ReadUInt32();
            Unknown_94h = reader.ReadUInt32();
            Unknown_98h = reader.ReadUInt32();
            Unknown_9Ch = reader.ReadUInt32();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            writer.Write(Unknown_80h);
            writer.Write(Unknown_90h);
            writer.Write(Unknown_94h);
            writer.Write(Unknown_98h);
            writer.Write(Unknown_9Ch);
        }
    }
}
