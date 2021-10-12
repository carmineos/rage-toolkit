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

using RageLib.Archives;
using RageLib.GTA5.Archives;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.Resources.GTA5;
using System;
using System.IO;

namespace RageLib.GTA5.Utilities
{
    public delegate void ProcessBinaryFileDelegate(string fullFileName, IArchiveBinaryFile binaryFile, RageArchiveEncryption7 encryption);
    public delegate void ProcessResourceFileDelegate(string fullFileName, IArchiveResourceFile resourceFile, RageArchiveEncryption7 encryption);
    public delegate void ProcessFileDelegate(string fullFileName, IArchiveFile file, RageArchiveEncryption7 encryption);

    public static class ArchiveUtilities
    {
        public static void ForEachBinaryFile(string gameDirectoryName, ProcessBinaryFileDelegate processDelegate)
        {
            ForEachFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file is IArchiveBinaryFile)
                {
                    processDelegate(fullFileName, (IArchiveBinaryFile)file, encryption);
                }
            });
        }

        public static void ForEachResourceFile(string gameDirectoryName, ProcessResourceFileDelegate processDelegate)
        {
            ForEachFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file is IArchiveResourceFile)
                {
                    processDelegate(fullFileName, (IArchiveResourceFile)file, encryption);
                }
            });
        }

        public static void ForEachFile(string gameDirectoryName, ProcessFileDelegate processDelegate)
        {
            var archiveFileNames = Directory.GetFiles(gameDirectoryName, "*.rpf", SearchOption.AllDirectories);
            for (int i = 0; i < archiveFileNames.Length; i++)
            {
                var fileName = archiveFileNames[i];
                var fileInfo = new FileInfo(fileName);
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var inputArchive = RageArchiveWrapper7.Open(fileStream, fileInfo.Name);
                ForEachFile(fileName.Replace(gameDirectoryName, ""), inputArchive.Root, inputArchive.Encryption, processDelegate);
                inputArchive.Dispose();
            }
        }

        private static void ForEachFile(string fullPathName, IArchiveDirectory directory, RageArchiveEncryption7 encryption, ProcessFileDelegate processDelegate)
        {
            foreach (var file in directory.GetFiles())
            {
                processDelegate(fullPathName + "\\" + file.Name, file, encryption);
                if ((file is IArchiveBinaryFile) && file.Name.EndsWith(".rpf", StringComparison.OrdinalIgnoreCase))
                {
                    var fileStream = ((IArchiveBinaryFile)file).GetStream();
                    var inputArchive = RageArchiveWrapper7.Open(fileStream, file.Name);
                    ForEachFile(fullPathName + "\\" + file.Name, inputArchive.Root, inputArchive.Encryption, processDelegate);
                }
            }
            foreach (var subDirectory in directory.GetDirectories())
            {
                ForEachFile(fullPathName + "\\" + subDirectory.Name, subDirectory, encryption, processDelegate);
            }
        }

        public static void UnpackArchive(string fileName, string outputFolderPath, bool recursive)
        {
            var fileInfo = new FileInfo(fileName);
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            UnpackArchive(fileStream, fileInfo.Name, outputFolderPath, recursive);
        }

        public static void UnpackArchive(Stream stream, string fileName, string outputFolderPath, bool recursive)
        {
            var inputArchive = RageArchiveWrapper7.Open(stream, fileName);
            UnpackDirectory(inputArchive.Root, Path.Combine(outputFolderPath, inputArchive.Name), recursive);
            inputArchive.Dispose();
        }

        public static void UnpackArchive(RageArchiveWrapper7 archive, string outputFolderPath, bool recursive)
        {
            UnpackDirectory(archive.Root, Path.Combine(outputFolderPath, archive.Name), recursive);
        }

        public static void UnpackDirectory(IArchiveDirectory directory, string outputFolderPath, bool unpackArchives)
        {
            var directoryPath = Path.Combine(outputFolderPath, directory.Name);
            var directoryInfo = Directory.CreateDirectory(directoryPath);

            foreach (var file in directory.GetFiles())
            {
                var filePath = Path.Combine(directoryPath, file.Name);

                if (file is IArchiveBinaryFile binFile)
                {
                    // If it's an archive
                    if (binFile.Name.EndsWith(".rpf", StringComparison.OrdinalIgnoreCase) && unpackArchives)
                    {
                        UnpackArchive(binFile.GetStream(), binFile.Name, directoryPath, unpackArchives);
                    }
                    else
                    {
                        (binFile as RageArchiveBinaryFileWrapper7).ExportUncompressed(filePath);
                    }
                }
                else
                {
                    file.Export(filePath);
                }
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                UnpackDirectory(subDirectory, directoryPath, unpackArchives);
            }
        }

        public static void PackArchive(string inputFolderPath, string outputFileName, bool recursive, RageArchiveEncryption7 encryption = RageArchiveEncryption7.None)
        {
            var archive = RageArchiveWrapper7.Create(outputFileName);
            PackDirectory(archive.Root, Path.Combine(inputFolderPath, archive.Root.Name), recursive, encryption);
            archive.Encryption = encryption;
            archive.Flush();
            archive.Dispose();
        }

        public static void PackDirectory(IArchiveDirectory directory, string inputFolderPath, bool recursive, RageArchiveEncryption7 encryption = RageArchiveEncryption7.None)
        {
            var files = Directory.EnumerateFiles(inputFolderPath);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);

                IArchiveFile archiveFile;

                if (ResourceFile_GTA5_pc.IsResourceFile(file))
                {
                    archiveFile = directory.CreateResourceFile();
                    archiveFile.Name = fileName;
                    archiveFile.Import(file);
                }
                else
                {
                    archiveFile = directory.CreateBinaryFile();
                    archiveFile.Name = fileName;
                    (archiveFile as RageArchiveBinaryFileWrapper7).ImportCompressed(file);
                }
                
            }

            var directories = Directory.EnumerateDirectories(inputFolderPath);
            foreach (var subDirectory in directories)
            {
                // Workaround as Path.GetDirectoryName(subDirectory) seems bugged
                var directoryName = new DirectoryInfo(subDirectory).Name;

                if(Path.GetExtension(subDirectory) == ".rpf" && recursive)
                {
                    var tmpFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    var dir = Directory.CreateDirectory(tmpFolder);
                    var tmpPath = Path.Combine(tmpFolder, directoryName);
                    PackArchive(subDirectory, tmpPath, recursive, encryption);
                    var binaryFile = directory.CreateBinaryFile();
                    binaryFile.Name = Path.GetFileName(tmpPath);
                    binaryFile.Import(tmpPath);
                    dir.Delete(true);

                    // We should write to memory instead of filesystem when possible
                    // What happens if the archive exceeds MemoryStream max capacity ?
                    // Should we write to filesystem in that case?
                    
                    // TODO: Add API to create a RageArchiveWrapper7 from a RageArchiveBinaryFileWrapper7 and viceversa
                    //using var archiveStream = new MemoryStream();
                    //{
                    //    var archive = RageArchiveWrapper7.Create(archiveStream, directoryName);
                    //    PackDirectory(archive.Root, subDirectory, recursive, encryption);
                    //    archive.Encryption = encryption;
                    //    archive.Flush();
                    //    
                    //    var binaryFile = directory.CreateBinaryFile();
                    //    binaryFile.Name = directoryName;
                    //    binaryFile.Import(archiveStream);
                    //
                    //    archive.Dispose();
                    //}
                }
                else
                {
                    var archiveSubdirectory = directory.CreateDirectory();
                    archiveSubdirectory.Name = directoryName;
                    PackDirectory(archiveSubdirectory, subDirectory, recursive, encryption);
                }
            }
        }
    }
}
