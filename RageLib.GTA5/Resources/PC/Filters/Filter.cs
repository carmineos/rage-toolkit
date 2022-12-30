// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RageLib.Resources.GTA5.PC.Filters
{
    // crFrameFilter -> this is polymorphic, there are many filters
    public class Filter : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong VFT;
        public uint Unknown_8h; // 0x00000001
        public uint Unknown_Ch;
        public uint Unknown_10h; // 0x00000004
        public uint Unknown_14h; // 0x00000000
        public SimpleList64<ulong> Unknown_18h;
        public SimpleList64<float> Unknown_28h;
        public ulong Unknown_38h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.VFT = reader.ReadUInt64();
            this.Unknown_8h = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadValueList<ulong>();
            this.Unknown_28h = reader.ReadValueList<float>();
            this.Unknown_38h = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.VFT);
            writer.Write(this.Unknown_8h);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.WriteValueList(this.Unknown_18h);
            writer.WriteValueList(this.Unknown_28h);
            writer.Write(this.Unknown_38h);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Unknown_18h.Entries != null) list.Add(Unknown_18h.Entries);
            if (Unknown_28h.Entries != null) list.Add(Unknown_28h.Entries);
            return list.ToArray();
        }
    }
}
