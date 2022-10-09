// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace ArchiveTool.Helpers
{
    public static class SizeHelpers
    {
        public static string GetDecimalSize(long size)
        {
            return size switch
            {
                < 1_000 => $"{size:.###} B",
                < 1_000_000 => $"{size / 1_000m:.###} KB",
                < 1_000_000_000 => $"{size / 1_000_000m:.###} MB",
                _ => $"{size / 1_000_000_000m:.###} GB",
            };
        }

        public static string GetBinarySize(long size)
        {
            return size switch
            {  
                < 1024 => $"{size:.###} B",
                < 1048576 => $"{size / 1024m:.###} KiB",
                < 1073741824 => $"{size / 1048576m:.###} MiB",
                _ => $"{size / 1073741824m:.###} GiB",
            };
        }

        public static string GetExplorerSize(long size)
        {
            // Should be KiB but we emulate Windows Explorer
            // https://devblogs.microsoft.com/oldnewthing/20090611-00/?p=17933

            var q = size >> 10;
            var r = size % 1024 > 0 ? 1 : 0;
            return $"{q + r:#,#} KB";
        }
    }
}
