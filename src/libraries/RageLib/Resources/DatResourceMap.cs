// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources
{
    // datResourceMap ?
    public class DatResourceMap : ResourceSystemBlock
    {
        public override long BlockLength => 16 + (8 * (VirtualPagesCount + PhysicalPagesCount));

        // structure data
        public uint Unknown_0h;
        public uint Unknown_4h;
        public byte VirtualPagesCount;
        public byte PhysicalPagesCount;
        public ushort Unknown_Ah;
        public uint Unknown_Ch;
        public ulong[] VirtualPagesPointers;
        public ulong[] PhysicalPagesPointers;

        public DatResourceMap() : this(64, 64) { }

        public DatResourceMap(byte virtualPagesCount, byte physicalPagesCount)
        {
            VirtualPagesCount = virtualPagesCount;
            PhysicalPagesCount = physicalPagesCount;
        }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.VirtualPagesCount = reader.ReadByte();
            this.PhysicalPagesCount = reader.ReadByte();
            this.Unknown_Ah = reader.ReadUInt16();
            this.Unknown_Ch = reader.ReadUInt32();

            if (VirtualPagesCount > 0)
            {
                this.VirtualPagesPointers = new ulong[VirtualPagesCount];
                for (int i = 0; i < VirtualPagesCount; i++)
                    this.VirtualPagesPointers[i] = reader.ReadUInt64();
            }

            if (PhysicalPagesCount > 0)
            {
                this.PhysicalPagesPointers = new ulong[PhysicalPagesCount];
                for (int i = 0; i < PhysicalPagesCount; i++)
                    this.PhysicalPagesPointers[i] = reader.ReadUInt64();
            }
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.VirtualPagesCount);
            writer.Write(this.PhysicalPagesCount);
            writer.Write(this.Unknown_Ah);
            writer.Write(this.Unknown_Ch);

            var pages = VirtualPagesCount + PhysicalPagesCount;
            for (int i = 0; i < pages; i++)
                writer.Write(0UL);
        }
    }
}