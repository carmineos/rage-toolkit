// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.VehicleRecords
{
    public class VehicleRecords : PgBase64
    {
        public override long BlockLength => 0x20;

        // structure data
        public SimpleList64<VehicleRecordsEntry> Entries;

        public VehicleRecords()
        {
            this.Entries = new SimpleList64<VehicleRecordsEntry>();
        }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Entries = reader.ReadBlock<SimpleList64<VehicleRecordsEntry>>();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(this.Entries);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, Entries)
            };
        }
    }
}
