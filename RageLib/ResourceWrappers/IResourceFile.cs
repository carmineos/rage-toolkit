// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.IO;

namespace RageLib.ResourceWrappers
{
    /// <summary>
    /// Represents a resource file.
    /// </summary>
    public interface IResourceFile
    {
        /// <summary>
        /// Loads the resource from a file.
        /// </summary>
        void Load(string fileName);

        /// <summary>
        /// Loads the resource from a stream.
        /// </summary>
        void Load(Stream stream);

        /// <summary>
        /// Saves the resource to a file.
        /// </summary>
        void Save(string fileName);

        /// <summary>
        /// Saves the resource to a stream.
        /// </summary>
        void Save(Stream stream);
    }
}
