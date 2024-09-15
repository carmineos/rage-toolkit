// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using ArchiveTool.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ArchiveTool.Views;

public sealed partial class ContainerDetailsControl : UserControl
{
    public ContainerDetailsViewModel ViewModel => (ContainerDetailsViewModel)this.DataContext;

    public ContainerDetailsControl()
    {
        this.InitializeComponent();
    }
}
