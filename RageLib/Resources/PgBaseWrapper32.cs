// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources
{
    // rage::pgBaseWrapper<struct rage::datOwner<T>>
    public class PgBaseWrapper32<T> : PgBase32 where T : DatBase32, new()
    {
        public override long BlockLength => 0x20;

        // structure data
        private uint DataPointer;
        private uint Unknown_0Ch; // 0xCDCDCDCD
        private uint Unknown_10h; // 0xCDCDCDCD
        private uint Unknown_14h; // 0xCDCDCDCD
        private uint Unknown_18h; // 0xCDCDCDCD
        private uint Unknown_1Ch; // 0xCDCDCDCD

        // reference data
        public T? Data { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            DataPointer = reader.ReadUInt32();
            Unknown_0Ch = reader.ReadUInt32();
            Unknown_10h = reader.ReadUInt32();
            Unknown_14h = reader.ReadUInt32();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();

            // read reference data
            Data = reader.ReadBlockAt<T>(DataPointer);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            DataPointer = (uint)(Data?.BlockPosition ?? 0);

            // write structure data
            writer.Write(DataPointer);
            writer.Write(Unknown_0Ch);
            writer.Write(Unknown_10h);
            writer.Write(Unknown_14h);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Data is not null) list.Add(Data);
            return list.ToArray();
        }
    }
}
