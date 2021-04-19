using GalaSoft.MvvmLight;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Model;
using Workshop.Service;
using Workshop.Model.Excel;
using Workshop.Helper;
using Workshop.Infrastructure.Models;

namespace Workshop.ViewModel
{
    public class ImportPageViewModel : ViewModelBase
    {
        public ImportPageViewModel()
        {
            this.ImportCommand = new RelayCommand(ImportAction, CanSubmit);
            this.GetDataCommand = new RelayCommand(GetDataAction, CanSubmit);
            this.Employees = new ObservableCollection<EmpoyeeEntity>();
            this.ProcessResultList=new ObservableCollection<ProcessResult>();
        }

        private void GetDataAction()
        {
            foreach (var item in this.Employees)
            {
                var group = Employees.GroupBy(c => c.Sum.Formula);
                string mainFormula = default;
                int seed = 0;
                foreach (var groupItem in group)
                {
                    seed = Math.Max(seed, groupItem.Count());
                    if (seed == groupItem.Count())
                    {
                        mainFormula = groupItem.Key;
                    }
                }

                foreach (var empoyee in Employees)
                {
                    var row = Employees.IndexOf(empoyee);
                    var id = ProcessResultList.Count + 1;
                    var level = 1;
                    if (empoyee.Sum.Formula != mainFormula)
                    {
                        var column = "应发合计";
                        var content = $"此列中的公式不满足该列主要公式，主要公式为:{mainFormula}";
                        var newItem = new ProcessResult
                        {
                            Column = column,
                            Row = row,
                            Content = content,
                            Id = id,
                            Level = level
                        };
                        this.ProcessResultList.Add(newItem);

                    }

                    if (empoyee.AgeBonus < 0)
                    {
                        var column = "年限";
                        var content = $"此列中数据不满足大于等于0";
                        var newItem = new ProcessResult
                        {
                            Column = column,
                            Row = row,
                            Content = content,
                            Id = id,
                            Level = level
                        };
                        this.ProcessResultList.Add(newItem);
                    }

                }
            }
        }


        private void ImportAction()
        {
            var result = DocHelper.ImportFromDelegator((importer) =>
            {

                var op1 = new ImportOption<EmpoyeeEntity>(0, 2);
                op1.SheetName = "全职";
                var r1 = importer.Process<EmpoyeeEntity>(op1);


                return new { Employees = r1 };

            });

            this.Employees = new ObservableCollection<EmpoyeeEntity>(result.Employees);


        }


        private ObservableCollection<ProcessResult> _processResultList;

        public ObservableCollection<ProcessResult> ProcessResultList
        {
            get { return _processResultList; }
            set
            {
                _processResultList = value;
                RaisePropertyChanged(nameof(ProcessResultList));
            }
        }
        private ObservableCollection<EmpoyeeEntity> _employees;

        public ObservableCollection<EmpoyeeEntity> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }
        private bool CanSubmit()
        {
            //return !string.IsNullOrEmpty(this.CurrentContent);
            //return this.ContentList.Count > 0;
            return true;
        }




        public RelayCommand GetDataCommand { get; set; }

        public RelayCommand ImportCommand { get; set; }
    }
}
