﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources
{
    // datResourceFileHeader
    public struct DatResourceFileHeader
    {
        public uint Id;
        public uint Flags;
        public DatResourceInfo ResourceInfo;

        public DatResourceFileHeader(uint id, uint flags, uint virtualFlags, uint physicalFlags)
        {
            Id = id;
            Flags = flags;
            ResourceInfo = new DatResourceInfo(virtualFlags, physicalFlags);
        }
    }
}
