// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // pgBase
    // pgBaseRefCounted
    // ptxEmitterRule
    public class EmitterRule : PgBase64
    {
        public override long BlockLength => 0x630;

        // structure data
        public uint Unknown_10h;
        public uint Unknown_14h; // 0x00000000
        public float Unknown_18h; // 0x40833333 
        public uint Unknown_1Ch; // 0x00000000
        public ulong NamePointer;
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong p2;
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong p3;
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong p4;
        public ulong Unknown_60h; // 0x0000000000000000
        public ulong Unknown_68h; // 0x0000000000000000
        public ulong Unknown_70h; // 0x0000000000000000
        public KeyframeProp KeyframeProp0;
        public KeyframeProp KeyframeProp1;
        public KeyframeProp KeyframeProp2;
        public KeyframeProp KeyframeProp3;
        public KeyframeProp KeyframeProp4;
        public KeyframeProp KeyframeProp5;
        public KeyframeProp KeyframeProp6;
        public KeyframeProp KeyframeProp7;
        public KeyframeProp KeyframeProp8;
        public KeyframeProp KeyframeProp9;
        public ResourcePointerList64<KeyframeProp> KeyframeProps;
        public uint Unknown_628h;
        public uint Unknown_62Ch; // 0x00000000

        // reference data
        public string_r? Name { get; set; }
        public Domain? p2data { get; set; }
        public Domain? p3data { get; set; }
        public Domain? p4data { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadSingle();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.NamePointer = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.p2 = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.p3 = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.p4 = reader.ReadUInt64();
            this.Unknown_60h = reader.ReadUInt64();
            this.Unknown_68h = reader.ReadUInt64();
            this.Unknown_70h = reader.ReadUInt64();
            this.KeyframeProp0 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp1 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp2 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp3 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp4 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp5 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp6 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp7 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp8 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp9 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProps = reader.ReadPointerList<KeyframeProp>();
            this.Unknown_628h = reader.ReadUInt32();
            this.Unknown_62Ch = reader.ReadUInt32();

            // read reference data
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
            this.p2data = reader.ReadBlockAt<Domain>(
                this.p2 // offset
            );
            this.p3data = reader.ReadBlockAt<Domain>(
                this.p3 // offset
            );
            this.p4data = reader.ReadBlockAt<Domain>(
                this.p4 // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.NamePointer = (ulong)(this.Name?.BlockPosition ?? 0);
            this.p2 = (ulong)(this.p2data?.BlockPosition ?? 0);
            this.p3 = (ulong)(this.p3data?.BlockPosition ?? 0);
            this.p4 = (ulong)(this.p4data?.BlockPosition ?? 0);
            //this.refcnt2 = (ushort)(this.refs != null ? this.refs.Count : 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.NamePointer);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.p2);
            writer.Write(this.Unknown_40h);
            writer.Write(this.p3);
            writer.Write(this.Unknown_50h);
            writer.Write(this.p4);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_70h);
            writer.WriteBlock(this.KeyframeProp0);
            writer.WriteBlock(this.KeyframeProp1);
            writer.WriteBlock(this.KeyframeProp2);
            writer.WriteBlock(this.KeyframeProp3);
            writer.WriteBlock(this.KeyframeProp4);
            writer.WriteBlock(this.KeyframeProp5);
            writer.WriteBlock(this.KeyframeProp6);
            writer.WriteBlock(this.KeyframeProp7);
            writer.WriteBlock(this.KeyframeProp8);
            writer.WriteBlock(this.KeyframeProp9);
            writer.WritePointerList(this.KeyframeProps);
            writer.Write(this.Unknown_628h);
            writer.Write(this.Unknown_62Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Name != null) list.Add(Name);
            if (p2data != null) list.Add(p2data);
            if (p3data != null) list.Add(p3data);
            if (p4data != null) list.Add(p4data);
            if (KeyframeProps.Entries != null) list.Add(KeyframeProps.Entries);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(120, KeyframeProp0),
                new Tuple<long, IResourceBlock>(264, KeyframeProp1),
                new Tuple<long, IResourceBlock>(408, KeyframeProp2),
                new Tuple<long, IResourceBlock>(552, KeyframeProp3),
                new Tuple<long, IResourceBlock>(696, KeyframeProp4),
                new Tuple<long, IResourceBlock>(840, KeyframeProp5),
                new Tuple<long, IResourceBlock>(984, KeyframeProp6),
                new Tuple<long, IResourceBlock>(1128, KeyframeProp7),
                new Tuple<long, IResourceBlock>(1272, KeyframeProp8),
                new Tuple<long, IResourceBlock>(1416, KeyframeProp9)
            };
        }
    }
}
