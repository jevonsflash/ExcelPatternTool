using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Workshop.Helper;
using Workshop.Model;
using Workshop.Model.Enum;
using Workshop.Model.Excel;

namespace Workshop.ViewModel
{
    public class IndexPageViewModel : ViewModelBase
    {

        public IndexPageViewModel()
        {
        }

     
        private void ContentList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

    



      

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand<string> SubmitCurrentCommand { get; set; }

    }
}
