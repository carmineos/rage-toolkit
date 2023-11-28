// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Utilities;

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
    public override long? Size => null;

    public ArchiveDirectoryExplorerItem(IArchiveDirectory directory, ContainerExplorerItem parent)
    {
        _archiveDirectory = directory;
        _parent = parent;

        _children = new List<ExplorerItem>();
    }

    public override void LoadChildren(bool recursive)
    {
        foreach (var file in _archiveDirectory.GetFiles())
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

        foreach (var directory in _archiveDirectory.GetDirectories())
        {
            var archiveFolderExplorerItem = new ArchiveDirectoryExplorerItem(directory, this);

            if (recursive)
                archiveFolderExplorerItem.LoadChildren(recursive);

            _children.Add(archiveFolderExplorerItem);
        }
    }

    internal IArchiveDirectory GetArchiveDirectory() => _archiveDirectory;

    public override void ExportItem(string exportPath)
    {
        // TODO: Refactor RageLib.GTA5.Utilities.ArchiveUtilities.UnpackDirectory
        _ = Directory.CreateDirectory(exportPath);
        ArchiveUtilities.UnpackDirectory(_archiveDirectory, exportPath, false);
    }

    public override void ImportFile(string filePath)
    {
        // TODO: Reset Archive Encryption
        //((RageArchiveWrapper7)_archive).Encryption = RageLib.GTA5.Archives.RageArchiveEncryption7.None;
        ArchiveUtilities.ImportFile(_archiveDirectory, filePath);
        // TODO: Flush Archive
        //_archive.Flush();
    }

    public override void ImportDirectory(string directoryPath)
    {
        // TODO: Reset Archive Encryption
        //((RageArchiveWrapper7)_archive).Encryption = RageLib.GTA5.Archives.RageArchiveEncryption7.None;
        ArchiveUtilities.ImportDirectory(_archiveDirectory, directoryPath, false, RageLib.GTA5.Archives.RageArchiveEncryption7.None);
        // TODO: Flush Archive
        // _archive.Flush();
    }
}
