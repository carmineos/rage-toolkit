// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.UI.Xaml.Controls;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Services.VirtualFileSystem;
using RageLib.GTA5.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;

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
        private ObservableCollection<DataGridItemViewModel> children;
        
        [ObservableProperty]
        private ObservableCollection<DataGridItemViewModel> selectedChildren;
        
        [ObservableProperty]
        private ObservableCollection<BreadcrumbItemViewModel> breadcrumbs;
        
        public MainViewModel()
        {
            _models = new List<ContainerExplorerItem>();

            children = new ObservableCollection<DataGridItemViewModel>();
            treeViewItems = new ObservableCollection<TreeViewItemViewModel>();
            breadcrumbs = new ObservableCollection<BreadcrumbItemViewModel>();
            
            this.PropertyChanged += MainViewModel_PropertyChanged;

            TreeViewCollapsed = new TypedEventHandler<TreeView, TreeViewCollapsedEventArgs>(OnTreeViewCollapsed);
            TreeViewExpanding = new TypedEventHandler<TreeView, TreeViewExpandingEventArgs>(OnTreeViewExpanding);
            TreeViewItemInvoked = new TypedEventHandler<TreeView, TreeViewItemInvokedEventArgs>(OnTreeViewItemInvoked);
            BreadcrumbBarItemClicked = new TypedEventHandler<BreadcrumbBar, BreadcrumbBarItemClickedEventArgs>(OnBreadcrumbBarItemClicked);
            AutoSuggestBoxTextChanged = new TypedEventHandler<AutoSuggestBox, AutoSuggestBoxTextChangedEventArgs>(OnAutoSuggestBoxTextChanged);
            AutoSuggestBoxQuerySubmitted = new TypedEventHandler<AutoSuggestBox, AutoSuggestBoxQuerySubmittedEventArgs>(OnAutoSuggestBoxQuerySubmitted);
            
            // TEMP
            //OpenFolder(@"C:\Program Files\Rockstar Games\Grand Theft Auto V\");
            //SelectedTreeViewItem = TreeViewItems[0];
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

        [ICommand]
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
        
        [ICommand]
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

        [ICommand]
        public async Task PackFolder()
        {
            string path = await Pickers.ShowSingleFolderPicker();

            if (path is null)
                return;

            var name = new DirectoryInfo(path).Name;
            name = Path.GetExtension(name) == ".rpf" ? name : Path.ChangeExtension(name, ".rpf");

            string savePath = await Pickers.ShowFileSavePicker(name, new KeyValuePair<string, List<string>>("Rage Pack File 7", new List<string>() { ".rpf" }));

            if (savePath is null)
                return;

            // TODO: Show Error messages in case of wrong paths

            _ = Task.Run(() =>
            {
                ArchiveUtilities.PackArchive(path, savePath, true, RageLib.GTA5.Archives.RageArchiveEncryption7.None);
                new ToastContentBuilder()
                .AddText("Pack Folder Completed")
                .AddText(savePath)
                .AddButton(new ToastButton("Open Path", "")
                .SetProtocolActivation(new Uri(new FileInfo(savePath).Directory.FullName)))
                .Show();
            });
        }

        [ICommand]
        public async Task NavigateToParent()
        {
            if (SelectedTreeViewItem?.Parent is not null)
                SelectedTreeViewItem = SelectedTreeViewItem.Parent;
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        
            switch (e.PropertyName)
            {
                case nameof(SelectedChildren):
                    break;
        
                case nameof(SelectedTreeViewItem):
                    UpdateBreadcrumbs();
                    UpdateChildren();
                    SelectedTreeViewItem.IsSelected = true;
                    SelectedTreeViewItem.IsExpanded = true;
                    break;
        
                default:
                    break;
            }
        }

        public bool IsAlreadyOpen(string path)
        {
            foreach(var item in TreeViewItems)
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

        //public bool Import_CanClick()
        //{
        //    return _model.Archive != null;
        //}

        //public void Import_Clicked()
        //{
        //    var dlg = new OpenFileDialog();
        //    dlg.Title = "Import file";
        //    if (dlg.ShowDialog() == true)
        //    {
        //        _model.Import(SelectedDirectory.GetDirectory(), dlg.FileName);
        //    }

        //    Files = SelectedDirectory.GetFiles();
        //}

        //public bool Export_CanClick()
        //{
        //    return _model.Archive != null && SelectedFile != null;
        //}

        //public void Export_Clicked()
        //{
        //    var dlg = new SaveFileDialog();
        //    dlg.Title = "Export file";
        //    dlg.FileName = SelectedFile.GetFile().Name;
        //    if (dlg.ShowDialog() == true)
        //    {
        //        _model.Export(SelectedFile.GetFile(), dlg.FileName);
        //    }
        //}

        public TypedEventHandler<TreeView, TreeViewCollapsedEventArgs> TreeViewCollapsed { get; }
        public TypedEventHandler<TreeView, TreeViewExpandingEventArgs> TreeViewExpanding { get; }
        public TypedEventHandler<TreeView, TreeViewItemInvokedEventArgs> TreeViewItemInvoked { get; }
        public TypedEventHandler<BreadcrumbBar, BreadcrumbBarItemClickedEventArgs> BreadcrumbBarItemClicked { get; }
        public TypedEventHandler<AutoSuggestBox, AutoSuggestBoxTextChangedEventArgs> AutoSuggestBoxTextChanged { get; }
        public TypedEventHandler<AutoSuggestBox, AutoSuggestBoxQuerySubmittedEventArgs> AutoSuggestBoxQuerySubmitted { get; }

        private void OnTreeViewItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args?.InvokedItem is TreeViewItemViewModel treeViewItemViewModel)
                SelectedTreeViewItem = treeViewItemViewModel;
        }

        private void OnTreeViewExpanding(TreeView sender, TreeViewExpandingEventArgs args)
        {
            if (args?.Item is TreeViewItemViewModel treeViewItemViewModel)
                treeViewItemViewModel.ExpandCommand.Execute(this);
        }

        private void OnTreeViewCollapsed(TreeView sender, TreeViewCollapsedEventArgs args)
        {
            if (args?.Item is TreeViewItemViewModel treeViewItemViewModel)
                treeViewItemViewModel.CollapseCommand.Execute(this);
        }

        private void OnBreadcrumbBarItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
        {
            if (args?.Item is BreadcrumbItemViewModel breadcrumb)
            {
                var selected = SelectedTreeViewItem;
                
                while (selected.Model != breadcrumb.Model)
                {
                    selected = selected.Parent;
                }

                SelectedTreeViewItem = selected;
            }
        }


        private void OnAutoSuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // TODO: cache children collection, only change a filtered copy
            UpdateChildren();

            var searchText = sender?.Text;

            if (string.IsNullOrEmpty(searchText))
                return;

            var filteredChildren = Children.Where(c => c.Name.Contains(searchText)).AsEnumerable();

            Children = new ObservableCollection<DataGridItemViewModel>(filteredChildren);
        }

        private void OnAutoSuggestBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            // TODO: Global search, display results in new tab
        }

        public void UpdateBreadcrumbs()
        {
            if (SelectedTreeViewItem is null)
                return;

            Breadcrumbs.Clear();


            Stack<ContainerExplorerItem> stack = new Stack<ContainerExplorerItem>();
            
            var selected = SelectedTreeViewItem;
            while(selected.Parent is not null)
            {
                stack.Push(selected.Model);
                selected = selected.Parent;
            }
            stack.Push(selected.Model);

            while (stack.Count > 0)
                Breadcrumbs.Add(new BreadcrumbItemViewModel(stack.Pop()));
        }

        public void UpdateChildren()
        {
            if (SelectedTreeViewItem is null)
                return;

            Children.Clear();

            foreach (var child in SelectedTreeViewItem.Model.Children)
            {
                Children.Add(new DataGridItemViewModel(child));
            }
        }
    }
}