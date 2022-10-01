// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;

namespace Tools.Core.FileSystem;

public class ArchiveExplorerItem : ContainerExplorerItem
{
    private readonly IArchive _archive;
    private readonly ContainerExplorerItem _parent;
    private readonly List<ExplorerItem> _children;

    public override string Name => _archive.Name;
    public override string PhysicalPath =>
        _parent.ItemType == ExplorerItemType.Root || _parent.ItemType == ExplorerItemType.Directory
        ? Path.Combine(_parent.PhysicalPath, Name) : _parent.PhysicalPath;
    public override ExplorerItemType ItemType => ExplorerItemType.Archive;
    public override ContainerExplorerItem Parent => _parent;
    public override List<ExplorerItem> Children => _children;

    public ArchiveExplorerItem(IArchive archive, ContainerExplorerItem parent)
    {
        _archive = archive;
        _parent = parent;

        _children = new List<ExplorerItem>();
    }

    public override void LoadChildren(bool recursive)
    {
        var files = _archive.Root.GetFiles();

        foreach (var file in files)
        {
            if (Path.GetExtension(file.Name) == ".rpf")
            {
                var archive = ArchiveHelpers.Open(file.GetStream(), file.Name);
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

        var directories = _archive.Root.GetDirectories();

        foreach (var directory in directories)
        {
            var archiveFolderExplorerItem = new ArchiveDirectoryExplorerItem(directory, this);

            if (recursive)
                archiveFolderExplorerItem.LoadChildren(recursive);

            _children.Add(archiveFolderExplorerItem);
        }
    }
}
