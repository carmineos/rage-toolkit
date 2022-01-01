// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RageLib.Archives;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ArchiveTool.ViewModels
{
    /// <summary>
    /// Represents a view-model of the main window.
    /// </summary>
    public interface IMainViewModel
    {
        ICommand OpenCommand { get; }
        ICommand CloseCommand { get; }
        ICommand ExitCommand { get; }
        ICommand ImportCommand { get; }
        ICommand ExportCommand { get; }
        ICommand ConfigureCommand { get; }

        ICollection<IDirectoryViewModel> Directories { get; }
        DirectoryViewModel SelectedDirectory { get; set; }
        
        ICollection<FileViewModel> Files { get; }
        FileViewModel SelectedFile { get; set; }
    }

    class MainViewModel : ObservableObject
    {
        private MainModel _model;
        private FileViewModel _selectedFile;
        private ICollection<FileViewModel> _files;
        private DirectoryViewModel _selectedDirectory;
        private ICollection<IDirectoryViewModel> _directories;

        public ICollection<FileViewModel> Files
        {
            get => _files;
            private set => SetProperty(ref _files, value);
        }

        public ICollection<IDirectoryViewModel> Directories
        {
            get => _directories;
            private set => SetProperty(ref _directories, value);
        }

        public DirectoryViewModel SelectedDirectory
        {
            get => _selectedDirectory;
            set => SetProperty(ref _selectedDirectory, value);
        }

        public FileViewModel SelectedFile
        {
            get => _selectedFile;
            set => SetProperty(ref _selectedFile, value);
        }

        public IRelayCommand OpenCommand { get; }
        public IRelayCommand CloseCommand { get; }
        public IRelayCommand ExitCommand { get; }
        public IRelayCommand ImportCommand { get; }
        public IRelayCommand ExportCommand { get; }
        public IRelayCommand ConfigureCommand { get; }
        
        public MainViewModel()
        {
            this._model = new MainModel();

            this.OpenCommand = new RelayCommand(Open_Clicked, Open_CanClick);
            this.CloseCommand = new RelayCommand(Close_Clicked, Close_CanClick);
            this.ExitCommand = new RelayCommand(Exit_Clicked);
            this.ImportCommand = new RelayCommand(Import_Clicked, Import_CanClick);
            this.ExportCommand = new RelayCommand(Export_Clicked, Export_CanClick);
            this.ConfigureCommand = new RelayCommand(Configure_Clicked);

            // TODO: Is this really required?
            this.PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CloseCommand?.NotifyCanExecuteChanged();

            switch (e.PropertyName)
            {
                case nameof(SelectedFile):
                    ExportCommand?.NotifyCanExecuteChanged();
                    break;

                case nameof(SelectedDirectory):
                    Files = _selectedDirectory.GetFiles();
                    ImportCommand?.NotifyCanExecuteChanged();
                    break;

                default:
                    break;
            }
        }

        public bool Open_CanClick()
        {
            return _model.HasKeys;
        }

        public void Open_Clicked()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Select archive";
            if (dlg.ShowDialog() == true)
            {
                // open archive
                this._model.Load(dlg.FileName);

                var vm = new DirectoryViewModel(_model.Archive.Root, null);

                var directories = new List<IDirectoryViewModel>();
                directories.Add(vm);

                Files = null;
                Directories = directories;

                var dirstack = new Stack<DirectoryViewModel>();
                dirstack.Push(vm);
                while (dirstack.Count > 0)
                {
                    var qq = dirstack.Pop();
                    qq.OnSelectionChanged = DirectoryChanged;

                    foreach (var xx in qq.Children)
                        dirstack.Push(xx);
                }

                vm.IsSelected = true;
            }
        }

        public bool Close_CanClick()
        {
            return _model.Archive != null;
        }

        public void Close_Clicked()
        {
            // close archive
            _model.Close();

            Directories = null;
            Files = null;
        }

        public void Exit_Clicked()
        {
            Application.Current.Shutdown();
        }

        public bool Import_CanClick()
        {
            return _model.Archive != null;
        }

        public void Import_Clicked()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Import file";
            if (dlg.ShowDialog() == true)
            {
                _model.Import(SelectedDirectory.GetDirectory(), dlg.FileName);
            }

            Files = SelectedDirectory.GetFiles();
        }

        public bool Export_CanClick()
        {
            return _model.Archive != null && SelectedFile != null;
        }

        public void Export_Clicked()
        {
            var dlg = new SaveFileDialog();
            dlg.Title = "Export file";
            dlg.FileName = SelectedFile.GetFile().Name;
            if (dlg.ShowDialog() == true)
            {
                _model.Export(SelectedFile.GetFile(), dlg.FileName);
            }
        }

        public void Configure_Clicked()
        { }

        


        public void DirectoryChanged(DirectoryViewModel parameter)
        {
            if (parameter.IsSelected)
            {
                SelectedDirectory = parameter;
            }
        }
    }
}