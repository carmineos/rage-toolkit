// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.Windows.Storage.Pickers;

namespace ArchiveTool.Helpers
{
    public static class Pickers
    {
        public static async Task<string?> ShowSingleFilePicker(params string[] filters)
        {
            var picker = new FileOpenPicker(App.Current.MainWindow.AppWindow.Id);

            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;

            if (filters.Length is 0)
            {
                picker.FileTypeFilter.Add("*");
            }
            else
            {
                foreach (var item in filters)
                    picker.FileTypeFilter.Add(item);
            }

            var file = await picker.PickSingleFileAsync();

            return file?.Path;
        }

        public static async Task<string?> ShowSingleFolderPicker()
        {
            var picker = new FolderPicker(App.Current.MainWindow.AppWindow.Id);

            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Desktop;

            var folder = await picker.PickSingleFolderAsync();

            return folder?.Path;
        }

        public static async Task<string?> ShowFileSavePicker(string suggestedFileName, params KeyValuePair<string,List<string>>[] choices)
        {
            var picker = new FileSavePicker(App.Current.MainWindow.AppWindow.Id);

            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            
            if (choices.Length is 0)
            {
                picker.FileTypeChoices.Add("All Types (*.*)", [Path.GetExtension(suggestedFileName)]);
            }
            else
            {
                foreach (var item in choices)
                    picker.FileTypeChoices.Add(item.Key, item.Value);
            }

            picker.SuggestedFileName = suggestedFileName;
            var file = await picker.PickSaveFileAsync();

            return file?.Path;
        }
    }
}
