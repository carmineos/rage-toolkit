// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTool.Helpers
{
    public static class StringHelper
    {
        public static string SizeString(ulong size)
        {
            return size switch
            {
                <= 1 << 10 => $"{size:F3} B",
                <= 1 << 20 => $"{size << 10:F3} kB",
                <= 1 << 30 => $"{size << 20:F3} MB",
                _ => $"{size << 30:F3} GB",
            };
        }
    }
}
