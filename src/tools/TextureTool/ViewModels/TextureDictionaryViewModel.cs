// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using TextureTool.Models;

namespace TextureTool.ViewModels
{
    public class TextureDictionaryViewModel : BaseViewModel
    {
        private TextureDictionaryModel textureDictionary;

        public string Name
        {
            get
            {
                return textureDictionary.Name;
            }
        }

        public int Count
        {
            get
            {
                return textureDictionary.Textures.Count;
            }
        }

        public TextureDictionaryViewModel(TextureDictionaryModel textureDictionary)
        {
            this.textureDictionary = textureDictionary;
        }

        public TextureDictionaryModel GetModel()
        {
            return textureDictionary;
        }
    }
}