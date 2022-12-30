// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.Common
{
    public struct SimpleList64<T> where T : unmanaged
    {
        // structure data
        public ArrayInfo64 ArrayInfo;

        // reference data
        public SimpleArray<T>? Entries { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.ArrayInfo = reader.ReadStruct<ArrayInfo64>();

            // read reference data
            this.Entries = reader.ReadBlockAt<SimpleArray<T>>(
                this.ArrayInfo.EntriesPointer, // offset
                this.ArrayInfo.EntriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.ArrayInfo.EntriesPointer = (ulong)(this.Entries?.BlockPosition ?? 0);
            this.ArrayInfo.EntriesCount = (ushort)(this.Entries?.Count ?? 0);
            this.ArrayInfo.EntriesCapacity = (ushort)(this.Entries?.Count ?? 0);

            // write structure data
            writer.WriteStruct(this.ArrayInfo);
        }
    }
}
