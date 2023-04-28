// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Bounds;
using RageLib.ResourceWrappers.Bounds;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Bounds
{
    public class BoundDictionaryFileWrapper_GTA5_pc : IBoundDictionaryFile
    {
        private PgDictionary64<Bound> boundDictionary;

        public IBoundDictionary BoundDictionary
        {
            get
            {
                return new BoundDictionaryWrapper_GTA5_pc(boundDictionary);
            }
        }

        public void Load(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<Bound>>();
            resource.Load(stream);

            boundDictionary = resource.ResourceData;
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<PgDictionary64<Bound>>();
            resource.Load(fileName);

            boundDictionary = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<Bound>>();
            resource.ResourceData = boundDictionary;
            resource.Version = 43;
            resource.Save(stream);
        }

        public void Save(string fileName)
        {
            var resource = new Resource7<PgDictionary64<Bound>>();
            resource.ResourceData = boundDictionary;
            resource.Version = 43;
            resource.Save(fileName);
        }
    }
}