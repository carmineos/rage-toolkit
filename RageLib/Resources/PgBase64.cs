// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources
{
    // pgBase
    public class PgBase64 : DatBase64
    {
        public override long BlockLength => 0x10;

        // structure data
        public ulong PageMapPointer;

        // reference data
        public DatResourceMap PageMap;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.PageMapPointer = reader.ReadUInt64();

            // read reference data
            this.PageMap = reader.ReadBlockAt<DatResourceMap>(
                this.PageMapPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.PageMapPointer = (ulong)(this.PageMap?.BlockPosition ?? 0);

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