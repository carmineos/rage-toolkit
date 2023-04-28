// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.ResourceWrappers.Drawables;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Drawables
{
    public class DrawableDictionaryFileWrapper_GTA5_pc : IDrawableDictionaryFile
    {
        private PgDictionary64<GtaDrawable> drawableDictionary;

        public IDrawableDictionary DrawableDictionary
        {
            get
            {
                return new DrawableDictionaryWrapper_GTA5_pc(drawableDictionary);
            }
        }

        public void Load(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<GtaDrawable>>();
            resource.Load(stream);

            drawableDictionary = resource.ResourceData;
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<PgDictionary64<GtaDrawable>>();
            resource.Load(fileName);

            drawableDictionary = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<GtaDrawable>>();
            resource.ResourceData = drawableDictionary;
            resource.Version = 165;
            resource.Save(stream);
        }

        public void Save(string fileName)
        {
            var resource = new Resource7<PgDictionary64<GtaDrawable>>();
            resource.ResourceData = drawableDictionary;
            resource.Version = 165;
            resource.Save(fileName);
        }
    }
}