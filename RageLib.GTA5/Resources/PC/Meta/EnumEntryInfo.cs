// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.GTA5.PC.Meta
{
    public class EnumEntryInfo : ResourceSystemBlock
    {
        public override long BlockLength => 8;

        // structure data
        public int EntryNameHash { get; set; }
        public int EntryValue { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EntryNameHash = reader.ReadInt32();
            this.EntryValue = reader.ReadInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.EntryNameHash);
            writer.Write(this.EntryValue);
        }
    }
}
