// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.IO;

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
        public abstract ExplorerItemType ItemType { get; }
        public abstract ExplorerItem Parent { get; }

        public static string GetRelativePath(ExplorerItem item)
        {
            //if (item.ItemType == ExplorerItemType.Root)
            //    return Path.DirectorySeparatorChar.ToString();

            if (item.Parent != null)
                return Path.Combine(GetRelativePath(item.Parent), item.Name);

            return item.Name;
        }
    }

    public abstract class ContainerExplorerItem : ExplorerItem
    {
        public abstract List<ExplorerItem> Children { get; }

        public abstract void LoadChildren(bool recursive);
    }
}
