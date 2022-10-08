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
        public static string SizeString(long? size)
        {
            if (size is null)
                return "";

            decimal d = (decimal)size.Value;
            
            return size switch
            {  
                //null => "",
                //<= 1 << 10 => $"{size} B",
                //<= 1 << 20 => $"{size >> 10:###.###} KB",
                //<= 1 << 30 => $"{size >> 20:###.###} MB",
                //_ => $"{size >> 30:###.###} GB",
                <= 1024 => $"{d:###.###} B",
                <= 1048576 => $"{d / 1024m:###.###} KB",
                <= 1073741824 => $"{d / 1048576m:###.###} MB",
                _ => $"{d / 1073741824m:###.###} GB",
            };
        }
    }
}
