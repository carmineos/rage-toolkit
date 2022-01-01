// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources
{
    // datResourceMap ?
    public class DatResourceMap32 : ResourceSystemBlock
    {
        public override long BlockLength => 0x210;

        public byte[] Data { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Data = reader.ReadBytes(0x210);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Data);
        }
    }
}