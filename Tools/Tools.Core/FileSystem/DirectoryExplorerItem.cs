// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core.FileSystem;

public class DirectoryExplorerItem : ContainerExplorerItem
{
    private readonly DirectoryInfo _directory;
    private readonly DirectoryExplorerItem _parent;
    private readonly List<ExplorerItem> _children;

    public override string Name => _directory.Name;
    public override string PhysicalPath => _directory.FullName;
    public override ExplorerItemType ItemType => ExplorerItemType.Directory;
    public override DirectoryExplorerItem Parent => _parent;
    public override List<ExplorerItem> Children => _children;

    public DirectoryExplorerItem(DirectoryInfo directory, DirectoryExplorerItem parent)
    {
        _directory = directory;
        _parent = parent;

        _children = new List<ExplorerItem>();
    }

    public override void LoadChildren(bool recursive)
    {
        var files = _directory.EnumerateFiles();

        foreach (var file in files)
        {
            if (file.Extension == ".rpf")
            {
                var archive = ArchiveHelpers.Open(new FileStream(file.FullName, FileMode.Open, FileAccess.Read), file.Name);
                var archiveExplorerItem = new ArchiveExplorerItem(archive, this);

                if (recursive)
                    archiveExplorerItem.LoadChildren(recursive);

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

            if (recursive)
                folderExplorerItem.LoadChildren(recursive);

            _children.Add(folderExplorerItem);
        }
    }
}
