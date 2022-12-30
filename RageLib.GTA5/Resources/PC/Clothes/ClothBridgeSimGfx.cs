// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // pgBase
    // clothBridgeSimGfx
    public class ClothBridgeSimGfx : PgBase64
    {
        public override long BlockLength => 0x140;

        // structure data
        public uint Count;
        public uint Unknown_14h;
        public uint Unknown_18h;
        public uint Unknown_1Ch; // 0x00000000
        public SimpleList64<float> PinRadius0;
        public SimpleList64<float> PinRadius1;
        public SimpleList64<float> PinRadius2;
        public SimpleList64<float> PinRadius3;
        public SimpleList64<float> VertexWeight0;
        public SimpleList64<float> VertexWeight1;
        public SimpleList64<float> VertexWeight2;
        public SimpleList64<float> VertexWeight3;
        public SimpleList64<float> InflationScale0;
        public SimpleList64<float> InflationScale1;
        public SimpleList64<float> InflationScale2;
        public SimpleList64<float> InflationScale3;
        public SimpleList64<ushort> ClothDisplayMap0;
        public SimpleList64<ushort> ClothDisplayMap1;
        public SimpleList64<ushort> ClothDisplayMap2;
        public SimpleList64<ushort> ClothDisplayMap3;
        public ulong Unknown_120h; // 0x0000000000000000
        public SimpleList64<uint> Unknown_128h;
        public ulong Unknown_138h; // 0x0000000000000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Count = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.PinRadius0 = reader.ReadValueList<float>();
            this.PinRadius1 = reader.ReadValueList<float>();
            this.PinRadius2 = reader.ReadValueList<float>();
            this.PinRadius3 = reader.ReadValueList<float>();
            this.VertexWeight0 = reader.ReadValueList<float>();
            this.VertexWeight1 = reader.ReadValueList<float>();
            this.VertexWeight2 = reader.ReadValueList<float>();
            this.VertexWeight3 = reader.ReadValueList<float>();
            this.InflationScale0 = reader.ReadValueList<float>();
            this.InflationScale1 = reader.ReadValueList<float>();
            this.InflationScale2 = reader.ReadValueList<float>();
            this.InflationScale3 = reader.ReadValueList<float>();
            this.ClothDisplayMap0 = reader.ReadValueList<ushort>();
            this.ClothDisplayMap1 = reader.ReadValueList<ushort>();
            this.ClothDisplayMap2 = reader.ReadValueList<ushort>();
            this.ClothDisplayMap3 = reader.ReadValueList<ushort>();
            this.Unknown_120h = reader.ReadUInt64();
            this.Unknown_128h = reader.ReadValueList<uint>();
            this.Unknown_138h = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Count);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.WriteValueList(this.PinRadius0);
            writer.WriteValueList(this.PinRadius1);
            writer.WriteValueList(this.PinRadius2);
            writer.WriteValueList(this.PinRadius3);
            writer.WriteValueList(this.VertexWeight0);
            writer.WriteValueList(this.VertexWeight1);
            writer.WriteValueList(this.VertexWeight2);
            writer.WriteValueList(this.VertexWeight3);
            writer.WriteValueList(this.InflationScale0);
            writer.WriteValueList(this.InflationScale1);
            writer.WriteValueList(this.InflationScale2);
            writer.WriteValueList(this.InflationScale3);
            writer.WriteValueList(this.ClothDisplayMap0);
            writer.WriteValueList(this.ClothDisplayMap1);
            writer.WriteValueList(this.ClothDisplayMap2);
            writer.WriteValueList(this.ClothDisplayMap3);
            writer.Write(this.Unknown_120h);
            writer.WriteValueList(this.Unknown_128h);
            writer.Write(this.Unknown_138h);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (PinRadius0.Entries is not null) list.Add(PinRadius0.Entries);
            if (PinRadius1.Entries is not null) list.Add(PinRadius1.Entries);
            if (PinRadius2.Entries is not null) list.Add(PinRadius2.Entries);
            if (PinRadius3.Entries is not null) list.Add(PinRadius3.Entries);
            if (VertexWeight0.Entries is not null) list.Add(VertexWeight0.Entries);
            if (VertexWeight1.Entries is not null) list.Add(VertexWeight1.Entries);
            if (VertexWeight2.Entries is not null) list.Add(VertexWeight2.Entries);
            if (VertexWeight3.Entries is not null) list.Add(VertexWeight3.Entries);
            if (InflationScale0.Entries is not null) list.Add(InflationScale0.Entries);
            if (InflationScale1.Entries is not null) list.Add(InflationScale1.Entries);
            if (InflationScale2.Entries is not null) list.Add(InflationScale2.Entries);
            if (InflationScale3.Entries is not null) list.Add(InflationScale3.Entries);
            if (ClothDisplayMap0.Entries is not null) list.Add(ClothDisplayMap0.Entries);
            if (ClothDisplayMap1.Entries is not null) list.Add(ClothDisplayMap1.Entries);
            if (ClothDisplayMap2.Entries is not null) list.Add(ClothDisplayMap2.Entries);
            if (ClothDisplayMap3.Entries is not null) list.Add(ClothDisplayMap3.Entries);
            if (Unknown_128h.Entries is not null) list.Add(Unknown_128h.Entries);
            return list.ToArray();
        }
    }
}
