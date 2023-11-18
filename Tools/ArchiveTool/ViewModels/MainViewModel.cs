// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tools.Core;
using Tools.Core.FileSystem;
using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;

namespace ArchiveTool.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly List<ContainerExplorerItem> _models;

        [ObservableProperty]
        private ObservableCollection<TreeViewItemViewModel> treeViewItems;

        [ObservableProperty]
        private TreeViewItemViewModel selectedTreeViewItem;

        [ObservableProperty]
        private ContainerDetailsViewModel childrenDetailsViewModel;

        [ObservableProperty]
        private ObservableCollection<BreadcrumbItemViewModel> breadcrumbs;

        public MainViewModel()
        {
            _models = new List<ContainerExplorerItem>();
            childrenDetailsViewModel = new ContainerDetailsViewModel(null);
            treeViewItems = new ObservableCollection<TreeViewItemViewModel>();
            breadcrumbs = new ObservableCollection<BreadcrumbItemViewModel>();
        }

        public void OpenFolder(string path)
        {
            var item = new RootExplorerItem(path);
            item.LoadChildren(true);
            _models.Add(item);

            var treeRoot = new TreeViewItemViewModel(item, null)
            {
                IsSelected = false,
                IsExpanded = true
            };

            TreeViewItems.Add(treeRoot);
        }

        public void OpenArchive(string path)
        {
            FileInfo file = new FileInfo(path);
            var archive = RageArchiveWrapper7.Open(new FileStream(file.FullName, FileMode.Open, FileAccess.Read), file.Name);
            var item = new ArchiveExplorerItem(archive, null);
            item.LoadChildren(true);
            _models.Add(item);

            var treeRoot = new TreeViewItemViewModel(item, null)
            {
                IsSelected = false,
                IsExpanded = false
            };

            TreeViewItems.Add(treeRoot);
        }

        [RelayCommand]
        public async Task OpenArchive()
        {
            string path = await Pickers.ShowSingleFilePicker(".rpf");

            if (path is null)
                return;

            if (IsAlreadyOpen(path))
            {
                // TODO: Show Content Dialog here!
                return;
            }

            OpenArchive(path);
        }

        [RelayCommand]
        public async Task OpenFolder()
        {
            string path = await Pickers.ShowSingleFolderPicker();

            if (path is null)
                return;

            if (IsAlreadyOpen(path))
            {
                // TODO: Show Content Dialog here!
                return;
            }

            OpenFolder(path);
        }

        [RelayCommand]
        public async Task PackFolder()
        {
            string path = await Pickers.ShowSingleFolderPicker();

            if (path is null)
                return;

            var name = new DirectoryInfo(path).Name;
            name = Path.GetExtension(name) == ".rpf" ? name : Path.ChangeExtension(name, ".rpf");

            string savePath = await Pickers.ShowFileSavePicker(name, new KeyValuePair<string, List<string>>(FileTypes.Rage.RagePackFile.Name, new() { FileTypes.Rage.RagePackFile.Extension }));

            if (savePath is null)
                return;

            //// TODO: Show Error messages in case of wrong paths
            //var notification = new AppNotificationBuilder()
            //    .AddText("Packing Archive")
            //    .AddProgressBar(new AppNotificationProgressBar()
            //    {
            //        Title = $"Packing {name}",
            //        Value = 0.0,
            //        ValueStringOverride = "Packed 0/0",
            //        Status = "Packing..."
            //    })
            //    .AddButton(new AppNotificationButton()
            //    {
            //        Content = "Open Path",
            //        InvokeUri = new Uri(new FileInfo(savePath).Directory.FullName)
            //    })
            //    .SetGroup("archives-operations")
            //    .SetTag("packing-archive")
            //    .BuildNotification();

            //AppNotificationManager.Default.Show(notification);

            await Task.Run(() =>
            {
                ArchiveUtilities.PackArchive(path, savePath, true, RageLib.GTA5.Archives.RageArchiveEncryption7.NG);

                //AppNotificationManager.Default.UpdateAsync(new AppNotificationProgressData(2)
                //{
                //    Title = $"Packing {name}",
                //    Value = 1.0,
                //    ValueStringOverride =
                //    "Packed 1/1",
                //    Status = "Packing..."
                //}, "packing-archive");
            });
        }

        [RelayCommand]
        public void NavigateToParent()
        {
            if (SelectedTreeViewItem?.Parent is not null)
                SelectedTreeViewItem = SelectedTreeViewItem.Parent;
        }

        partial void OnSelectedTreeViewItemChanged(TreeViewItemViewModel value)
        {
            UpdateBreadcrumbs();
            ChildrenDetailsViewModel = new ContainerDetailsViewModel(value.Model);
            value.IsSelected = true;
            value.IsExpanded = true;
        }

        public bool IsAlreadyOpen(string path)
        {
            foreach (var item in TreeViewItems)
            {
                if (item.Model.PhysicalPath == path)
                    return true;

                if (item.Model.PhysicalPath.StartsWith(path))
                    return true;

                // TODO: It should check recursively to avoid opening two nested folders
                // We could just check if path is a subpath of any open item
            }

            return false;
        }

        [RelayCommand]
        public void TreeViewExpanding(TreeViewItemViewModel treeViewItemViewModel)
        {
            treeViewItemViewModel.ExpandCommand.Execute(null);
        }

        [RelayCommand]
        public void TreeViewCollapsed(TreeViewItemViewModel treeViewItemViewModel)
        {
            treeViewItemViewModel.CollapseCommand.Execute(null);
        }

        [RelayCommand]
        public void TreeViewItemInvoked(TreeViewItemViewModel treeViewItemViewModel)
        {
            SelectedTreeViewItem = treeViewItemViewModel;
        } 

        [RelayCommand]
        public void BreadcrumbBarItemClicked(BreadcrumbItemViewModel breadcrumb)
        {
            var selected = SelectedTreeViewItem;

            while (selected.Model != breadcrumb.Model)
            {
                selected = selected.Parent;
            }

            SelectedTreeViewItem = selected;
        }

        [RelayCommand]
        public async Task AutoSuggestBoxQuerySubmitted(string queryText)
        {
            // TODO: Global search, display results in new tab
            await Task.CompletedTask;
        }

        private void UpdateBreadcrumbs()
        {
            if (SelectedTreeViewItem is null)
                return;

            Breadcrumbs.Clear();
            Stack<ContainerExplorerItem> stack = new Stack<ContainerExplorerItem>();

            var selected = SelectedTreeViewItem;          
            while (selected.Parent is not null)
            {
                stack.Push(selected.Model);
                selected = selected.Parent;
            }
            stack.Push(selected.Model);

            while (stack.Count > 0)
                Breadcrumbs.Add(new BreadcrumbItemViewModel(stack.Pop()));
        }
    }
}