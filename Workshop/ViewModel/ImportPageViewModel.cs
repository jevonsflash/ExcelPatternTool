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
using Workshop.Core.DataBase;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Core.Validators;
using Workshop.Model;
using Workshop.Model.Excel;
using Workshop.Helper;
using Workshop.Infrastructure.Core;
using Workshop.Infrastructure.Helper;
using Workshop.Infrastructure.Models;
using Workshop.Model.Dto;

namespace Workshop.ViewModel
{
    public class ImportPageViewModel : ViewModelBase
    {
        private readonly WorkshopDbContext _dbContext;
        private Validator validator;
        public ImportPageViewModel(WorkshopDbContext dbContext)
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
            this._dbContext = dbContext;
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

        private async void SubmitAction()
        {
            var task = InvokeHelper.InvokeOnUi<IEnumerable<Employee>>(null, () =>
            {
                var employeeAccount = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeAccount>(this.Employees);
                var employeeSalay = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeSalay>(this.Employees, new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IAdvancedType, object>().ConvertUsing(s => s.GetValue());
                    cfg.CreateMap<EmployeeEntity, EmployeeSalay>()
                        .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum.Value));
                }));
                var employeeSocialInsuranceAndFund = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeSocialInsuranceAndFund>(this.Employees, new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IAdvancedType, object>().ConvertUsing(s => s.GetValue());
                    cfg.CreateMap<EmployeeEntity, EmployeeSocialInsuranceAndFund>()
                        .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum1.Value));
                }));
                var enterpriseSocialInsuranceAndFund = AutoMapperHelper.MapToList<EmployeeEntity, EnterpriseSocialInsuranceAndFund>(this.Employees, new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IAdvancedType, object>().ConvertUsing(s => s.GetValue());
                    cfg.CreateMap<EmployeeEntity, EnterpriseSocialInsuranceAndFund>()
                        .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum2.Value));
                }));
                var employeeSocialInsuranceDetail = AutoMapperHelper.MapToList<EmployeeEntity, EmployeeSocialInsuranceDetail>(this.Employees, new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IAdvancedType, object>().ConvertUsing(s => s.GetValue());
                    cfg.CreateMap<EmployeeEntity, EmployeeSocialInsuranceDetail>();
                }));
                var resultEmployees = AutoMapperHelper.MapToList<EmployeeEntity, Employee>(this.Employees).Select(c => new Employee()
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
                    JobCate = c.JobCate,
                    EmployeeAccount = employeeAccount.FirstOrDefault(d => d.Id == c.Id),
                    EmployeeSalay = employeeSalay.FirstOrDefault(d => d.Id == c.Id),
                    EmployeeSocialInsuranceAndFund = employeeSocialInsuranceAndFund.FirstOrDefault(d => d.Id == c.Id),
                    EnterpriseSocialInsuranceAndFund = enterpriseSocialInsuranceAndFund.FirstOrDefault(d => d.Id == c.Id),
                    EmployeeSocialInsuranceDetail = employeeSocialInsuranceDetail.FirstOrDefault(d => d.Id == c.Id)

                });
                this._dbContext.Employee.AddRangeAsync(resultEmployees);
                var result = this._dbContext.SaveChanges();

                return resultEmployees;



            }, async (t) =>
            {

            });
        }

        private void GetDataAction()
        {
            this.ProcessResultList.Clear();
            foreach (var item in this.Employees)
            {

                var row =item.RowNumber+1;
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
        }



        private void ImportAction()
        {

            this.Employees.Clear();
            var task = InvokeHelper.InvokeOnUi<dynamic>(null, () =>
            {
                var result = DocHelper.ImportFromDelegator((importer) =>
                {

                    var op1 = new ImportOption<EmployeeEntity>(0, 2);
                    op1.SheetName = "全职";
                    var r1 = importer.Process<EmployeeEntity>(op1);


                    return new { Employees = r1 };

                });
                return result;


            }, (t) =>
            {
                var data = t;
                if (data != null)
                {


                    this.Employees = new ObservableCollection<EmployeeEntity>(data.Employees);
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
