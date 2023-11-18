// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Archives;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Utilities;

namespace Tools.Core.FileSystem;

public class ArchiveExplorerItem : ContainerExplorerItem
{
    private readonly IArchive _archive;
    private readonly ContainerExplorerItem _parent;
    private readonly List<ExplorerItem> _children;

    public override string Name => _archive.Name;
    public override string? PhysicalPath => _parent is null ? string.Empty :
        _parent.ItemType == ExplorerItemType.Root || _parent.ItemType == ExplorerItemType.Directory
        ? Path.Combine(_parent.PhysicalPath!, Name) : null;
    public override ExplorerItemType ItemType => ExplorerItemType.Archive;
    public override ContainerExplorerItem? Parent => _parent;
    public override List<ExplorerItem> Children => _children;
    public override long? Size => _archive.Size;

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

    public override void ExportItem(string exportPath)
    {
        // If it's a physical archive
        if (!string.IsNullOrEmpty(PhysicalPath))
        {
            var fileInfo = new FileInfo(PhysicalPath);
            fileInfo.CopyTo(exportPath);
            return;
        }

        // Search the IArchiveFile (as binary file not as archive)
        IArchiveFile? selfBinaryFile = null;
        
        if (Parent is ArchiveExplorerItem parentArchive)
        {
            selfBinaryFile = parentArchive.GetArchive().Root.GetFile(Name);
        }
        else if(Parent is ArchiveDirectoryExplorerItem parentDirectory)
        {
            selfBinaryFile = parentDirectory.GetArchiveDirectory().GetFile(Name);
        }

        // Export it
        selfBinaryFile?.Export(exportPath);
    }

    internal IArchive GetArchive() => _archive;

    public override void ImportFile(string filePath)
    {
        // TODO: Reset Archive Encryption
        ((RageArchiveWrapper7)_archive).Encryption = RageLib.GTA5.Archives.RageArchiveEncryption7.None;
        ArchiveUtilities.ImportFile(_archive.Root, filePath);
        _archive.Flush();
    }

    public override void ImportDirectory(string directoryPath)
    {
        // TODO: Reset Archive Encryption
        ((RageArchiveWrapper7)_archive).Encryption = RageLib.GTA5.Archives.RageArchiveEncryption7.None;
        ArchiveUtilities.ImportDirectory(_archive.Root, directoryPath, false, RageLib.GTA5.Archives.RageArchiveEncryption7.None);      
        _archive.Flush();
    }

    public void Extract(string directoryPath, bool recursive)
    {
        // TODO: Refactor ArchiveUtilities, create game agnostic service
        ArchiveUtilities.UnpackArchive(_archive, directoryPath, recursive);
    }
}
