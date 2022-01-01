// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.Cryptography;
using System.IO;

namespace ExtractKeysFromDump
{
    //
    // Example command for running the tool:
    // ExtractKeysFromDump.exe -executableFile "C:\GTA 5\GTA5-dump.exe" -keyPath "C:\GTA 5\Keys"
    //
    public class Program
    {
        private readonly string[] arguments;

        public Program(string[] arguments)
        {
            this.arguments = arguments;
        }

        public void Run()
        {
            string executableFile = GetArgument("-executableFile");
            string keyPath = GetArgument("-keyPath");
            ExtractKeysIntoDirectory(executableFile, keyPath);
        }

        private string GetArgument(string argumentName)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i].Equals(argumentName))
                {
                    return arguments[i + 1];
                }
            }
            return null;
        }

        private void ExtractKeysIntoDirectory(string executableFile, string keyPath)
        {
            GTA5Constants.Generate(File.ReadAllBytes(executableFile));
            GTA5Constants.SaveToPath(keyPath);
        }

        static void Main(string[] args)
        {
            new Program(args).Run();
        }
    }
}
