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
using Workshop.Core.DataBase;
using Workshop.Core.Domains;
using Workshop.Helper;
using Workshop.Model;
using Workshop.Model.Enum;
using Workshop.Model.Excel;

namespace Workshop.ViewModel
{
    public class IndexPageViewModel : ViewModelBase
    {

        private readonly WorkshopDbContext _dbContext;
        public IndexPageViewModel(WorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
            this.InitData();
        }


        private void ContentList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }


        private void InitData()
        {
            int data = 0;

            var task = InvokeHelper.InvokeOnUi<int>(null, () =>
            {
                var result = this._dbContext.Employee.Count();
                return result;


            }, (t) =>
            {
                data = t;
                try
                {
                    this.EmployeeInfoCount = data;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });


        }

        private int _employeeInfoCount;

        public int EmployeeInfoCount
        {
            get { return _employeeInfoCount; }
            set
            {
                _employeeInfoCount = value;
                RaisePropertyChanged();
            }
        }



        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand<string> SubmitCurrentCommand { get; set; }

    }
}
