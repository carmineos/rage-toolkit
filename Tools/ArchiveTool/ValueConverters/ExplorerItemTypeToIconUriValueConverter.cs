// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using Tools.Core.FileSystem;

namespace ArchiveTool.ValueConverters
{
    public class ExplorerItemTypeToIconUriValueConverter : IValueConverter
    {
        private BitmapImage FileImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/Document.png", UriKind.RelativeOrAbsolute));
        private BitmapImage FolderImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/FolderClosed.png", UriKind.RelativeOrAbsolute));
        private BitmapImage ArchiveFolderImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/FolderClosedTeal.png", UriKind.RelativeOrAbsolute));
        private BitmapImage ArchiveImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/FolderClosedBlue.png", UriKind.RelativeOrAbsolute));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var itemType = (ExplorerItemType)value;

            return itemType switch
            {
                ExplorerItemType.Root => FolderImage,
                ExplorerItemType.File => FileImage,
                ExplorerItemType.Directory => FolderImage,
                ExplorerItemType.Archive => ArchiveImage,
                ExplorerItemType.ArchiveFile => FileImage,
                ExplorerItemType.ArchiveDirectory => ArchiveFolderImage,
                _ => FileImage,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
