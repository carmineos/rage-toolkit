using RageLib.GTA5.ArchiveWrappers;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public class RootExplorerItem : DirectoryExplorerItem
    {
        public override string VirtualPath => Path.DirectorySeparatorChar.ToString();
        public override ExplorerItemType ItemType => ExplorerItemType.Root;

        public RootExplorerItem(DirectoryInfo directory) : base (directory, null)
        {
        }

        public RootExplorerItem(string directoryPath) : base(new DirectoryInfo(directoryPath), null)
        {
        }
    }
}
