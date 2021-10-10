/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

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