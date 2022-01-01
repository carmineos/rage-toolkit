// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.RDR2.PC.Drawables
{
    public class ShaderParameter : ResourceSystemBlock
    {
        public override long BlockLength => 0xB0;

        // structure data
        public byte Unknown_00h;
        public byte Unknown_01h;
        public ushort Unknown_02h;
        public ulong DataPointer;

        // reference data

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
