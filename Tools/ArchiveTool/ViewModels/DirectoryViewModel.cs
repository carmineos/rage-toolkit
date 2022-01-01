// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using RageLib.Archives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace ArchiveTool.ViewModels
{
    /// <summary>
    /// Represents a view-model of a directory.
    /// </summary>
    public interface IDirectoryViewModel
    {
        /// <summary>
        /// Gets the name of the directory.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the directory is selected.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the directory is expanded.
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Gets the list of directories in the current directory.
        /// </summary>
        ICollection<DirectoryViewModel> Children { get; }
    }

    public class DirectoryViewModel : ObservableObject, IDirectoryViewModel
    {
        private IArchiveDirectory directory;

        private DirectoryViewModel parentDirectory;
        private List<DirectoryViewModel> childDirectories;
        private bool isSelected;
        private bool isExpanded;
       
        public Action<DirectoryViewModel> OnSelectionChanged;
        
        public IArchiveDirectory GetDirectory()
        {
            return directory;
        }

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

        public ICollection<DirectoryViewModel> Children => childDirectories;

        public ICollection<FileViewModel> GetFiles()
        {
            var files = new List<FileViewModel>();

            foreach (var file in directory.GetFiles())
            {
                if (file is IArchiveBinaryFile)
                {
                    files.Add(new BinaryFileViewModel(file));
                }
                else
                {
                    files.Add(new ResourceFileViewModel(file));
                }
            }

            return files;
        }

        public string Name => parentDirectory == null ? Path.DirectorySeparatorChar.ToString() : directory.Name;

        public DirectoryViewModel(IArchiveDirectory directory, DirectoryViewModel parent)
        {
            this.directory = directory;
            this.parentDirectory = parent;

            childDirectories = new List<DirectoryViewModel>();
            foreach (var f in directory.GetDirectories())
            {
                var dd = new DirectoryViewModel(f, this);
                childDirectories.Add(dd);
            }

            this.PropertyChanged += DirectoryViewModel_PropertyChanged;
        }

        private void DirectoryViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IsSelected):
                    OnSelectionChanged?.Invoke(this);
                    break;

                case nameof(IsExpanded):
                    if (isExpanded && parentDirectory != null)
                        parentDirectory.IsExpanded = true;
                    break;

                default:
                    break;
            }
        }
    }
}