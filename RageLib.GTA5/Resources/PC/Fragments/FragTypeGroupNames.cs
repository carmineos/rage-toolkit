// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.Common.Simple;
using System;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    public class FragTypeGroupNames : ResourceSystemBlock
    {
        public override long BlockLength => GroupNames.BlockLength + 8;

        // structure data
        public ResourcePointerArray64<string32_r> GroupNames;
        public ulong Unknown_VFT;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int cnt = Convert.ToInt32(parameters[0]);

            // read structure data
            GroupNames = reader.ReadBlock<ResourcePointerArray64<string32_r>>(cnt);
            Unknown_VFT = reader.ReadUInt64();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.WriteBlock(GroupNames);
            writer.Write(Unknown_VFT);
        }

        // Don't add parts on purpose as group names position is assigned first to FragTypeGroup, then the reference here gets updated
        // TODO:   Use a new helper structure to keep a reference to a block

        //public override Tuple<long, IResourceBlock>[] GetParts()
        //{
        //    return new Tuple<long, IResourceBlock>[] {
        //        new Tuple<long, IResourceBlock>(0x0, GroupNames)
        //    };
        //}
    }
}
