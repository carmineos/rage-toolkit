// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using RageLib.GTA5.Services.VirtualFileSystem;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArchiveTool.ViewModels
{
    public class BreadcrumbItemViewModel : ObservableObject
    {
        private readonly ContainerExplorerItem _model;

        public string Name => _model.Name;
        
        public ExplorerItemType ItemType => _model.ItemType;

        public ContainerExplorerItem Model => _model;

        public ObservableCollection<BreadcrumbItemViewModel> Children => 
            new ObservableCollection<BreadcrumbItemViewModel>(_model.Children
                .Where(c => c is ContainerExplorerItem)
                .Select(c => new BreadcrumbItemViewModel((ContainerExplorerItem)c)));

        public BreadcrumbItemViewModel(ContainerExplorerItem model)
        {
            _model = model;
        }
    }
}
