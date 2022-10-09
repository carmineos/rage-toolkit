// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using Microsoft.UI.Xaml.Data;
using System;

namespace ArchiveTool.ValueConverters
{
    public class SizeToStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var size = value as long?;
            return size is null ? "" : SizeHelpers.GetExplorerSize(size.Value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
