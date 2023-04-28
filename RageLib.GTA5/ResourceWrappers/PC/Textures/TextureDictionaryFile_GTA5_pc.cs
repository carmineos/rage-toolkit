// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Textures;
using RageLib.ResourceWrappers;
using System;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Textures
{
    /// <summary>
    /// Represents a wrapper for a GTA5 PC texture dictionary file.
    /// </summary>
    public class TextureDictionaryFileWrapper_GTA5_pc : ITextureDictionaryFile
    {
        private PgDictionary64<TextureDX11> textureDictionary;

        /// <summary>
        /// Gets the texture dictionary.
        /// </summary>
        public ITextureDictionary TextureDictionary
        {
            get { return new TextureDictionaryWrapper_GTA5_pc(textureDictionary); }
        }

        public TextureDictionaryFileWrapper_GTA5_pc()
        {
            textureDictionary = new PgDictionary64<TextureDX11>();
            textureDictionary.Hashes = new SimpleList64<uint>();
            textureDictionary.Values = new ResourcePointerList64<TextureDX11>();
        }

        /// <summary>
        /// Loads the texture dictionary from a file.
        /// </summary>
        public void Load(string fileName)
        {
            var resource = new Resource7<PgDictionary64<TextureDX11>>();
            resource.Load(fileName);

            textureDictionary = resource.ResourceData;
        }

        /// <summary>
        /// Saves the texture dictionary to a file.
        /// </summary>
        public void Save(string fileName)
        {
            var w = new TextureDictionaryWrapper_GTA5_pc(textureDictionary);
            w.UpdateClass();

            var resource = new Resource7<PgDictionary64<TextureDX11>>();
            resource.ResourceData = textureDictionary;
            resource.Version = 13;
            resource.Save(fileName);
        }

        public void Load(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<TextureDX11>>();
            resource.Load(stream);

            if (resource.Version != 13)
                throw new Exception("version error");

            textureDictionary = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var w = new TextureDictionaryWrapper_GTA5_pc(textureDictionary);
            w.UpdateClass();

            var resource = new Resource7<PgDictionary64<TextureDX11>>();
            resource.ResourceData = textureDictionary;
            resource.Version = 13;
            resource.Save(stream);
        }
    }
}