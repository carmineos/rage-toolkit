// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.ResourceWrappers;
using RageLib.GTA5.ResourceWrappers.PC.Textures;
using System;
using System.Collections.Generic;
using System.IO;

namespace TextureTool.Models
{
    public class TextureDictionaryModel
    {
        private ITextureDictionary textureDictionary;
        private string name;

        public ITextureDictionary TextureDictionary
        {
            get
            {
                return textureDictionary;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<TextureModel> Textures
        {
            get
            {
                var list = new List<TextureModel>();
                if (textureDictionary != null && TextureDictionary.Textures != null)
                {
                    foreach (var texture in textureDictionary.Textures)
                        list.Add(new TextureModel(texture));
                }
                return list;
            }
        }

        public TextureDictionaryModel(ITextureDictionary textureDictionary, string name = "")
        {
            this.textureDictionary = textureDictionary;
            this.name = name;
        }

        public void Import(string fileName, bool replaceOnly = false)
        {
            var info = new FileInfo(fileName);
            var name = info.Name.Replace(".dds", "");

            var existingTexture = (TextureModel)null;
            foreach (var texture in textureDictionary.Textures)
                if (texture.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    existingTexture = new TextureModel(texture);

            if (existingTexture != null)
            {
                existingTexture.Import(fileName);
            }
            else if (!replaceOnly)
            {
                var texture = new TextureWrapper_GTA5_pc();
                var textureModel = new TextureModel(texture);
                textureModel.Name = name;
                textureModel.Import(fileName);

                textureDictionary.Textures.Add(texture);
            }
        }

        public void Export(TextureModel texture, string fileName)
        {
            texture.Export(fileName);
        }

        public void Delete(TextureModel texture)
        {
            textureDictionary.Textures.Remove(texture.Texture);
        }
    }
}