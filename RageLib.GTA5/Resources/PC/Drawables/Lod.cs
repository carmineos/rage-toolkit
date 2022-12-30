// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // rmcLod
    public class Lod : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public ResourcePointerList64<DrawableModel> Models;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            Models = reader.ReadPointerList<DrawableModel>();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.WritePointerList(Models);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Models.Entries != null) list.Add(Models.Entries);
            return list.ToArray();
        }
    }
}
