// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.Common
{
    public struct ResourcePointerList64<T> where T : IResourceSystemBlock, new()
    {
        // structure data
        public ArrayInfo64 ArrayInfo;

        // reference data
        public ResourcePointerArray64<T>? Entries { get; set; }
        
        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.ArrayInfo = reader.ReadStruct<ArrayInfo64>();

            this.Entries = reader.ReadBlockAt<ResourcePointerArray64<T>>(
                this.ArrayInfo.EntriesPointer, // offset
                this.ArrayInfo.EntriesCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update...
            this.ArrayInfo.EntriesPointer = (ulong)(this.Entries?.BlockPosition ?? 0);
            this.ArrayInfo.EntriesCount = (ushort)(this.Entries?.Count ?? 0);
            this.ArrayInfo.EntriesCapacity = (ushort)(this.Entries?.Count ?? 0);

            // write...
            writer.WriteStruct(ArrayInfo);
        }
    }
}
