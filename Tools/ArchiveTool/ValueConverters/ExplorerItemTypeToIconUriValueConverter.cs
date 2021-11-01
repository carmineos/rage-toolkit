// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.Services.VirtualFileSystem;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ArchiveTool.ValueConverters
{
    public class ExplorerItemTypeToIconUriValueConverter : IValueConverter
    {
        private Uri FileUri = new Uri("pack://application:,,,/Icons/file.png");
        private Uri FolderUri = new Uri("pack://application:,,,/Icons/folder.png");
        private Uri ArchiveUri = new Uri("pack://application:,,,/Icons/archive.png");
        private Uri ArchiveFolderUri = new Uri("pack://application:,,,/Icons/archive-folder.png");
        private Uri ArchiveFileUri = new Uri("pack://application:,,,/Icons/archive-file.png");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemType = (ExplorerItemType)value;
           
            return itemType switch
            {
                ExplorerItemType.Root => FolderUri,
                ExplorerItemType.File => FileUri,
                ExplorerItemType.Directory => FolderUri,
                ExplorerItemType.Archive => ArchiveUri,
                ExplorerItemType.ArchiveFile => ArchiveFileUri,
                ExplorerItemType.ArchiveDirectory => ArchiveFolderUri,
                _ => FileUri,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
