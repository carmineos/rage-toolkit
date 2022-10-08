// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading;
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

        public bool CanExport => _model is IExport;
        public bool CanOpen => true;

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

        public async Task ExportAt(string destinationPath, CancellationToken token)
        {
            var copyPath = Path.Combine(destinationPath, _model.Name);
            _model.ExportItem(copyPath);

            await Task.CompletedTask;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task Export(CancellationToken token)
        {
            var destinationPath = await Pickers.ShowFileSavePicker(_model.Name);

            if (destinationPath is null)
                return;

            _model.ExportItem(destinationPath);    
        }
    }
}
