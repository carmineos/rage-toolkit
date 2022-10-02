// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core.FileSystem;

public class FileExplorerItem : ExplorerItem
{
    private readonly FileInfo _file;
    private readonly DirectoryExplorerItem _parent;

    public override string Name => _file.Name;
    public override string? PhysicalPath => _file.FullName;
    public override ExplorerItemType ItemType => ExplorerItemType.File;
    public override ContainerExplorerItem Parent => _parent;

    public FileExplorerItem(FileInfo file, DirectoryExplorerItem parent)
    {
        _file = file;
        _parent = parent;
    }

    public override void ExportItem(string exportPath)
    {
        _file.CopyTo(exportPath);
    }
}
