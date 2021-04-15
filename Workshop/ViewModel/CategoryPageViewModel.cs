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
using Workshop.Helper;
using Workshop.Model;
using Workshop.Service;
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
            this.RemoveCommand = new RelayCommand<EmployeeDto>(RemoveAction);
            this.EditCommand = new RelayCommand<EmployeeDto>(EditAction);
            InitData();
            this.PropertyChanged += CategoryPageViewModel_PropertyChanged;
            this._dbContext = dbContext;

        }

        private void InitData()
        {
            IList<EmployeeDto> data = null;

            InvokeHelper.InvokeOnUi(null, () =>
            {
                
                Thread.Sleep(2000);

            }).ContinueWith((t) =>
            {

                this.EmployeeInfos = new ObservableCollection<EmployeeDto>(data);
                this.EmployeeInfos.CollectionChanged += CategoryInfos_CollectionChanged;
            });


        }

        private void CategoryPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.EmployeeInfos))
            {
                if (this.EmployeeInfos != null && this.EmployeeInfos.Count > 0)
                {
                    LocalDataService.SaveCollectionLocal(this.EmployeeInfos);

                }
            }
        }

        private void CategoryInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            SubmitCommand.RaiseCanExecuteChanged();
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                var result = 0;
                using (var dbContext=this._dbContext)
                {
                    dbContext.Employee.Update(e.NewItems[0] as Employee);
                    result= dbContext.SaveChanges();
                }
                if (result == 0)
                {
                    MessageBox.Show("更新失败");

                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var result = 0;
                using (var dbContext = this._dbContext)
                {
                    dbContext.Employee.Remove(e.OldItems[0] as Employee);
                    result = dbContext.SaveChanges();
                }
                if (result == 0)
                {
                    MessageBox.Show("删除失败");

                }
               

            }
            using (var dbContext = this._dbContext)
            {
               var result= dbContext.Employee.ToList();
                App.GolobelCategorys = new List<Employee>(result);
            }

        }

        private void RemoveAction(EmployeeDto obj)
        {
            if (obj == null)
            {
                return;

            }
            RemoveCategory(obj);
        }


        internal void RemoveCategory(EmployeeDto CategoryInfo)
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

        private void EditAction(EmployeeDto obj)
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


                DocHelper.SaveTo(odInfos);
            }

        }


        private ObservableCollection<EmployeeDto> _categoryTypeInfos;

        public ObservableCollection<EmployeeDto> EmployeeInfos
        {
            get
            {
                if (_categoryTypeInfos == null)
                {
                    _categoryTypeInfos = new ObservableCollection<EmployeeDto>();
                }
                return _categoryTypeInfos;
            }
            set
            {
                _categoryTypeInfos = value;
                RaisePropertyChanged(nameof(EmployeeInfos));
            }
        }

        public void CreateCategory(EmployeeDto model)
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
        public RelayCommand<EmployeeDto> EditCommand { get; set; }
        public RelayCommand<EmployeeDto> RemoveCommand { get; set; }

    }

}
