/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxShaderVarKeyframe
    public class ShaderVarKeyframe : ShaderVar
    {
        public override long BlockLength => 0x50;

        // structure data
        public uint Unknown_18h;
        public uint Unknown_1Ch; // 0x00000001
        public ulong Unknown_20h; // 0x0000000000000000
        public ArrayHeader64<Unknown_P_009> Unknown_28h_Header;
        public ulong Unknown_38h; // 0x0000000000000000
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000

        // reference data
        public SimpleArray<Unknown_P_009> Unknown_28h_Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h_Header = reader.ReadStruct<ArrayHeader64<Unknown_P_009>>();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();

            // read reference data
            this.Unknown_28h_Data = reader.ReadBlockAt<SimpleArray<Unknown_P_009>>(Unknown_28h_Header.EntriesPointer, Unknown_28h_Header.EntriesCount);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.Unknown_28h_Header.EntriesPointer = (ulong)(this.Unknown_28h_Data != null ? this.Unknown_28h_Data.BlockPosition : 0);
            this.Unknown_28h_Header.EntriesCount = (ushort)(this.Unknown_28h_Data != null ? this.Unknown_28h_Data.Count : 0);
            this.Unknown_28h_Header.EntriesCapacity = (ushort)(this.Unknown_28h_Data != null ? this.Unknown_28h_Data.Count : 0);

            // write structure data
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Unknown_20h);
            writer.WriteStruct(this.Unknown_28h_Header);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Unknown_28h_Data != null) list.Add(Unknown_28h_Data);
            return list.ToArray();
        }
    }
}
