// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Input;
using RageLib.GTA5.Services.VirtualFileSystem;
using System.IO;

namespace ArchiveTool.ViewModels
{
    public class DataGridItemViewModel : ObservableObject
    {
        private readonly ExplorerItem _model;

        public string Name => _model.Name;
        public string Extensions => Path.GetExtension(Name);
        public ExplorerItemType ItemType => _model.ItemType;

        public DoubleTappedEventHandler ItemDoubleTapped { get; }

        private void OnItemDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            // TODO: Navigate to tapped element
        }

        public DataGridItemViewModel(ExplorerItem model)
        {
            _model = model; 
            ItemDoubleTapped = new DoubleTappedEventHandler(OnItemDoubleTapped);
        }
    }
}
