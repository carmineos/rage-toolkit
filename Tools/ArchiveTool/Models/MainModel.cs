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