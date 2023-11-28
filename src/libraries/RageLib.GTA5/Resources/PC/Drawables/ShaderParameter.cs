// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class ShaderParameter : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public byte DataType;
        public byte RegisterIndex;
        public ushort Unknown_2h;
        public uint Unknown_4h;
        public ulong DataPointer;

        public IResourceBlock Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.DataType = reader.ReadByte();
            this.RegisterIndex = reader.ReadByte();
            this.Unknown_2h = reader.ReadUInt16();
            this.Unknown_4h = reader.ReadUInt32();
            this.DataPointer = reader.ReadUInt64();

            // DONT READ DATA...
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.DataType);
            writer.Write(this.RegisterIndex);
            writer.Write(this.Unknown_2h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.DataPointer);

            // DONT WRITE DATA
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        //public override IResourceBlock[] GetReferences()
        //{
        //    var list = new List<IResourceBlock>(base.GetReferences());
        //    if (Data != null) list.Add(Data);
        //    return list.ToArray();
        //}
    }
}
