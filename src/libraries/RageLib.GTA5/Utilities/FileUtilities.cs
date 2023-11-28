// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Utilities
{
    public class FileUtilities
    {
        public static HashSet<string> GetAllFileNamesWithoutExtension(string gameDirectoryName)
        {
            var fileNamesWithoutExtension = new HashSet<string>();
            foreach (var fileName in GetAllFileNames(gameDirectoryName))
            {
                fileNamesWithoutExtension.Add(Path.GetFileNameWithoutExtension(fileName));
            }
            return fileNamesWithoutExtension;
        }

        public static HashSet<string> GetAllFileNames(string gameDirectoryName)
        {
            var fileNames = new HashSet<string>();
            foreach (var fileName in Directory.GetFiles(gameDirectoryName, "*.*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(fileName);
                fileNames.Add(fileInfo.Name);
            }
            ArchiveUtilities.ForEachFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                fileNames.Add(file.Name);
            });
            return fileNames;
        }
    }
}
