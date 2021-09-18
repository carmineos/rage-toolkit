/*
    Copyright(c) 2016 Neodymium

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

using RageLib.GTA5.PSO;
using RageLib.GTA5.RBF;
using RageLib.GTA5.Services;
using RageLib.Resources.GTA5;
using RageLib.Services;
using System;
using System.IO;
using System.Reflection;

namespace MetaTool
{
    public class Program
    {
        private static JenkinsDictionary joaatDictionary;
        private static MetaConverter converter;
        private static bool loaded;

        public static void Main(string[] args)
        {
            if (args is null || args.Length < 1)
                return;

            joaatDictionary = new JenkinsDictionary();

            converter = new MetaConverter(joaatDictionary);

            foreach (var path in args)
            {
                Convert(path);
                GC.Collect();
            }
        }

        private static void BuildDictionary()
        {
            if (loaded)
                return;

            //AddHashForStrings(joaatDictionary, "MetaTool.Lists.FileNames.txt");
            AddHashForStrings(joaatDictionary, "MetaTool.Lists.MetaNames.txt");
            //AddUserDictionary(joaatDictionary, "UserDictionary.txt");

            AddHashForStrings(joaatDictionary, "MetaTool.Lists.PsoTypeNames.txt");
            AddHashForStrings(joaatDictionary, "MetaTool.Lists.PsoFieldNames.txt");
            AddHashForStrings(joaatDictionary, "MetaTool.Lists.PsoEnumValues.txt");
            AddHashForStrings(joaatDictionary, "MetaTool.Lists.PsoCommon.txt");
            AddHashForStrings(joaatDictionary, "MetaTool.Lists.FileNames.txt");
            AddHashForStrings(joaatDictionary, "MetaTool.Lists.PsoCollisions.txt");
            AddUserDictionary(joaatDictionary, "UserDictionary.txt");

            loaded = true;
        }

        public static void Convert(string filePath)
        {
            if (filePath.EndsWith(".ymap.xml") ||
             filePath.EndsWith(".ytyp.xml") ||
             filePath.EndsWith(".ymt.xml"))
            {
                converter.ConvertXmlToResource(filePath);
            }
            else if (filePath.EndsWith(".ymap") ||
                   filePath.EndsWith(".ytyp") ||
                   filePath.EndsWith(".ymt"))
            {
                if (ResourceFile_GTA5_pc.IsResourceFile(filePath))
                {
                    BuildDictionary();
                    converter.ConvertResourceToXml(filePath);
                }
                else if (PsoFile.IsPSO(filePath))
                {
                    BuildDictionary();
                    converter.ConvertPsoToXml(filePath);
                }
                else if (RbfFile.IsRBF(filePath))
                {
                    converter.ConvertRbfToXml(filePath);
                }
            }
        }

        public static void AddHashForStrings(JenkinsDictionary joaatDictionary, string resourceFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceFileName))
                joaatDictionary.AddFromFile(resourceStream);
        }

        public static void AddUserDictionary(JenkinsDictionary joaatDictionary, string resourceFileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), resourceFileName);
            if (!File.Exists(path))
                return;

            joaatDictionary.AddFromFile(resourceFileName);
        }
    }
}
