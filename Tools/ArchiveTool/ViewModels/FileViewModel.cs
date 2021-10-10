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

using CommunityToolkit.Mvvm.ComponentModel;
using RageLib.Archives;

namespace ArchiveTool.ViewModels
{
    /// <summary>
    /// Represents a view-model of a file.
    /// </summary>
    public interface IFileViewModel
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Gets a value indicating whether the file is compressed.
        /// </summary>
        bool IsCompressed { get; }

        /// <summary>
        /// Gets a value indicating whether the file is encrypted.
        /// </summary>
        bool IsEncrypted { get; }

        /// <summary>
        /// Gets a value indicating whether the file is a resource file.
        /// </summary>
        bool IsResource { get; }
    }

    public abstract class FileViewModel : ObservableObject, IFileViewModel
    {
        public abstract string Name { get; }
        public abstract long Size { get; }
        public abstract bool IsCompressed { get; }
        public abstract bool IsEncrypted { get; }
        public abstract bool IsResource { get; }

        public abstract IArchiveFile GetFile();
    }

    public class BinaryFileViewModel : FileViewModel
    {
        private IArchiveBinaryFile file;

        public override string Name => file.Name;

        public override long Size => file.Size;

        public override bool IsCompressed => file.IsCompressed;

        public override bool IsEncrypted => file.IsEncrypted;

        public override bool IsResource => false;

        public BinaryFileViewModel(IArchiveFile file)
        {
            this.file = (IArchiveBinaryFile)file;
        }

        public override IArchiveFile GetFile()
        {
            return file;
        }
    }

    public class ResourceFileViewModel : FileViewModel
    {
        private IArchiveFile file;

        public override string Name => file.Name;

        public override long Size => file.Size;

        public override bool IsCompressed => true;

        public override bool IsEncrypted => false;

        public override bool IsResource => true;

        public ResourceFileViewModel(IArchiveFile file)
        {
            this.file = file;
        }

        public override IArchiveFile GetFile()
        {
            return file;
        }
    }
}