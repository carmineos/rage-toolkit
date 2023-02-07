// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Clothes;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Clothes
{
    public class ClothDictionaryFileWrapper_GTA5_pc
    {
        private PgDictionary64<CharacterCloth> clothDictionary;

        public void Load(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<CharacterCloth>>();
            resource.Load(stream);

            clothDictionary = resource.ResourceData;
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<PgDictionary64<CharacterCloth>>();
            resource.Load(fileName);

            clothDictionary = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<CharacterCloth>>();
            resource.ResourceData = clothDictionary;
            resource.Version = 8;
            resource.Save(stream);
        }

        public void Save(string fileName)
        {
            var resource = new Resource7<PgDictionary64<CharacterCloth>>();
            resource.ResourceData = clothDictionary;
            resource.Version = 8;
            resource.Save(fileName);
        }
    }
}
