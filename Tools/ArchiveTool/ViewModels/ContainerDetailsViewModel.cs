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
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanExport))]
    [NotifyPropertyChangedFor(nameof(CanImportFile))]
    [NotifyPropertyChangedFor(nameof(CanImportDirectory))]
    private ContainerExplorerItem model;

    [ObservableProperty]
    private ObservableCollection<DataGridItemViewModel> children;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanExport))]
    private ObservableCollection<DataGridItemViewModel> selectedChildren;

    public bool CanImportFile => model is IImportFile;
    public bool CanImportDirectory => model is IImportDirectory;
    public bool CanExport => model is IExport && selectedChildren.Count > 0;

    public ContainerDetailsViewModel()
    {
        children = new ObservableCollection<DataGridItemViewModel>();
        selectedChildren = new ObservableCollection<DataGridItemViewModel>();
    }

    partial void OnModelChanged(ContainerExplorerItem value)
    {
        LoadChildren();
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
    public void SetSelectedItems(IList<object> selectedItems)
    {
        var items = selectedItems.Cast<DataGridItemViewModel>();
        SelectedChildren = new ObservableCollection<DataGridItemViewModel>(items);
    }
}
