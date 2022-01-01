// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

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