// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Navigations
{
    public class Sector : ResourceSystemBlock
    {
        public override long BlockLength => 0x60;

        // structure data
        public Vector4 Unknown_0h;
        public Vector4 Unknown_10h;
        public uint Unknown_20h;
        public uint Unknown_24h;
        public uint Unknown_28h;
        public ulong DataPointer;
        public ulong SubTree1Pointer;
        public ulong SubTree2Pointer;
        public ulong SubTree3Pointer;
        public ulong SubTree4Pointer;
        public uint Unknown_54h; // 0x00000000
        public uint Unknown_58h; // 0x00000000
        public uint Unknown_5Ch; // 0x00000000

        // reference data
        public SectorData? Data { get; set; }
        public Sector? SubTree1 { get; set; }
        public Sector? SubTree2 { get; set; }
        public Sector? SubTree3 { get; set; }
        public Sector? SubTree4 { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadVector4();
            this.Unknown_10h = reader.ReadVector4();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.DataPointer = reader.ReadUInt64();
            this.SubTree1Pointer = reader.ReadUInt64();
            this.SubTree2Pointer = reader.ReadUInt64();
            this.SubTree3Pointer = reader.ReadUInt64();
            this.SubTree4Pointer = reader.ReadUInt64();
            this.Unknown_54h = reader.ReadUInt32();
            this.Unknown_58h = reader.ReadUInt32();
            this.Unknown_5Ch = reader.ReadUInt32();

            // read reference data
            this.Data = reader.ReadBlockAt<SectorData>(
                this.DataPointer // offset
            );
            this.SubTree1 = reader.ReadBlockAt<Sector>(
                this.SubTree1Pointer // offset
            );
            this.SubTree2 = reader.ReadBlockAt<Sector>(
                this.SubTree2Pointer // offset
            );
            this.SubTree3 = reader.ReadBlockAt<Sector>(
                this.SubTree3Pointer // offset
            );
            this.SubTree4 = reader.ReadBlockAt<Sector>(
                this.SubTree4Pointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.DataPointer = (ulong)(this.Data?.BlockPosition ?? 0);
            this.SubTree1Pointer = (ulong)(this.SubTree1?.BlockPosition ?? 0);
            this.SubTree2Pointer = (ulong)(this.SubTree2?.BlockPosition ?? 0);
            this.SubTree3Pointer = (ulong)(this.SubTree3?.BlockPosition ?? 0);
            this.SubTree4Pointer = (ulong)(this.SubTree4?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.DataPointer);
            writer.Write(this.SubTree1Pointer);
            writer.Write(this.SubTree2Pointer);
            writer.Write(this.SubTree3Pointer);
            writer.Write(this.SubTree4Pointer);
            writer.Write(this.Unknown_54h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Data != null) list.Add(Data);
            if (SubTree1 != null) list.Add(SubTree1);
            if (SubTree2 != null) list.Add(SubTree2);
            if (SubTree3 != null) list.Add(SubTree3);
            if (SubTree4 != null) list.Add(SubTree4);
            return list.ToArray();
        }
    }
}
