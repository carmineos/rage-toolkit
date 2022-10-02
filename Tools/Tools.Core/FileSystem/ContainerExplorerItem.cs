// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Tools.Core.FileSystem.Abstractions;

namespace Tools.Core.FileSystem;

public abstract class ContainerExplorerItem : ExplorerItem, IImportFile, IImportDirectory
{
    public abstract List<ExplorerItem> Children { get; }

    public abstract void LoadChildren(bool recursive);

    public abstract void ImportFile(string filePath);
    public abstract void ImportDirectory(string directoryPath);
}
