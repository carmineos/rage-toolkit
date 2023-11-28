// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core.FileSystem.Abstractions;

public interface IExport
{
    void ExportItem(string exportPath);
}