// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Bounds;
using RageLib.ResourceWrappers.Bounds;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Bounds
{
    public class BoundDictionaryWrapper_GTA5_pc : IBoundDictionary
    {
        private PgDictionary64<Bound> boundDictionary;

        public BoundDictionaryWrapper_GTA5_pc(PgDictionary64<Bound> boundDictionary)
        {
            this.boundDictionary = boundDictionary;
        }

        public IBoundList Bounds
        {
            get
            {
                return new BoundListWrapper_GTA5_pc(boundDictionary.Values.Entries);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public uint GetHash(int index)
        {
            return (uint)boundDictionary.Hashes.Entries[index];
        }
    }
}