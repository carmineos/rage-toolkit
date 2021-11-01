// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.Toolkit.Mvvm.ComponentModel;
using RageLib.GTA5.Services.VirtualFileSystem;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ArchiveTool.ViewModels
{
    public class TreeViewItemViewModel : ObservableObject
    {
        private readonly ContainerExplorerItem _model;
        private readonly TreeViewItemViewModel _parent;
        private readonly ObservableCollection<TreeViewItemViewModel> _children;

        private bool isSelected;
        private bool isExpanded;
       
        public Action<TreeViewItemViewModel> OnSelectionChanged;

        public ContainerExplorerItem Model => _model;

        public ExplorerItemType ItemType => _model.ItemType;

        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }

        public string Name => _model.Name;

        public ObservableCollection<TreeViewItemViewModel> Children => _children;

        public TreeViewItemViewModel(ContainerExplorerItem explorerItem, TreeViewItemViewModel parent)
        {
            _model = explorerItem;
            _parent = parent;

            _children = new ObservableCollection<TreeViewItemViewModel>();
            
            foreach (var f in explorerItem.Children)
            {
                if(f is ContainerExplorerItem c)
                {
                    var dd = new TreeViewItemViewModel(c, this);
                    _children.Add(dd);
                }
            }

            this.PropertyChanged += TreeViewItemViewModel_PropertyChanged;
        }

        private void TreeViewItemViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IsSelected):
                    OnSelectionChanged?.Invoke(this);
                    break;

                case nameof(IsExpanded):
                    if (isExpanded && _parent != null)
                        _parent.IsExpanded = true;
                    break;

                default:
                    break;
            }
        }
    }
}