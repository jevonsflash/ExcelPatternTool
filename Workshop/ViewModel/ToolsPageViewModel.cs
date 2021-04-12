using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Workshop.ViewModel
{
    public class ToolsPageViewModel:ViewModelBase
    {
        public ToolsPageViewModel()
        {
            this.GoPageCommand = new RelayCommand<string>(GoPageAction);
        }

        private void GoPageAction(string obj)
        {
        }

        public RelayCommand<string> GoPageCommand { get; set; }

    }
}
