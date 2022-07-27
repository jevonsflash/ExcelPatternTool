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
using ExcelPatternTool.Common;
using ExcelPatternTool.Control;
using ExcelPatternTool.Core.DataBase;

using ExcelPatternTool.Core.Excel.Services;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core;
using ExcelPatternTool.Model;
using ExcelPatternTool.View;
using ExcelPatternTool.Helper;
using ExcelPatternTool.Core.Excel.Models;
using ExcelPatternTool.Core.Excel.Models.Interfaces;
using ExcelPatternTool.Core.EntityProxy;

namespace ExcelPatternTool.ViewModel
{
    public class CategoryPageViewModel : ObservableObject
    {

        private string filePath;

        private readonly ExcelPatternToolDbContext _dbContext;
        public CategoryPageViewModel(ExcelPatternToolDbContext dbContext)
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
            this.Entities.Clear();
            OnPropertyChanged(nameof(HasValue));
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
                     this.Entities = new ObservableCollection<object>(data);
                     this.Entities.CollectionChanged += CategoryInfos_CollectionChanged;


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
            if (e.PropertyName == nameof(this.Entities))
            {
                SubmitCommand.NotifyCanExecuteChanged();
                OnPropertyChanged(nameof(HasValue));
            }
        }

        private void CategoryInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasValue));

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
            if (Entities.Any(c => (c as IExcelEntity).RowNumber == CategoryInfo.RowNumber))
            {
                var current = Entities.FirstOrDefault(c => (c as IExcelEntity).RowNumber == CategoryInfo.RowNumber);
                Entities.RemoveAt(Entities.IndexOf(current));

            }
            else
            {
                MessageBox.Show("条目不存在");

            }
        }


        public bool HasValue => this.Entities.Count>0;


        private void SubmitAction()
        {
            var odInfos = Entities.ToList();



            if (odInfos.Count > 0)
            {

                var defaultFontName = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontName"];
                var defaultFontColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontColor"];
                short defaultFontSize = Convert.ToInt16(AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultFontSize"]);
                var defaultBorderColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultBorderColor"];
                var defaultBackColor = AppConfigurtaionService.Configuration["HeaderDefaultStyle:DefaultBackColor"];

                var task = InvokeHelper.InvokeOnUi<IEnumerable<object>>(null, () =>
                {

                    DocHelper.SaveTo(EntityProxyContainer.Current.EntityType, this.Entities, new ExportOption(1, 1) { SheetName = "Sheet1", GenHeaderRow = true });

                    return this.Entities;



                }, async (t) =>
                {
                    var result = this._dbContext.SaveChanges();
                    MessageBox.Show("已完成导出");

                });
            }

        }


        private ObservableCollection<object> _categoryTypeInfos;

        public ObservableCollection<object> Entities
        {
            get
            {
                if (_categoryTypeInfos == null)
                {
                    _categoryTypeInfos = new ObservableCollection<object>();
                }
                return _categoryTypeInfos;
            }
            set
            {
                _categoryTypeInfos = value;
                OnPropertyChanged(nameof(Entities));
            }
        }


        private bool CanSubmit()
        {
            return this.Entities.Count > 0;

        }


        public RelayCommand GetDataCommand { get; set; }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand<IExcelEntity> RemoveCommand { get; set; }

    }

}
