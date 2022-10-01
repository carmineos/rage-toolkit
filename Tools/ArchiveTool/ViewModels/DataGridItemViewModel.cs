// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;
using Tools.Core.FileSystem;

namespace ArchiveTool.ViewModels
{
    public partial class DataGridItemViewModel : ObservableObject
    {
        private readonly ExplorerItem _model;

        public string Name => _model.Name;
        public string Extensions => Path.GetExtension(Name);
        public ExplorerItemType ItemType => _model.ItemType;


        [RelayCommand]
        public async Task ItemDoubleTapped()
        {
            // TODO: Navigate to tapped element
            await Task.CompletedTask;
        }
        
        public DataGridItemViewModel(ExplorerItem model)
        {
            _model = model;
        }
    }
}
