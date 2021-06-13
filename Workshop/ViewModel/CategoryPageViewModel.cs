using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Workshop.Common;
using Workshop.Control;
using Workshop.Core.DataBase;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Helper;
using Workshop.Infrastructure.Core;
using Workshop.Infrastructure.Helper;
using Workshop.Infrastructure.Models;
using Workshop.Infrastructure.Services;
using Workshop.Model;
using Workshop.View;
namespace Workshop.ViewModel
{
    public class CategoryPageViewModel : ViewModelBase
    {

        private string filePath;

        private readonly WorkshopDbContext _dbContext;
        public CategoryPageViewModel(WorkshopDbContext dbContext)
        {
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.CreateCommand = new RelayCommand(CreateAction);
            this.RemoveCommand = new RelayCommand<Employee>(RemoveAction);
            this.EditCommand = new RelayCommand<Employee>(EditAction);
            this.PropertyChanged += CategoryPageViewModel_PropertyChanged;
            this._dbContext = dbContext;
            InitData();

        }

        private void InitData()
        {
            IList<Employee> data = null;

            var task = InvokeHelper.InvokeOnUi<IList<Employee>>(null, () =>
        {
            var result = this._dbContext.Employee.Where(c => true)
                .Include(c => c.EmployeeAccount)
                .Include(c => c.EmployeeSalay)
                .Include(c => c.EmployeeSocialInsuranceAndFund)
                .Include(c => c.EnterpriseSocialInsuranceAndFund)
                .Include(c => c.EmployeeSocialInsuranceDetail)

                .ToList();
            return result;


        }, (t) =>
             {
                 data = t;
                 try
                 {
                     this.EmployeeInfos = new ObservableCollection<Employee>(data);
                     this.EmployeeInfos.CollectionChanged += CategoryInfos_CollectionChanged;


                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e);
                     throw;
                 }
             });


        }

        private void CategoryPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.EmployeeInfos))
            {
                SubmitCommand.RaiseCanExecuteChanged();

            }
        }

        private void CategoryInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            SubmitCommand.RaiseCanExecuteChanged();
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                var result = 0;
                _dbContext.Employee.Update(e.NewItems[0] as Employee);
                result = _dbContext.SaveChanges();
                if (result == 0)
                {
                    MessageBox.Show("更新失败");

                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var result = 0;
                _dbContext.Employee.Remove(e.OldItems[0] as Employee);
                result = _dbContext.SaveChanges();
                if (result == 0)
                {
                    MessageBox.Show("删除失败");

                }


            }


        }

        private void RemoveAction(Employee obj)
        {
            if (obj == null)
            {
                return;

            }
            RemoveCategory(obj);
        }


        internal void RemoveCategory(Employee CategoryInfo)
        {
            if (EmployeeInfos.Any(c => c.Id == CategoryInfo.Id))
            {
                var current = EmployeeInfos.FirstOrDefault(c => c.Id == CategoryInfo.Id);
                EmployeeInfos.RemoveAt(EmployeeInfos.IndexOf(current));
            }
            else
            {
                MessageBox.Show("条目不存在");

            }
        }

        private void EditAction(Employee obj)
        {
            if (obj == null)
            {
                return;

            }
            var childvm = SimpleIoc.Default.GetInstance<CreateCategoryViewModel>();
            childvm.CurrentEmployee = obj;

            var cpwindow = new CreateCategoryWindow();
            cpwindow.ShowDialog();

        }



        private void CreateAction()
        {
            var cpwindow = new CreateCategoryWindow();
            cpwindow.ShowDialog();
        }

        private void SubmitAction()
        {
            var odInfos = EmployeeInfos.ToList();



            if (odInfos.Count > 0)
            {

                var defaultFontName = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontName"];
                var defaultFontColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontColor"];
                short defaultFontSize = Convert.ToInt16(AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontSize"]);
                var defaultBorderColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultBorderColor"];
                var defaultBackColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultBackColor"];

                var employeeEntitys = this.EmployeeInfos.Select(c => new EmployeeEntity
                {
                    Year = c.Year,
                    Mounth = c.Mounth,
                    Batch = c.Batch,
                    SerialNum = c.SerialNum,
                    Dept = c.Dept,
                    Proj = c.Proj,
                    State = c.State,
                    Name = new StyledType<string>(c.Name),
                    IDCard = new StyledType<string>(c.IDCard),
                    Level = c.Level,
                    JobCate = c.JobCate,
                    AccountNum = c.EmployeeAccount.AccountNum,
                    AccountBankAlias = c.EmployeeAccount.AccountBankAlias,
                    AccountBankName = new FormulatedType<string>(c.EmployeeAccount.AccountBankName),
                    AccountBankLoc = new FormulatedType<string>(c.EmployeeAccount.AccountBankLoc),
                    SocialInsuranceNum = c.EmployeeAccount.SocialInsuranceNum,
                    ProbationSalary = c.EmployeeSalay.ProbationSalary,
                    BasicSalary = c.EmployeeSalay.BasicSalary,
                    SkillSalary = c.EmployeeSalay.SkillSalary,
                    PerformanceBonus = c.EmployeeSalay.PerformanceBonus,
                    PostAllowance = c.EmployeeSalay.PostAllowance,
                    OtherAllowances = c.EmployeeSalay.OtherAllowances,
                    SalesBonus = c.EmployeeSalay.SalesBonus,
                    Bonus1 = c.EmployeeSalay.Bonus1,
                    Bonus2 = c.EmployeeSalay.Bonus2,
                    PerformanceRewards = c.EmployeeSalay.PerformanceRewards,
                    PerformanceDeduct = c.EmployeeSalay.PerformanceDeduct,
                    NightAllowances = c.EmployeeSalay.NightAllowances,
                    OrdinaryOvertime = c.EmployeeSalay.OrdinaryOvertime,
                    HolidayOvertime = c.EmployeeSalay.HolidayOvertime,
                    AgeBonus = c.EmployeeSalay.AgeBonus,
                    AttendanceDeduct = c.EmployeeSalay.AttendanceDeduct,
                    MonthlyAttendance = c.EmployeeSalay.MonthlyAttendance,
                    QuarterlyAttendance = c.EmployeeSalay.QuarterlyAttendance,
                    OtherRewards = c.EmployeeSalay.OtherRewards,
                    OtherDeduct = c.EmployeeSalay.OtherDeduct,
                    SupplementaryRewards = c.EmployeeSalay.SupplementaryRewards,
                    SupplementaryDeduct = c.EmployeeSalay.SupplementaryDeduct,
                    ParttimeSalary = c.EmployeeSalay.ParttimeSalary,
                    Sum = new FullAdvancedType<double>(c.EmployeeSalay.Sum),
                    HostelAllowances = c.EmployeeSalay.HostelAllowances,
                    MealAllowances = c.EmployeeSalay.MealAllowances,
                    TemporaryTax = c.EmployeeSalay.TemporaryTax,
                    SumWithTemporaryTax = new FullAdvancedType<double>(c.EmployeeSalay.SumWithTemporaryTax),
                    SocialInsurancePersonal = c.EmployeeSocialInsuranceAndFund.SocialInsurancePersonal,
                    SupplementarySocialInsurancePersonal =
                        c.EmployeeSocialInsuranceAndFund.SupplementarySocialInsurancePersonal,
                    ProvidentFundPersonal = c.EmployeeSocialInsuranceAndFund.ProvidentFundPersonal,
                    SupplementaryProvidentFundPersonal =
                        c.EmployeeSocialInsuranceAndFund.SupplementaryProvidentFundPersonal,
                    BeforeFillingOut = c.EmployeeSocialInsuranceAndFund.BeforeFillingOut,
                    PersonalIncomeTax = c.EmployeeSocialInsuranceAndFund.PersonalIncomeTax,
                    UnionFeePersonal = c.EmployeeSocialInsuranceAndFund.UnionFeePersonal,
                    SupplementaryUnionFeeDeductPersonal =
                        c.EmployeeSocialInsuranceAndFund.SupplementaryUnionFeeDeductPersonal,
                    SupplementaryCommercialInsuranceDeduct =
                        c.EmployeeSocialInsuranceAndFund.SupplementaryCommercialInsuranceDeduct,
                    Sum1 = new FullAdvancedType<double>(c.EmployeeSocialInsuranceAndFund.Sum),
                    SocialInsuranceEnterprise = c.EnterpriseSocialInsuranceAndFund.SocialInsuranceEnterprise,
                    SupplementarySocialInsuranceEnterprise =
                        c.EnterpriseSocialInsuranceAndFund.SupplementarySocialInsuranceEnterprise,
                    ProvidentFundEnterprise = c.EnterpriseSocialInsuranceAndFund.ProvidentFundEnterprise,
                    SupplementaryProvidentFundEnterprise =
                        c.EnterpriseSocialInsuranceAndFund.SupplementaryProvidentFundEnterprise,
                    UnionFeeEnterprise = c.EnterpriseSocialInsuranceAndFund.UnionFeeEnterprise,
                    SupplementaryUnionFeeDeductEnterprise =
                        c.EnterpriseSocialInsuranceAndFund.SupplementaryUnionFeeDeductEnterprise,
                    Sum2 = new FullAdvancedType<double>(c.EnterpriseSocialInsuranceAndFund.Sum),
                    BasicOldAgeInsurance = c.EmployeeSocialInsuranceDetail.BasicOldAgeInsurance,
                    BasicMedicalInsurance = c.EmployeeSocialInsuranceDetail.BasicMedicalInsurance,
                    UnemploymentInsurance = c.EmployeeSocialInsuranceDetail.UnemploymentInsurance,
                    Check = new FullAdvancedType<double>(c.EmployeeSocialInsuranceDetail.Check),
                    ProvidentFund = new FullAdvancedType<double>(c.EmployeeSocialInsuranceDetail.ProvidentFund)
                }).ToList();
                var task = InvokeHelper.InvokeOnUi<IEnumerable<EmployeeEntity>>(null, () =>
                {

                    DocHelper.SaveTo(employeeEntitys, new ExportOption(1,1){SheetName = "全职(生成器生成，需要按需修改)"});

                    return employeeEntitys;



                }, async (t) =>
                {

                });
            }

        }


        private ObservableCollection<Employee> _categoryTypeInfos;

        public ObservableCollection<Employee> EmployeeInfos
        {
            get
            {
                if (_categoryTypeInfos == null)
                {
                    _categoryTypeInfos = new ObservableCollection<Employee>();
                }
                return _categoryTypeInfos;
            }
            set
            {
                _categoryTypeInfos = value;
                RaisePropertyChanged(nameof(EmployeeInfos));
            }
        }

        public void CreateCategory(Employee model)
        {
            var id = Guid.NewGuid();
            var createtime = DateTime.Now;
            model.Id = id;
            model.CreateTime = createtime;
            if (EmployeeInfos.Any(c => c.Id == model.Id))
            {
                var current = EmployeeInfos.FirstOrDefault(c => c.Id == model.Id);
                EmployeeInfos[EmployeeInfos.IndexOf(current)] = model;
            }
            else
            {
                EmployeeInfos.Add(model);

            }
        }


        private bool CanSubmit()
        {
            return this.EmployeeInfos.Count > 0;

        }


        public RelayCommand GetDataCommand { get; set; }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand<Employee> EditCommand { get; set; }
        public RelayCommand<Employee> RemoveCommand { get; set; }

    }

}
