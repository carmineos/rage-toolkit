// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Tools.Core.FileSystem.Abstractions;

namespace Tools.Core.FileSystem;

public abstract class ExplorerItem : IExport
{
    public abstract string Name { get; }
    public abstract string? PhysicalPath { get; }
    public abstract ExplorerItemType ItemType { get; }
    public abstract ContainerExplorerItem? Parent { get; }

    public abstract void ExportItem(string exportPath);

    public virtual string GetRelativePath()
    {
        if (Parent is null)
            return Name;

        return Path.Combine(Parent.GetRelativePath(), Name);
    }
}
