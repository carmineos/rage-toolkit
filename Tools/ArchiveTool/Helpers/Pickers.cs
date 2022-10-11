// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ArchiveTool.Helpers
{
    public static class Pickers
    {
        public static async Task<string> ShowSingleFilePicker(params string[] filters)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, App.WindowHandle);
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;

            if (filters.Length is 0)
            {
                picker.FileTypeFilter.Add("*");
            }
            else
            {
                foreach (var item in filters)
                    picker.FileTypeFilter.Add(item);
            }

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            return file?.Path;
        }

        public static async Task<string> ShowSingleFolderPicker()
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, App.WindowHandle);
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder folder = await picker.PickSingleFolderAsync();

            return folder?.Path;
        }

        public static async Task<string> ShowFileSavePicker(string suggestedFileName, params KeyValuePair<string,List<string>>[] choices)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, App.WindowHandle);
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            
            if (choices.Length is 0)
            {
                picker.FileTypeChoices.Add("All Types (*.*)", new List<string> { Path.GetExtension(suggestedFileName) });
            }
            else
            {
                foreach (var item in choices)
                    picker.FileTypeChoices.Add(item.Key, item.Value);
            }

            picker.SuggestedFileName = suggestedFileName;
            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();

            return file?.Path;
        }
    }
}
