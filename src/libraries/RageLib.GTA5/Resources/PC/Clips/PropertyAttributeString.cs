// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crPropertyAttributeString
    public class PropertyAttributeString : PropertyAttribute
    {
        public override long BlockLength => 0x30;

        // structure data
        public ulong ValuePointer;
        public ushort ValueLength1;
        public ushort ValueLength2;
        public uint Unknown_2Ch; // 0x00000000

        // reference data
        public string_r? Value { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.ValuePointer = reader.ReadUInt64();
            this.ValueLength1 = reader.ReadUInt16();
            this.ValueLength2 = reader.ReadUInt16();
            this.Unknown_2Ch = reader.ReadUInt32();

            // read reference data
            this.Value = reader.ReadBlockAt<string_r>(
                this.ValuePointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.ValuePointer = (ulong)(this.Value?.BlockPosition ?? 0);
            this.ValueLength1 = (ushort)(this.Value != null ? this.Value.Value.Length : 0);
            this.ValueLength2 = (ushort)(this.Value != null ? this.Value.Value.Length + 1 : 0);

            // write structure data
            writer.Write(this.ValuePointer);
            writer.Write(this.ValueLength1);
            writer.Write(this.ValueLength2);
            writer.Write(this.Unknown_2Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Value != null) list.Add(Value);
            return list.ToArray();
        }
    }
}
