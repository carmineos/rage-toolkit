// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;
using System;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public class PsoString3 : IPsoValue
    {
        public string Value { get; set; }

        public void Read(PsoDataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            var BlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);
            var Offset = (int)((blockIndexAndOffset & 0xFFFFF000) >> 12);
            
            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            var count1 = reader.ReadUInt16();
            var count2 = reader.ReadUInt16();

            // one is the length with null terminator, but they are often inverted
            var length = Math.Min(count1, count2);
            var length_null = Math.Max(count1, count2);

            // check they are either equal or differ of 1
            Debug.Assert(length_null - length <= 1);

            var unknown_Ch = reader.ReadUInt32();
            Debug.Assert(unknown_Ch == 0);

            // read reference data...
            if (BlockIndex > 0)
            {
                var backupOfSection = reader.CurrentSectionIndex;
                var backupOfPosition = reader.Position;

                reader.CurrentSectionIndex = BlockIndex - 1;
                reader.Position = Offset;

                Value = reader.ReadString(length);

                reader.CurrentSectionIndex = backupOfSection;
                reader.Position = backupOfPosition;
            }
            else
            {
                Value = null;
            }
        }

        public void Write(DataWriter writer)
        {

        }
    }
}
