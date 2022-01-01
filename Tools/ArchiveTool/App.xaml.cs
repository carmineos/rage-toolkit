// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.Cryptography;
using System;
using System.Windows;

namespace ArchiveTool
{
    public partial class App : Application
    {
        public App()
        {
            //if (File.Exists("gta5_const.dat"))
            //{
            //    var fs = new FileStream("gta5_const.dat", FileMode.Open);
            //    var bf = new BinaryFormatter();

            //    GTA5Constants.PC_AES_KEY = (byte[])bf.Deserialize(fs);
            //    GTA5Constants.PC_NG_KEYS = (byte[][])bf.Deserialize(fs);
            //    GTA5Constants.PC_NG_DECRYPT_TABLES = (byte[][])bf.Deserialize(fs);
            //    GTA5Constants.PC_LUT = (byte[])bf.Deserialize(fs);

            //    fs.Close();
            //}
            GTA5Constants.LoadFromPath(Environment.CurrentDirectory);
        }
    }
}
