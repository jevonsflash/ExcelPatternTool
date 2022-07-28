using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ExcelPatternTool.Core.DataBase;
using ExcelPatternTool.Core.Validators;
using ExcelPatternTool.Model;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core;
using ExcelPatternTool.Model.Dto;
using CommunityToolkit.Mvvm.DependencyInjection;
using ExcelPatternTool.Core.Validators.Implements;
using ExcelPatternTool.Helper;
using ExcelPatternTool.Core.Excel.Models;
using ExcelPatternTool.Core.Excel.Models.Interfaces;
using ExcelPatternTool.Core.EntityProxy;

namespace ExcelPatternTool.ViewModel
{
    public class ImportPageViewModel : ObservableObject
    {
        public event EventHandler OnFinished;
        private Validator validator;
        public ImportPageViewModel()
        {
            validator = Ioc.Default.GetRequiredService<Validator>();
            validator.SetValidatorProvider(EntityProxyContainer.Current.EntityType, new DefaultValidatorProvider());
            this.ImportCommand = new RelayCommand(ImportAction, () => true);
            this.ValidDataCommand = new RelayCommand(GetDataAction, CanValidate);
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.Employees = new ObservableCollection<object>();
            this.ProcessResultList = new ObservableCollection<ProcessResultDto>();
            this.ProcessResultList.CollectionChanged += ProcessResultList_CollectionChanged;
            this.PropertyChanged += ImportPageViewModel_PropertyChanged;
        }

        private void ImportPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.IsValidSuccess))
            {
                SubmitCommand.NotifyCanExecuteChanged();
            }
            else if (e.PropertyName == nameof(this.Employees))
            {
                SubmitCommand.NotifyCanExecuteChanged();
                ValidDataCommand.NotifyCanExecuteChanged();

            }
        }

        private void ProcessResultList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.IsValidSuccess = this.ProcessResultList.Count == 0;
        }

        private async void SubmitAction()
        {
            var task = InvokeHelper.InvokeOnUi<IEnumerable<object>>(null, () =>
            {

                foreach (var employee in Employees)
                {
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        Ioc.Default.GetRequiredService<CategoryPageViewModel>().Entities.Add((IExcelEntity)employee);
                    });

                }


                return Employees;



            }, async (t) =>
            {

                this.Employees.Clear();
                this.OnFinished?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("已完成导入");

            });
        }

        private void GetDataAction()
        {
            this.ProcessResultList.Clear();
            foreach (var item in this.Employees)
            {

                var row = (item as IExcelEntity).RowNumber + 1;
                var id = ProcessResultList.Count + 1;
                var level = 1;


                var validateResult = validator.Validate(item);
                var result = validateResult.Where(c => c.IsValidated == false)
                    .Select(c => new ProcessResultDto()
                    {
                        Id = id,
                        Row = row,
                        Column = c.Column,
                        Level = level,
                        Content = c.Content,
                        KeyName = c.KeyName,
                    });


                foreach (var processResultDto in result)
                {
                    this.ProcessResultList.Add(processResultDto);

                }


            }
            var currentCount = ProcessResultList.Count();

        }



        private void ImportAction()
        {

            this.Employees.Clear();
            var task = InvokeHelper.InvokeOnUi<dynamic>(null, () =>
            {



                var result = DocHelper.ImportFromDelegator((importer) =>
                {



                    var op1 = new ImportOption(EntityProxyContainer.Current.EntityType, 0, 2);
                    var r1 = importer.Process(EntityProxyContainer.Current.EntityType, op1);


                    return new { Employees = r1 };

                });
                return result;


            }, (t) =>
            {
                var data = t;
                if (data != null)
                {


                    this.Employees = new ObservableCollection<object>(data.Employees);
                    this.IsValidSuccess = null;
                }
            });

        }


        private ObservableCollection<ProcessResultDto> _processResultList;

        public ObservableCollection<ProcessResultDto> ProcessResultList
        {
            get { return _processResultList; }
            set
            {
                _processResultList = value;
                OnPropertyChanged(nameof(ProcessResultList));
            }
        }
        private ObservableCollection<object> _employees;

        public ObservableCollection<object> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }


        private bool? _isValidSuccess;

        public bool? IsValidSuccess
        {
            get { return _isValidSuccess; }
            set
            {
                _isValidSuccess = value;

                OnPropertyChanged();
            }
        }

        private bool CanSubmit()
        {
            return IsValidSuccess.HasValue && IsValidSuccess.Value;
        }

        private bool CanValidate()
        {
            if (this.Employees.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public RelayCommand ValidDataCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }

        public RelayCommand ImportCommand { get; set; }
    }
}
