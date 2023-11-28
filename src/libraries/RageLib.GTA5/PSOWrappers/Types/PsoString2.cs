// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoString2 : IPsoValue
    {
        public string Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            var BlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);
            var Offset = (int)((blockIndexAndOffset & 0xFFFFF000) >> 12);

            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            // read reference data...
            var backupOfSection = reader.CurrentSectionIndex;
            var backupOfPosition = reader.Position;

            reader.CurrentSectionIndex = BlockIndex - 1;
            reader.Position = Offset;

            Value = reader.ReadString();

            reader.CurrentSectionIndex = backupOfSection;
            reader.Position = backupOfPosition;
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
