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
using System.Threading;
using System.Threading.Tasks;
using Tools.Core.FileSystem;
using Tools.Core.FileSystem.Abstractions;

namespace ArchiveTool.ViewModels;

public partial class ContainerDetailsViewModel : ObservableObject
{
    private readonly ContainerExplorerItem _model;

    [ObservableProperty]
    private ObservableCollection<DataGridItemViewModel> children;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanExport))]
    private ObservableCollection<DataGridItemViewModel> selectedChildren;

    public bool CanImportFile => _model is IImportFile;
    public bool CanImportDirectory => _model is IImportDirectory;
    public bool CanExport => _model is IExport && selectedChildren.Count > 0;

    public ContainerDetailsViewModel(ContainerExplorerItem model)
    {
        _model = model;
        children = new ObservableCollection<DataGridItemViewModel>();
        selectedChildren = new ObservableCollection<DataGridItemViewModel>();

        LoadChildren();
    }

    private void LoadChildren()
    {
        if (_model is null)
            return;

        Children.Clear();

        foreach (var child in _model.Children)
        {
            Children.Add(new DataGridItemViewModel(child));
        }
    }

    [RelayCommand]
    public void Search(string searchText)
    {
        LoadChildren();

        if (string.IsNullOrEmpty(searchText))
            return;

        var filteredChildren = Children.Where(c => c.Name.Contains(searchText));

        Children = new ObservableCollection<DataGridItemViewModel>(filteredChildren);
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    public async Task ImportFile()
    {
        var destinationPath = await Pickers.ShowSingleFilePicker();

        if (destinationPath is not null)
            ((IImportFile)_model).ImportFile(destinationPath);
    }

    [RelayCommand]
    public async Task ImportDirectory()
    {
        var destinationPath = await Pickers.ShowSingleFolderPicker();

        if (destinationPath is not null)
            ((IImportDirectory)_model).ImportDirectory(destinationPath);
    }

    [RelayCommand]
    public async Task ExportSelectedItems(CancellationToken token)
    {
        var destinationPath = await Pickers.ShowSingleFolderPicker();

        if (destinationPath is null)
            return;

        // TODO: Consider using Parallel Tasks
        foreach(var item in selectedChildren)
            await item.ExportAt(destinationPath, token);
    }

    [RelayCommand]
    public void SetSelectedItems(IList selectedItems)
    {
        var items = selectedItems.Cast<DataGridItemViewModel>();
        SelectedChildren = new ObservableCollection<DataGridItemViewModel>(items);
    }
}
