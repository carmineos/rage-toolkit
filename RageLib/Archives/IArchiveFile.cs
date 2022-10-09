// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.IO;

namespace RageLib.Archives
{
    /// <summary>
    /// Represents a file in an archive.
    /// </summary>
    public interface IArchiveFile
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        long Size { get; }

        /////////////////////////////////////////////////////////////////////////////
        // import and export
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Imports a file.
        /// </summary>
        void Import(string fileName);

        /// <summary>
        /// Imports a file.
        /// </summary>
        void Import(Stream stream);

        /// <summary>
        /// Exports a file.
        /// </summary>
        void Export(string fileName);

        /// <summary>
        /// Exports a file.
        /// </summary>
        void Export(Stream stream);

        /////////////////////////////////////////////////////////////////////////////
        // file access
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets the stream that respresents the possibly compressed and encrypted content of the file.
        /// </summary>
        Stream GetStream();
    }

    /// <summary>
    /// Represents a binary file in an archive.
    /// </summary>
    public interface IArchiveBinaryFile : IArchiveFile
    {
        /////////////////////////////////////////////////////////////////////////////
        // encryption...
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets or sets a value indicating whether the file is encrypted.
        /// </summary>
        bool IsEncrypted { get; set; }

        /////////////////////////////////////////////////////////////////////////////
        // compression...
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Gets or sets a value indicating whether the file is compressed.
        /// </summary>
        bool IsCompressed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the uncompressed size of the file. This 
        /// property can only be set if the file is compressed.
        /// </summary>
        long UncompressedSize { get; set; }

        /// <summary>
        /// Gets the compressed size of the file.
        /// </summary>
        long CompressedSize { get; }

        /////////////////////////////////////////////////////////////////////////////
        // compression 
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Compresses a file and imports it.
        /// </summary>
        void ImportCompressed(string fileName);

        /// <summary>
        /// Compresses a file and imports it.
        /// </summary>
        void ImportCompressed(Stream stream);

        /// <summary>
        /// Decompresses the file and exports it.
        /// </summary>
        void ExportUncompressed(string fileName);

        /// <summary>
        /// Decompresses the file and exports it.
        /// </summary>
        void ExportUncompressed(Stream stream);
    }

    /// <summary>
    /// Represents a resource file in an archive.
    /// </summary>
    public interface IArchiveResourceFile : IArchiveFile
    { }
}