/*
    Copyright(c) 2016 Neodymium

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

using RageLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RageLib.GTA5.PSO;

namespace RageLib.GTA5.PSOWrappers.Data
{
    public class PsoDataReader : DataReader
    {
        private readonly PsoFile psoFile;
        private readonly Stream stream;

        public override long Length => psoFile.DataMappingSection.Entries[CurrentSectionIndex].Length;
        
        public override long Position { get; set; }

        public int CurrentSectionIndex { get; set; }

        public int CurrentSectionHash => psoFile.DataMappingSection.Entries[CurrentSectionIndex].NameHash;

        public PsoDataReader(PsoFile psoFile) : base(null, Endianess.BigEndian)
        {
            this.psoFile = psoFile;
            this.stream = new MemoryStream(psoFile.DataSection.Data);
        }

        protected override void ReadFromStreamRaw(Span<byte> span)
        {
            stream.Position = psoFile.DataMappingSection.Entries[CurrentSectionIndex].Offset;
            stream.Position += Position;

            stream.Read(span);
            Position += span.Length;
        }

        protected override byte ReadByteFromStreamRaw()
        {
            stream.Position = psoFile.DataMappingSection.Entries[CurrentSectionIndex].Offset;
            stream.Position += Position;

            var b = (byte)stream.ReadByte();
            Position += 1;
            return b;
        }
    }
}
