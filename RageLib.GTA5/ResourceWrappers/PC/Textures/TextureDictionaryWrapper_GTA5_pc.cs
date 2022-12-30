// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Hash;
using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Textures;
using RageLib.ResourceWrappers;
using System;
using System.Collections.Generic;

namespace RageLib.GTA5.ResourceWrappers.PC.Textures
{
    /// <summary>
    /// Represents a wrapper for a GTA5 PC texture dictionary.
    /// </summary>
    public class TextureDictionaryWrapper_GTA5_pc : ITextureDictionary
    {
        private PgDictionary64<TextureDX11> textureDictionary;

        public ITextureList Textures
        {
            get
            {
                if (textureDictionary.Values.Entries != null)
                    return new TextureListWrapper_GTA5_pc(textureDictionary.Values.Entries);
                else
                    return null;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public TextureDictionaryWrapper_GTA5_pc()
        { }

        public TextureDictionaryWrapper_GTA5_pc(PgDictionary64<TextureDX11> baseClass)
        {
            this.textureDictionary = baseClass;
        }

        public void UpdateClass()
        {

            // structure

            textureDictionary.VFT = 0x0000000140570fd0;
            textureDictionary.PageMapPointer = 0x0000000000000000;
            textureDictionary.Count = 0x00000001;
            textureDictionary.Unknown_1Ch = 0x00000000;

            // references

            textureDictionary.PageMap = null;

            if (textureDictionary.Values.Entries != null)
            {

                var theHashList = new List<uint>();
                foreach (var texture in textureDictionary.Values.Entries)
                {
                    uint hash = Jenkins.Hash((string)texture.Name.Data);
                    theHashList.Add(hash);
                }
                theHashList.Sort();

                var bak = textureDictionary.Values.Entries;
                textureDictionary.Hashes.Entries = new SimpleArray<uint>(theHashList.ToArray());
                textureDictionary.Values.Entries = new ResourcePointerArray64<TextureDX11>();
                foreach (uint x in theHashList)
                {
                    foreach (var g in bak)
                    {
                        uint tx = Jenkins.Hash((string)g.Name.Data);
                        if (tx == x)
                            textureDictionary.Values.Entries.Add(g);
                    }
                }

                //textureDictionary.Hashes = new SimpleArray<uint>();            
                //foreach (var texture in textureDictionary.Textures)
                //{
                //    uint hash = Jenkins.Hash((string)texture.Name);
                //    textureDictionary.Hashes.Add((uint_r)hash);
                //}


                //var bak = textureDictionary.Textures;
                //textureDictionary.Textures = new ResourcePointerArray64<Texture_GTA5_pc>();


                foreach (var texture in textureDictionary.Values.Entries)
                {
                    (new TextureWrapper_GTA5_pc(texture)).UpdateClass();
                }

            }
            else
            {



            }

          

        }

        public PgDictionary64<TextureDX11> GetObject()
        {
            return textureDictionary;
        }

    }
}