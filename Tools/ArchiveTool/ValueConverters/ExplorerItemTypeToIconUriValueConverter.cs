// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.ViewModels;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using RageLib.GTA5.Services.VirtualFileSystem;
using System;

namespace ArchiveTool.ValueConverters
{
    public class ExplorerItemTypeToIconUriValueConverter : IValueConverter
    {
        private BitmapImage FileImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/file.png", UriKind.RelativeOrAbsolute));
        private BitmapImage FolderImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/folder.png", UriKind.RelativeOrAbsolute));
        private BitmapImage ArchiveFolderImage = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/archive-folder.png", UriKind.RelativeOrAbsolute));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var itemType = (ExplorerItemType)value;

            return itemType switch
            {
                ExplorerItemType.Root => FolderImage,
                ExplorerItemType.File => FileImage,
                ExplorerItemType.Directory => FolderImage,
                ExplorerItemType.Archive => ArchiveFolderImage,
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
