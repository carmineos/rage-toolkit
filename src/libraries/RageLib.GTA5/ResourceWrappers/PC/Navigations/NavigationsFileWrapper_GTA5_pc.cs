﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Navigations;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Navigations
{
    public class NavigationsFileWrapper_GTA5_pc
    {
        private Navigation navigation;

        public void Load(Stream stream)
        {
            var resource = new Resource7<Navigation>();
            resource.Load(stream);

            navigation = resource.ResourceData;
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<Navigation>();
            resource.Load(fileName);

            navigation = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new Resource7<Navigation>();
            resource.ResourceData = navigation;
            resource.Version = 2;
            resource.Save(stream);
        }

        public void Save(string fileName)
        {
            var resource = new Resource7<Navigation>();
            resource.ResourceData = navigation;
            resource.Version = 2;
            resource.Save(fileName);
        }
    }
}
