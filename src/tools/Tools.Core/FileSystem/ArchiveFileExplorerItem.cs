// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;

namespace Tools.Core.FileSystem;

public class ArchiveFileExplorerItem : ExplorerItem
{
    private readonly IArchiveFile _archiveFile;
    private readonly ContainerExplorerItem _parent;

    public override string Name => _archiveFile.Name;
    public override string? PhysicalPath => null;
    public override ExplorerItemType ItemType => ExplorerItemType.ArchiveFile;
    public override ContainerExplorerItem? Parent => _parent;
    public override long? Size => _archiveFile.Size;

    public ArchiveFileExplorerItem(IArchiveFile file, ContainerExplorerItem parent)
    {
        _archiveFile = file;
        _parent = parent;
    }

    public override void ExportItem(string exportPath)
    {
        if(_archiveFile is IArchiveBinaryFile archiveBinaryFile)
            archiveBinaryFile.ExportUncompressed(exportPath);
        else
            _archiveFile.Export(exportPath);
    }
}
