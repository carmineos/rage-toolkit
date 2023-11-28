// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using System;

namespace ArchiveTool.ValueConverters.WinUI
{
    public class WinUIControlEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value switch
            {
                // TreeView
                TreeViewItemInvokedEventArgs treeViewItemInvokedEvent => treeViewItemInvokedEvent.InvokedItem,
                TreeViewCollapsedEventArgs treeViewCollapsedEventArgs => treeViewCollapsedEventArgs.Item,
                TreeViewExpandingEventArgs treeViewExpandingEventArgs => treeViewExpandingEventArgs.Item,

                // BreadcrumbBar
                BreadcrumbBarItemClickedEventArgs breadcrumbBarItemClickedEventArgs => breadcrumbBarItemClickedEventArgs.Item,

                // AutoSuggestBox
                AutoSuggestBoxQuerySubmittedEventArgs autoSuggestBoxQuerySubmittedEventArgs => autoSuggestBoxQuerySubmittedEventArgs.QueryText,
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
