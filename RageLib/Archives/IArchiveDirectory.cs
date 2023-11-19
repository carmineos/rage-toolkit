// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Archives
{
    /// <summary>
    /// Represents a directory in an archive.
    /// </summary>
    public interface IArchiveDirectory
    {
        /// <summary>
        /// Gets or sets the name of the directory.
        /// </summary>
        string Name { get; set; }

        /////////////////////////////////////////////////////////////////////////////
        // directory management
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a directory list from the current directory. 
        /// </summary>
        IEnumerable<IArchiveDirectory> GetDirectories();

        /// <summary>
        /// Returns a directory from the current directory. 
        /// </summary>
        IArchiveDirectory? GetDirectory(string name);

        /// <summary>
        /// Creates a new directory inside this directory.
        /// </summary>
        IArchiveDirectory CreateDirectory();

        /// <summary>
        /// Deletes an existing directory inside this directory.
        /// </summary>
        void DeleteDirectory(IArchiveDirectory directory);

        /////////////////////////////////////////////////////////////////////////////
        // file management
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a file list from the current directory. 
        /// </summary>
        IEnumerable<IArchiveFile> GetFiles();

        /// <summary>
        /// Returns a file from the current directory. 
        /// </summary>
        IArchiveFile? GetFile(string name);

        /// <summary>
        /// Creates a new binary file inside this directory.
        /// </summary>
        IArchiveBinaryFile CreateBinaryFile();

        /// <summary>
        /// Creates a new resource file inside this directory.
        /// </summary>
        IArchiveResourceFile CreateResourceFile();

        /// <summary>
        /// Deletes an existing file inside this directory.
        /// </summary>
        void DeleteFile(IArchiveFile file);
    }
}