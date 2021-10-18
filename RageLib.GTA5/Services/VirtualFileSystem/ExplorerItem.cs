using System.Collections.Generic;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public enum ExplorerItemType
    {
        Root,
        File,
        Directory,
        Archive,
        ArchiveFile,
        ArchiveDirectory,
    }

    public abstract class ExplorerItem
    {
        public abstract string Name { get; }
        public abstract string PhysicalPath { get; }
        public abstract string VirtualPath { get; }
        public abstract ExplorerItemType ItemType { get; }
        public abstract ExplorerItem Parent { get; }
    }

    public abstract class ContainerExplorerItem : ExplorerItem
    {
        public abstract ICollection<ExplorerItem> Children { get; }
    }
}
