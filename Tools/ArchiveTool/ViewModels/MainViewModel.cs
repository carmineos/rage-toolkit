// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Services.VirtualFileSystem;
using RageLib.GTA5.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveTool.ViewModels
{
    class MainViewModel : ObservableObject
    {
        private readonly ObservableCollection<ContainerExplorerItem> model;
        
        private ObservableCollection<TreeViewItemViewModel> treeViewItems;
        private TreeViewItemViewModel _selectedTreeViewItem;
        private ObservableCollection<ExplorerItem> _children;
        private ObservableCollection<ExplorerItem> _selectedChildren;

        public ObservableCollection<ExplorerItem> Children
        {
            get => _children;
            private set => SetProperty(ref _children, value);
        }

        public ObservableCollection<TreeViewItemViewModel> TreeViewItems
        {
            get => treeViewItems;
            private set => SetProperty(ref treeViewItems, value);
        }

        public TreeViewItemViewModel SelectedTreeViewItem
        {
            get => _selectedTreeViewItem;
            set => SetProperty(ref _selectedTreeViewItem, value);
        }

        public ObservableCollection<ExplorerItem> SelectedChildren
        {
            get => _selectedChildren;
            set => SetProperty(ref _selectedChildren, value);
        }

        public IRelayCommand OpenFolderCommand { get; }
        public IRelayCommand OpenArchiveCommand { get; }
        public IRelayCommand CloseCommand { get; }
        public IRelayCommand ExitCommand { get; }
        public IRelayCommand ImportCommand { get; }
        public IRelayCommand ExportCommand { get; }
        public IRelayCommand PackFolderCommand { get; }
        
        public MainViewModel()
        {
            model = new ObservableCollection<ContainerExplorerItem>();

            Children = new ObservableCollection<ExplorerItem>();
            TreeViewItems = new ObservableCollection<TreeViewItemViewModel>();

            this.OpenFolderCommand = new RelayCommand(OpenFolder_Clicked);
            this.OpenArchiveCommand = new RelayCommand(OpenArchive_Clicked);
            this.CloseCommand = new RelayCommand(Close_Clicked, Close_CanClick);
            //this.ExitCommand = new RelayCommand(Exit_Clicked);
            //this.ImportCommand = new RelayCommand(Import_Clicked, Import_CanClick);
            //this.ExportCommand = new RelayCommand(Export_Clicked, Export_CanClick);
            this.PackFolderCommand = new RelayCommand(PackFolder_Clicked);
            
            this.PropertyChanged += MainViewModel_PropertyChanged;
        }

        public void OpenFolder(string path)
        {
            var item = new RootExplorerItem(path);
            item.LoadChildren(true);
            model.Add(item);

            var treeRoot = new TreeViewItemViewModel(item, null)
            {
                IsSelected = true,
                IsExpanded = true
            };

            TreeViewItems.Add(treeRoot);
            Children = new ObservableCollection<ExplorerItem>(item.Children);

            UseSelectionChangedHack(treeRoot);
        }

        public void OpenArchive(string path)
        {
            FileInfo file = new FileInfo(path);
            var archive = RageArchiveWrapper7.Open(new FileStream(file.FullName, FileMode.Open, FileAccess.Read), file.Name);
            var item = new ArchiveExplorerItem(archive, null);
            item.LoadChildren(true);
            model.Add(item);

            var treeRoot = new TreeViewItemViewModel(item, null)
            {
                IsSelected = true,
                IsExpanded = true
            };

            TreeViewItems.Add(treeRoot);
            Children = new ObservableCollection<ExplorerItem>(item.Children);

            UseSelectionChangedHack(treeRoot);
        }

        public void OpenArchive_Clicked()
        {
            using var fileDialog = new OpenFileDialog()
            {
                DefaultExt = "rpf",
                AddExtension = true,
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Rage Pack File 7 (*.rpf)|*.rpf"
            };

            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = fileDialog.FileName;

                if (IsAlreadyOpen(path))
                {
                    var msg = MessageBox.Show("The selected path is already open");
                    return;
                }

                OpenArchive(path);
            }
        }

        // TODO: Use an EventToCommand utility to avoid using this
        private void UseSelectionChangedHack(TreeViewItemViewModel treeRoot)
        {
            var dirstack = new Stack<TreeViewItemViewModel>();
            dirstack.Push(treeRoot);
            while (dirstack.Count > 0)
            {
                var qq = dirstack.Pop();
                qq.OnSelectionChanged = TreeViewItemViewModelSelectionChanged;

                foreach (var xx in qq.Children)
                    dirstack.Push(xx);
            }
        }

        public void TreeViewItemViewModelSelectionChanged(TreeViewItemViewModel parameter)
        {
            if (parameter.IsSelected)
            {
                SelectedTreeViewItem = parameter;
                CloseCommand.NotifyCanExecuteChanged();
            }
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        
            switch (e.PropertyName)
            {
                case nameof(SelectedChildren):
                    break;
        
                case nameof(SelectedTreeViewItem):
                    Children = new ObservableCollection<ExplorerItem>(_selectedTreeViewItem.Model.Children);
                    break;
        
                default:
                    break;
            }
        }

        //public bool Open_CanClick()
        //{
        //    return _model.HasKeys;
        //}

        public void OpenFolder_Clicked()
        {
            // It's ridicoulous that this requires WinForms
            using var dialog = new FolderBrowserDialog()
            { 
                SelectedPath = @"C:\Program Files\Rockstar Games\Grand Theft Auto V\",
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;

                if (IsAlreadyOpen(path))
                {
                    var msg = MessageBox.Show("The selected path is already open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                OpenFolder(path);
            }
        }

        public void PackFolder_Clicked()
        {
            using var folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                // TODO: Should the API also check folder content size?
                string path = folderDialog.SelectedPath;
                var name = new DirectoryInfo(path).Name;
                name = Path.GetExtension(name) == ".rpf" ? name : Path.ChangeExtension(name, ".rpf");

                using var saveFileDialog = new SaveFileDialog()
                { 
                    DefaultExt = "rpf",
                    AddExtension = true,
                    FileName = name,
                    Filter = "Rage Pack File 7 (*.rpf)|*.rpf"
                };

                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string outputPath = saveFileDialog.FileName;

                    Task.Run(() => ArchiveUtilities.PackArchive(path, outputPath, true, RageLib.GTA5.Archives.RageArchiveEncryption7.None));
                }
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

        public bool Close_CanClick()
        {
            // Only allow to close root items
            return SelectedTreeViewItem != null 
                && TreeViewItems.Contains(SelectedTreeViewItem);
        }

        public void Close_Clicked()
        {
            
        }

        //public void Exit_Clicked()
        //{
        //    Application.Current.Shutdown();
        //}

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

        //public void Configure_Clicked()
        //{ }
    }
}