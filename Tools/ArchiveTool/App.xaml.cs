/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

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
