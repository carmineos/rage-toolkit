using RageLib.Archives;
using RageLib.GTA5.ArchiveWrappers;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public class ArchiveExplorerItem : ContainerExplorerItem
    {
        private readonly IArchive _archive;
        private readonly ContainerExplorerItem _parent;
        private readonly ICollection<ExplorerItem> _children;

        public override string Name => _archive.Name;
        public override string PhysicalPath => 
            (_parent.ItemType == ExplorerItemType.Root || _parent.ItemType == ExplorerItemType.Directory)
            ? Path.Combine(_parent.PhysicalPath, Name) : _parent.PhysicalPath;
        public override string VirtualPath => Path.Combine(_parent.VirtualPath, Name);
        public override ExplorerItemType ItemType => ExplorerItemType.Archive;
        public override ContainerExplorerItem Parent => _parent;
        public override ICollection<ExplorerItem> Children => _children;

        public ArchiveExplorerItem(RageArchiveWrapper7 archive, ContainerExplorerItem parent)
        {
            _archive = archive;
            _parent = parent;

            _children = new List<ExplorerItem>();
        }

        public override void LoadChildren()
        {
            var files = _archive.Root.GetFiles();

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

            var directories = _archive.Root.GetDirectories();

            foreach (var directory in directories)
            {
                var archiveFolderExplorerItem = new ArchiveDirectoryExplorerItem(directory, this);
                archiveFolderExplorerItem.LoadChildren();
                _children.Add(archiveFolderExplorerItem);
            }
        }
    }
}
