using GalaSoft.MvvmLight;
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
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Core.Validators;
using Workshop.Model;
using Workshop.Model.Excel;
using Workshop.Helper;
using Workshop.Infrastructure.Helper;
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
            validator.SetValidatorProvider(new DefaultValidatorProvider<EmployeeEntity>());
            this.ImportCommand = new RelayCommand(ImportAction, true);
            this.ValidDataCommand = new RelayCommand(GetDataAction, CanValidate);
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.Employees = new ObservableCollection<EmployeeEntity>();
            this.ProcessResultList = new ObservableCollection<ProcessResultDto>();
            this.ProcessResultList.CollectionChanged += ProcessResultList_CollectionChanged;
            this.PropertyChanged += ImportPageViewModel_PropertyChanged;
        }

        private void ImportPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.IsValidSuccess))
            {
                SubmitCommand.RaiseCanExecuteChanged();
            }
            else if (e.PropertyName == nameof(this.Employees))
            {
                SubmitCommand.RaiseCanExecuteChanged();
                ValidDataCommand.RaiseCanExecuteChanged();

            }
        }

        private void ProcessResultList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.IsValidSuccess = this.ProcessResultList.Count == 0;
        }

        private void SubmitAction()
        {
            var employeeAccount = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeAccount>(this.Employees);
            var employeeSalay = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeSalay>(this.Employees, new MapperConfiguration(cfg=> cfg.CreateMap<EmployeeEntity, EmployeeSalay>().ForMember()));
            var employeeSocialInsuranceAndFund = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeSocialInsuranceAndFund>(this.Employees);
            var enterpriseSocialInsuranceAndFund = AutoMapperHelper.MapToList<EmployeeEntity, EnterpriseSocialInsuranceAndFund>(this.Employees);
            var employeeSocialInsuranceDetail = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeSocialInsuranceDetail>(this.Employees);
            var aa = AutoMapperHelper.MapToList<EmployeeEntity, Employee>(this.Employees).Select(c => new Employee()
            {
                Year = c.Year,
                Mounth = c.Mounth,
                Batch = c.Batch,
                SerialNum = c.SerialNum,
                Dept = c.Dept,
                Proj = c.Proj,
                State = c.State,
                Name = c.Name,
                IDCard = c.IDCard,
                Level = c.Level,
                EmployeeAccount = employeeAccount.FirstOrDefault(d => d.Id == c.Id),
                EmployeeSalay = employeeSalay.FirstOrDefault(d => d.Id == c.Id),
                EmployeeSocialInsuranceAndFund = employeeSocialInsuranceAndFund.FirstOrDefault(d => d.Id == c.Id),
                EnterpriseSocialInsuranceAndFund = enterpriseSocialInsuranceAndFund.FirstOrDefault(d => d.Id == c.Id),
                EmployeeSocialInsuranceDetail = employeeSocialInsuranceDetail.FirstOrDefault(d => d.Id == c.Id)

            });
        }

        private void GetDataAction()
        {
            this.ProcessResultList.Clear();
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
            this.IsValidSuccess = null;

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


        private bool? _isValidSuccess;

        public bool? IsValidSuccess
        {
            get { return _isValidSuccess; }
            set
            {
                _isValidSuccess = value;

                RaisePropertyChanged();
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
