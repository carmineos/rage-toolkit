// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using Tools.Core.FileSystem;
using Tools.Core.FileSystem.Abstractions;

namespace ArchiveTool.ViewModels;

public partial class ContainerDetailsViewModel : ObservableObject
{
    private ContainerExplorerItem _model = null!;

    [ObservableProperty]
    public partial ObservableCollection<DataGridItemViewModel> Children { get; set; } = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanExport))]
    public partial ObservableCollection<DataGridItemViewModel> SelectedChildren { get; set; } = [];

    public bool CanImportFile => _model is IImportFile;
    public bool CanImportDirectory => _model is IImportDirectory;
    public bool CanExport => _model is IExport && SelectedChildren.Count > 0;

    public void SetModel(ContainerExplorerItem model)
    {
        _model = model;

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
        foreach(var item in SelectedChildren)
            await item.ExportAt(destinationPath, token);
    }

    [RelayCommand]
    public void SetSelectedItems(IList selectedItems)
    {
        var items = selectedItems.Cast<DataGridItemViewModel>();
        SelectedChildren = new ObservableCollection<DataGridItemViewModel>(items);
    }
}
