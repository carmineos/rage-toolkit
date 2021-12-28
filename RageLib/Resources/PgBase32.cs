// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Resources
{
    // pgBase
    public class PgBase32 : DatBase32
    {
        public override long BlockLength => 0x8;

        // structure data
        public uint PageMapPointer;

        // reference data
        public DatResourceMap32 PageMap;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.PageMapPointer = reader.ReadUInt32();

            // read reference data
            this.PageMap = reader.ReadBlockAt<DatResourceMap32>(this.PageMapPointer);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.PageMapPointer = (uint)(this.PageMap?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.PageMapPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return PageMap == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { PageMap };
        }
    }
}