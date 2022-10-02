// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tools.Core.FileSystem;
using Tools.Core.FileSystem.Abstractions;

namespace ArchiveTool.ViewModels;

public partial class ContainerDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private ContainerExplorerItem model;

    [ObservableProperty]
    private ObservableCollection<DataGridItemViewModel> children;

    [ObservableProperty]
    private ObservableCollection<DataGridItemViewModel> selectedChildren;
    
    [ObservableProperty]
    private bool canImportFile;
    
    [ObservableProperty]
    private bool canImportDirectory;

    [ObservableProperty]
    private bool canExport;

    public ContainerDetailsViewModel()
    {
        children = new ObservableCollection<DataGridItemViewModel>();
        selectedChildren = new ObservableCollection<DataGridItemViewModel>();
        PropertyChanged += OnPropertyChanged;
    }

    private void LoadChildren()
    {
        if (Model is null)
            return;

        Children.Clear();

        foreach (var child in Model.Children)
        {
            Children.Add(new DataGridItemViewModel(child));
        }
    }

    private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName) { 
            case nameof(Model):
                CanImportFile = Model is IImportFile;
                CanImportDirectory = Model is IImportDirectory;
                CanExport = Model is IExport && SelectedChildren.Count > 0;
                LoadChildren();
                break;
            case nameof(SelectedChildren):
                CanExport = Model is IExport && SelectedChildren.Count > 0;
                break;
            case nameof(Children):
                break;
        }
    }

    [RelayCommand]
    public async Task Search(string searchText)
    {
        await Task.CompletedTask;

        LoadChildren();

        if (string.IsNullOrEmpty(searchText))
            return;

        var filteredChildren = Children.Where(c => c.Name.Contains(searchText)).AsEnumerable();

        Children = new ObservableCollection<DataGridItemViewModel>(filteredChildren);
    }

    [RelayCommand]
    public async Task ImportFile()
    {
        var destinationPath = await Pickers.ShowSingleFilePicker();

        if (destinationPath is not null)
            ((IImportFile)model).ImportFile(destinationPath);
    }

    [RelayCommand]
    public async Task ImportDirectory()
    {
        var destinationPath = await Pickers.ShowSingleFolderPicker();

        if (destinationPath is not null)
            ((IImportDirectory)model).ImportDirectory(destinationPath);
    }

    [RelayCommand]
    public async Task Export(IList<DataGridItemViewModel> selected)
    {
        var destinationPath = await Pickers.ShowSingleFolderPicker();

        if (destinationPath is null)
            return;

        foreach(var item in selected)
            item.Export(destinationPath);
    }

    [RelayCommand]
    public async Task UpdateSelectedItems(IList<object> selectedItems)
    {
        var items = selectedItems.ToList().Cast<DataGridItemViewModel>();
        SelectedChildren = new ObservableCollection<DataGridItemViewModel>(items);
    }
}
