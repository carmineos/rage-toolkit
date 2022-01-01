// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.ResourceWrappers.Drawables;
using System;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Drawables
{
    public class DrawableFileWrapper_GTA5_pc : IDrawableFile
    {
        private GtaDrawable drawable;

        public IDrawable Drawable
        {
            get
            {
                return new DrawableWrapper_GTA5_pc(drawable);
            }
        }

        public void Load(Stream stream)
        {
            var resource = new ResourceFile_GTA5_pc<GtaDrawable>();
            resource.Load(stream);

            drawable = resource.ResourceData;
        }


        /// <summary>
        /// Loads the texture dictionary from a file.
        /// </summary>
        public void Load(string fileName)
        {
            var resource = new ResourceFile_GTA5_pc<GtaDrawable>();
            resource.Load(fileName);

            drawable = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new ResourceFile_GTA5_pc<GtaDrawable>();
            resource.ResourceData = drawable;
            resource.Version = 165;
            resource.Save(stream);
        }


        /// <summary>
        /// Saves the texture dictionary to a file.
        /// </summary>
        public void Save(string fileName)
        {
            var resource = new ResourceFile_GTA5_pc<GtaDrawable>();
            resource.ResourceData = drawable;
            resource.Version = 165;
            resource.Save(fileName);
        }
    }
}
