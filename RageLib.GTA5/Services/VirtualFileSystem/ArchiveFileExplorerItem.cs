using RageLib.Archives;
using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public class ArchiveFileExplorerItem : ExplorerItem
    {
        private readonly IArchiveFile _archiveFile;
        private readonly ContainerExplorerItem _parent;

        public override string Name => _archiveFile.Name;
        public override string PhysicalPath => _parent.PhysicalPath;
        public override string VirtualPath => Path.Combine(_parent.VirtualPath, Name);
        public override ExplorerItemType ItemType => ExplorerItemType.ArchiveFile;
        public override ContainerExplorerItem Parent => _parent;

        public ArchiveFileExplorerItem(IArchiveFile file, ContainerExplorerItem parent)
        {
            _archiveFile = file;
            _parent = parent;
        }
    }
}
