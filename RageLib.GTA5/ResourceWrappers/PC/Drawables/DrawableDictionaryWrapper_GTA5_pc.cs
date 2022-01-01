// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.ResourceWrappers.Drawables;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Drawables
{
    public class DrawableDictionaryWrapper_GTA5_pc : IDrawableDictionary
    {
        private PgDictionary64<GtaDrawable> drawableDictionary;

        public DrawableDictionaryWrapper_GTA5_pc(PgDictionary64<GtaDrawable> drawableDictionary)
        {
            this.drawableDictionary = drawableDictionary;
        }

        public IDrawableList Drawables
        {
            get
            {
                return new DrawableListWrapper_GTA5_pc(drawableDictionary.Values.Entries);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public uint GetHash(int index)
        {
            return (uint)drawableDictionary.Hashes.Entries[index];
        }
    }
}