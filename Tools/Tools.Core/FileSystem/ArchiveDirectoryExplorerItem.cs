// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;

namespace Tools.Core.FileSystem;

public class ArchiveDirectoryExplorerItem : ContainerExplorerItem
{
    private readonly IArchiveDirectory _archiveDirectory;
    private readonly ContainerExplorerItem _parent;
    private readonly List<ExplorerItem> _children;

    public override string Name => _archiveDirectory.Name;
    public override string? PhysicalPath => null;
    public override ExplorerItemType ItemType => ExplorerItemType.ArchiveDirectory;
    public override ContainerExplorerItem? Parent => _parent;
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

        var directories = _archiveDirectory.GetDirectories();

        foreach (var directory in directories)
        {
            var archiveFolderExplorerItem = new ArchiveDirectoryExplorerItem(directory, this);

            if (recursive)
                archiveFolderExplorerItem.LoadChildren(recursive);

            _children.Add(archiveFolderExplorerItem);
        }
    }

    public override void ExportItem(string exportPath)
    {
        // TODO: Refactor RageLib.GTA5.Utilities.ArchiveUtilities.UnpackDirectory
    }
}
