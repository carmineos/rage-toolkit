using RageLib.GTA5.ArchiveWrappers;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Services.VirtualFileSystem
{
    public class DirectoryExplorerItem : ContainerExplorerItem
    {
        private readonly DirectoryInfo _directory;
        private readonly DirectoryExplorerItem _parent;
        private readonly ICollection<ExplorerItem> _children;

        public override string Name => _directory.Name;
        public override string PhysicalPath => _directory.FullName;
        public override string VirtualPath => Path.Combine(_parent.VirtualPath, Name);
        public override ExplorerItemType ItemType => ExplorerItemType.Directory;
        public override DirectoryExplorerItem Parent => _parent;
        public override ICollection<ExplorerItem> Children => _children;

        public DirectoryExplorerItem(DirectoryInfo directory, DirectoryExplorerItem parent)
        {
            _directory = directory;
            _parent = parent;

            _children = new List<ExplorerItem>();
        }

        public override void LoadChildren()
        {
            var files = _directory.EnumerateFiles();

            foreach (var file in files)
            {
                if (file.Extension == ".rpf")
                {
                    var archive = RageArchiveWrapper7.Open(new FileStream(file.FullName, FileMode.Open, FileAccess.Read), file.Name);
                    var archiveExplorerItem = new ArchiveExplorerItem(archive, this);
                    archiveExplorerItem.LoadChildren();
                    _children.Add(archiveExplorerItem);
                }
                else
                {
                    _children.Add(new FileExplorerItem(file, this));
                }
            }

            var directories = _directory.EnumerateDirectories();

            foreach (var directory in directories)
            {
                var folderExplorerItem = new DirectoryExplorerItem(directory, this);
                folderExplorerItem.LoadChildren();
                _children.Add(folderExplorerItem);
            }
        }
    }
}
