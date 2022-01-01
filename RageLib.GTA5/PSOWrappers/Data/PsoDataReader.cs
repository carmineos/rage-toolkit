// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

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

        private int currentSectionIndex;
        private PsoDataMappingEntry currentDataMappingEntry;

        public override long Length => currentDataMappingEntry.Length;
        
        public override long Position { get; set; }

        public int CurrentSectionIndex 
        { 
            get => currentSectionIndex;
            set 
            {
                currentSectionIndex = value;
                currentDataMappingEntry = psoFile.DataMappingSection.Entries[value];
            } 
        }

        public int CurrentSectionHash => currentDataMappingEntry.NameHash;

        public PsoDataReader(PsoFile psoFile) : base(null, Endianness.BigEndian)
        {
            this.psoFile = psoFile;
            this.stream = new MemoryStream(psoFile.DataSection.Data);
        }

        protected override void ReadFromStreamRaw(Span<byte> span)
        {
            stream.Position = currentDataMappingEntry.Offset;
            stream.Position += Position;

            stream.Read(span);
            Position += span.Length;
        }

        protected override byte ReadByteFromStreamRaw()
        {
            stream.Position = currentDataMappingEntry.Offset;
            stream.Position += Position;

            var b = (byte)stream.ReadByte();
            Position += 1;
            return b;
        }
    }
}
