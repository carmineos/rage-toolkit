// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TextureTool.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string info = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
    }
}