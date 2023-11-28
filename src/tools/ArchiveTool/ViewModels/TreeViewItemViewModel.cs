﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tools.Core.FileSystem;

namespace ArchiveTool.ViewModels
{
    public partial class TreeViewItemViewModel : ObservableObject
    {
        private readonly ContainerExplorerItem _model;
        private readonly TreeViewItemViewModel _parent;
        private readonly ObservableCollection<TreeViewItemViewModel> _children;

        [ObservableProperty]
        private bool isSelected;

        [ObservableProperty]
        private bool isExpanded;

        [ObservableProperty]
        private bool hasUnrealizedChildren;

        public ContainerExplorerItem Model => _model;

        public ExplorerItemType ItemType => _model.ItemType;

        public string Name => _model.Name;

        public ObservableCollection<TreeViewItemViewModel> Children => _children;

        public TreeViewItemViewModel Parent => _parent;

        public TreeViewItemViewModel(ContainerExplorerItem explorerItem, TreeViewItemViewModel parent)
        {
            _model = explorerItem;
            _parent = parent;

            _children = new ObservableCollection<TreeViewItemViewModel>();
            LoadChildren();
        }

        partial void OnIsExpandedChanged(bool value)
        {
            if (value && Parent is not null)
            {
                Parent.IsExpanded = true;
            }
        }

        private void LoadChildren()
        {
            ClearChildren();

            foreach (var f in _model.Children)
            {
                if (f is ContainerExplorerItem c)
                {
                    var dd = new TreeViewItemViewModel(c, this);
                    _children.Add(dd);
                }
            }

            HasUnrealizedChildren = false;
        }

        private void ClearChildren()
        {
            foreach (var item in Children)
            {
                item.ClearChildren();
            }

            Children.Clear();
            HasUnrealizedChildren = true;
        }

        [RelayCommand]
        public void Expand()
        {
            if (HasUnrealizedChildren)
                LoadChildren();
        }

        [RelayCommand]
        public void Collapse()
        {
            ClearChildren();
        }
    }
}