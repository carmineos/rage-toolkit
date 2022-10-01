// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core.FileSystem;

public abstract class ExplorerItem
{
    public abstract string Name { get; }
    public abstract string PhysicalPath { get; }
    public abstract ExplorerItemType ItemType { get; }
    public abstract ExplorerItem Parent { get; }

    public virtual string GetRelativePath()
    {
        if (Parent is null)
            return Name;

        return Path.Combine(Parent.GetRelativePath(), Name);
    }
}
