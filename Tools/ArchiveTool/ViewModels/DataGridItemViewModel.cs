// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;
using Tools.Core.FileSystem;
using Tools.Core.FileSystem.Abstractions;

namespace ArchiveTool.ViewModels
{
    public partial class DataGridItemViewModel : ObservableObject
    {
        private readonly ExplorerItem _model;

        public string Name => _model.Name;
        public string Extensions => Path.GetExtension(Name);
        public ExplorerItemType ItemType => _model.ItemType;
        public bool CanExport => _model is IExportableItem and not RootExplorerItem;

        public DataGridItemViewModel(ExplorerItem model)
        {
            _model = model;
        }

        [RelayCommand]
        public async Task ItemDoubleTapped()
        {
            // TODO: Navigate to tapped element
            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task Export()
        {
            var destinationnPath = await Pickers.ShowSingleFolderPicker();
            
            if (destinationnPath is null)
                return;
            
            var copyPath = Path.Combine(destinationnPath, _model.Name);

            _model.ExportItem(copyPath);
        }
    }
}
