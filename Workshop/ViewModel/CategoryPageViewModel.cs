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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Workshop.Common;
using Workshop.Control;
using Workshop.Core.DataBase;

using Workshop.Core.Excel.Services;
using Workshop.Core.Helper;
using Workshop.Core;
using Workshop.Model;
using Workshop.View;
using Workshop.Helper;
using Workshop.Core.Excel.Models;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.ViewModel
{
    public class CategoryPageViewModel : ObservableObject
    {

        private string filePath;

        private readonly WorkshopDbContext _dbContext;
        public CategoryPageViewModel(WorkshopDbContext dbContext)
        {
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.ClearCommand = new RelayCommand(ClearAction);
            this.RemoveCommand = new RelayCommand<IExcelEntity>(RemoveAction);
            this.PropertyChanged += CategoryPageViewModel_PropertyChanged;
            this._dbContext = dbContext;
            InitData();

        }

        private void ClearAction()
        {
            this.EmployeeInfos.Clear();
            MessageBox.Show("清空成功");
        }

        private void InitData()
        {
            IList<IExcelEntity> data = null;

            var task = InvokeHelper.InvokeOnUi<IList<IExcelEntity>>(null, () =>
        {
            var result = new List<IExcelEntity>();
            return result;


        }, (t) =>
             {
                 data = t;
                 try
                 {
                     this.EmployeeInfos = new ObservableCollection<IExcelEntity>(data);
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
                SubmitCommand.NotifyCanExecuteChanged();

            }
        }

        private void CategoryInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void RemoveAction(IExcelEntity obj)
        {
            if (obj == null)
            {
                return;

            }
            RemoveCategory(obj);
        }


        internal void RemoveCategory(IExcelEntity CategoryInfo)
        {
            if (EmployeeInfos.Any(c => c.RowNumber == CategoryInfo.RowNumber))
            {
                var current = EmployeeInfos.FirstOrDefault(c => c.RowNumber == CategoryInfo.RowNumber);
                EmployeeInfos.RemoveAt(EmployeeInfos.IndexOf(current));
            }
            else
            {
                MessageBox.Show("条目不存在");

            }
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

                var task = InvokeHelper.InvokeOnUi<IEnumerable<IExcelEntity>>(null, () =>
                {

                    DocHelper.SaveTo(this.EmployeeInfos, new ExportOption(1, 1) { SheetName = "全职(生成器生成，请按需修改)", GenHeaderRow = true });

                    return this.EmployeeInfos;



                }, async (t) =>
                {
                    var result = this._dbContext.SaveChanges();
                    MessageBox.Show("已完成导出");

                });
            }

        }


        private ObservableCollection<IExcelEntity> _categoryTypeInfos;

        public ObservableCollection<IExcelEntity> EmployeeInfos
        {
            get
            {
                if (_categoryTypeInfos == null)
                {
                    _categoryTypeInfos = new ObservableCollection<IExcelEntity>();
                }
                return _categoryTypeInfos;
            }
            set
            {
                _categoryTypeInfos = value;
                OnPropertyChanged(nameof(EmployeeInfos));
            }
        }


        private bool CanSubmit()
        {
            return this.EmployeeInfos.Count > 0;

        }


        public RelayCommand GetDataCommand { get; set; }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand<IExcelEntity> RemoveCommand { get; set; }

    }

}
