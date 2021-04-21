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
using GalaSoft.MvvmLight.Ioc;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Core.Validators;
using Workshop.Model;
using Workshop.Service;
using Workshop.Model.Excel;
using Workshop.Helper;
using Workshop.Infrastructure.Models;
using Workshop.Model.Dto;

namespace Workshop.ViewModel
{
    public class ImportPageViewModel : ViewModelBase
    {
        private Validator validator;
        public ImportPageViewModel()
        {
            validator = SimpleIoc.Default.GetInstance<Validator>();
            validator.SetValidatorProvider(new ValidatorProvider<EmployeeEntity>());
            this.ImportCommand = new RelayCommand(ImportAction, CanSubmit);
            this.GetDataCommand = new RelayCommand(GetDataAction, CanSubmit);
            this.Employees = new ObservableCollection<EmployeeEntity>();
            this.ProcessResultList = new ObservableCollection<ProcessResultDto>();
        }

        private void GetDataAction()
        {
            foreach (var item in this.Employees)
            {

                var row = Employees.IndexOf(item);
                var id = ProcessResultList.Count + 1;
                var level = 1;


                var validateResult = validator.Validate(item);
                var result = validateResult.Where(c => c.IsValidated == false)
                    .Select(c => new ProcessResultDto()
                    {
                        Id = id,
                        Row = row,
                        Level = level,
                        Content = c.Content,
                        Column = c.Column,
                    });


                foreach (var processResultDto in result)
                {
                    this.ProcessResultList.Add(processResultDto);

                }

            }
        }

        private string GetSumMainFormula()
        {
            var group = Employees.GroupBy(c => c.Sum.Formula);
            string mainFormula = default;
            int seed = 0;
            foreach (var groupItem in @group)
            {
                seed = Math.Max(seed, groupItem.Count());
                if (seed == groupItem.Count())
                {
                    mainFormula = groupItem.Key;
                }
            }

            return mainFormula;
        }


        private void ImportAction()
        {
            var result = DocHelper.ImportFromDelegator((importer) =>
            {

                var op1 = new ImportOption<EmployeeEntity>(0, 2);
                op1.SheetName = "全职";
                var r1 = importer.Process<EmployeeEntity>(op1);


                return new { Employees = r1 };

            });

            this.Employees = new ObservableCollection<EmployeeEntity>(result.Employees);


        }


        private ObservableCollection<ProcessResultDto> _processResultList;

        public ObservableCollection<ProcessResultDto> ProcessResultList
        {
            get { return _processResultList; }
            set
            {
                _processResultList = value;
                RaisePropertyChanged(nameof(ProcessResultList));
            }
        }
        private ObservableCollection<EmployeeEntity> _employees;

        public ObservableCollection<EmployeeEntity> Employees
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
