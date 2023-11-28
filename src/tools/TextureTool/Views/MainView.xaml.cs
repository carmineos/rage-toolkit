// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Windows;
using TextureTool.ViewModels;

namespace TextureTool.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}