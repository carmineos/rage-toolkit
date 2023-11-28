// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.Archives
{
    /// <summary>
    /// Represents an archive.
    /// </summary>
    public interface IArchive : IDisposable
    {
        /// <summary>
        /// Gets or sets the name of the archive.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the length of the archive
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Gets the root directory of the archive.
        /// </summary>
        IArchiveDirectory Root { get; }

        /// <summary>
        /// Clears all buffers for this archive and causes any buffered data to be 
        /// written to the underlying device.
        /// </summary>
        void Flush();
    }
}