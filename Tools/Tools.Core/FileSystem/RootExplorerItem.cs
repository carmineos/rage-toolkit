﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core.FileSystem;

public class RootExplorerItem : DirectoryExplorerItem
{
    public override ExplorerItemType ItemType => ExplorerItemType.Root;

    public RootExplorerItem(DirectoryInfo directory) : base(directory, null!)
    {
    }

    public RootExplorerItem(string directoryPath) : base(new DirectoryInfo(directoryPath), null!)
    {
    }
}
