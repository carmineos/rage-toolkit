// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;

namespace ArchiveTool.ValueConverters.WinUI
{
    public class SelectionChangedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var listView = parameter as ListView;

            return listView.SelectedItems;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
