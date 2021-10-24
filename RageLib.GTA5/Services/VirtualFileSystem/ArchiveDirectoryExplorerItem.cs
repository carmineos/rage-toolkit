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
        private readonly List<ExplorerItem> _children;

        public override string Name => _archiveDirectory.Name;
        public override string PhysicalPath => _parent.PhysicalPath;
        public override ExplorerItemType ItemType => ExplorerItemType.ArchiveDirectory;
        public override ContainerExplorerItem Parent => _parent;
        public override List<ExplorerItem> Children => _children;

        public ArchiveDirectoryExplorerItem(IArchiveDirectory directory, ContainerExplorerItem parent)
        {
            _archiveDirectory = directory;
            _parent = parent;

            _children = new List<ExplorerItem>();
        }

        public override void LoadChildren(bool recursive)
        {
            var files = _archiveDirectory.GetFiles();

            foreach (var file in files)
            {
                if (Path.GetExtension(file.Name) == ".rpf")
                {
                    var archive = RageArchiveWrapper7.Open(file.GetStream(), file.Name);
                    var archiveExplorerItem = new ArchiveExplorerItem(archive, this);
                    
                    if (recursive) 
                        archiveExplorerItem.LoadChildren(recursive);

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
                
                if (recursive) 
                    archiveFolderExplorerItem.LoadChildren(recursive);

                _children.Add(archiveFolderExplorerItem);
            }
        }
    }
}
