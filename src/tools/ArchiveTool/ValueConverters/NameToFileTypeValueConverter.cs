// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.UI.Xaml.Data;
using System;
using System.IO;
using Tools.Core;

namespace ArchiveTool.ValueConverters
{
    public class NameToFileTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var itemName = value as string;

            var ext = Path.GetExtension(itemName);

            if (string.IsNullOrEmpty(ext))
                return "Folder";

            if (FileTypes.AllTypes.TryGetValue(ext, out var type))
                return type.Name;

            return $"File ({ext})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
