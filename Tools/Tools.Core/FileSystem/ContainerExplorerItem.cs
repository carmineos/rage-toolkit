// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core.FileSystem;

public abstract class ContainerExplorerItem : ExplorerItem
{
    public abstract List<ExplorerItem> Children { get; }

    public abstract void LoadChildren(bool recursive);
}
