using RageLib.Archives;
using RageLib.GTA5.ArchiveWrappers;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public class ArchiveDirectoryExplorerItem : ContainerExplorerItem
    {
        private readonly IArchiveDirectory _archiveDirectory;
        private readonly ContainerExplorerItem _parent;
        private readonly ICollection<ExplorerItem> _children;

        public override string Name => _archiveDirectory.Name;
        public override string PhysicalPath => _parent.PhysicalPath;
        public override string VirtualPath => Path.Combine(_parent.VirtualPath, Name);
        public override ExplorerItemType ItemType => ExplorerItemType.ArchiveDirectory;
        public override ContainerExplorerItem Parent => _parent;
        public override ICollection<ExplorerItem> Children => _children;

        public ArchiveDirectoryExplorerItem(IArchiveDirectory directory, ContainerExplorerItem parent)
        {
            _archiveDirectory = directory;
            _parent = parent;

            _children = new List<ExplorerItem>();
        }

        public override void LoadChildren()
        {
            var files = _archiveDirectory.GetFiles();

            foreach (var file in files)
            {
                if (Path.GetExtension(file.Name) == ".rpf")
                {
                    var archive = RageArchiveWrapper7.Open(file.GetStream(), file.Name);
                    var archiveExplorerItem = new ArchiveExplorerItem(archive, this);
                    archiveExplorerItem.LoadChildren();
                    _children.Add(archiveExplorerItem);
                }
                else
                {
                    _children.Add(new ArchiveFileExplorerItem(file, this));
                }
            }

            var directories = _archiveDirectory.GetDirectories();

            foreach (var directory in directories)
            {
                var archiveFolderExplorerItem = new ArchiveDirectoryExplorerItem(directory, this);
                archiveFolderExplorerItem.LoadChildren();
                _children.Add(archiveFolderExplorerItem);
            }
        }
    }
}
