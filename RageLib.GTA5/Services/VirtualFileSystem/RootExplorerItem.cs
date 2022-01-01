// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public class RootExplorerItem : DirectoryExplorerItem
    {
        public override ExplorerItemType ItemType => ExplorerItemType.Root;

        public RootExplorerItem(DirectoryInfo directory) : base(directory, null)
        {
        }

        public RootExplorerItem(string directoryPath) : base(new DirectoryInfo(directoryPath), null)
        {
        }
    }
}
