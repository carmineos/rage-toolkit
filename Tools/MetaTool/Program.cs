// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

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
