// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Cryptography;
using System.IO;

namespace ArchiveTool.Models
{
    public class MainModel
    {
        private IArchive archive;

        public IArchive Archive => archive;

        public bool HasKeys => GTA5Constants.PC_AES_KEY != null;

        /// <summary>
        /// Creates an archive.
        /// </summary>
        public void New(string fileName)
        {
            // close first...
            Close();

            this.archive = RageArchiveWrapper7.Create(fileName);
        }

        /// <summary>
        /// Opens an archive.
        /// </summary>
        public void Load(string fileName)
        {
            // close first...
            Close();

            this.archive = RageArchiveWrapper7.Open(fileName);
        }

        /// <summary>
        /// Closes an archive.
        /// </summary>
        public void Close()
        {
            if (archive != null)
            {
                //archive.Flush();
                archive.Dispose();
            }

            archive = null;
        }

        /// <summary>
        /// Imports a file.
        /// </summary>
        public void Import(IArchiveDirectory directory, string fileName)
        {
            var fi = new FileInfo(fileName);
            
            bool isResource = RageLib.Resources.GTA5.ResourceFile_GTA5_pc.IsResourceFile(fileName);

            // delete existing file
            var existingFile = directory.GetFile(fi.Name);
            if (existingFile != null)
                directory.DeleteFile(existingFile);

            if (isResource)
            {
                var newF = directory.CreateResourceFile();
                newF.Name = fi.Name;
                newF.Import(fileName);
            }
            else
            {
                var newF = directory.CreateBinaryFile();
                newF.Name = fi.Name;
                newF.Import(fileName);
            }
        }

        /// <summary>
        /// Exports a file.
        /// </summary>
        public void Export(IArchiveFile file, string fileName)
        {
            if (file is IArchiveBinaryFile binFile)
            {
                (binFile as RageArchiveBinaryFileWrapper7).ExportUncompressed(fileName);
            }
            else
            {
                file.Export(fileName);
            }
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        public void Delete()
        { }

        public void FindConstants(string exeFileName)
        {
            var exeData = File.ReadAllBytes(exeFileName);

            GTA5Constants.Generate(exeData);
            GTA5Constants.SaveToPath(".");
        }
    }
}