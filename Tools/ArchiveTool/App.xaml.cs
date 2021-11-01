// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.Cryptography;
using System;
using System.IO;
using System.Windows;

namespace ArchiveTool
{
    public partial class App : Application
    {
        public App()
        {
            GTA5Constants.LoadFromPath(Path.Combine(Environment.CurrentDirectory, "keys"));
        }
    }
}
