// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;

namespace RageLib.Resources.GTA4.PC.Drawables
{
    public class RmcLod : ResourceSystemBlock
    {
        public override long BlockLength => 0x10;

        // structure data
        public ResourcePointerList32<GrmModel> Models;

        public RmcLod()
        {
            Models = new ResourcePointerList32<GrmModel>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            Models = reader.ReadBlock<ResourcePointerList32<GrmModel>>(reader, parameters);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.WriteBlock(Models);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[]
            {
                new Tuple<long, IResourceBlock>(0x0, Models),
            };
        }
    }
}
