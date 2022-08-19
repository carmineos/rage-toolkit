// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Fragments;
using RageLib.ResourceWrappers.Fragments;
using System;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Fragments
{
    /// <summary>
    /// Represents a wrapper for a GTA5 PC fragment file.
    /// </summary>
    public class FragmentFileWrapper_GTA5_pc : IFragmentFile
    {
        private FragType fragType;

        /// <summary>
        /// Gets the fragtype.
        /// </summary>
        public IFragType FragType
        {
            get { return new FragTypeWrapper_GTA5_pc(fragType); }
        }

        public FragmentFileWrapper_GTA5_pc()
        {
            fragType = new FragType();
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<FragType>();
            resource.Load(fileName);

            fragType = resource.ResourceData;
        }

        public void Save(string fileName)
        {
            var w = new FragTypeWrapper_GTA5_pc(fragType);
            w.UpdateClass();

            var resource = new Resource7<FragType>();
            resource.ResourceData = fragType;
            resource.Version = 162;
            resource.Save(fileName);
        }

        public void Load(Stream stream)
        {
            var resource = new Resource7<FragType>();
            resource.Load(stream);

            if (resource.Version != 162)
                throw new Exception("version error");

            fragType = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var w = new FragTypeWrapper_GTA5_pc(fragType);
            w.UpdateClass();

            var resource = new Resource7<FragType>();
            resource.ResourceData = fragType;
            resource.Version = 162;
            resource.Save(stream);
        }
    }
}
