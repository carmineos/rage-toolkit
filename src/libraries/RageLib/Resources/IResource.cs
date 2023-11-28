// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.IO;

namespace RageLib.Resources
{
    public interface IResource
    {
        byte[] VirtualData { get; set; }
        byte[] PhysicalData { get; set; }
        int Version { get; set; }

        void Load(Stream stream);
        void Load(string fileName);
        void Save(Stream stream);
        void Save(string fileName);
    }

    public interface IResource<T> : IResource where T : IResourceBlock, new()
    {
        T ResourceData { get; set; }
    }
}
