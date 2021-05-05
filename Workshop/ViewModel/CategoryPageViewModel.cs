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
using Workshop.Infrastructure.Helper;
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
            var result = this._dbContext.Employee.Where(c => true).ToList();
            return result;


        }, (t) =>
             {
                 data = t;
                 this.EmployeeInfos = new ObservableCollection<Employee>(data);
                 this.EmployeeInfos.CollectionChanged += CategoryInfos_CollectionChanged;
             });


        }

        private void CategoryPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.EmployeeInfos))
            {
                if (this.EmployeeInfos != null && this.EmployeeInfos.Count > 0)
                {
                    LocalDataHelper.SaveCollectionLocal(this.EmployeeInfos);

                }
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


                //DocHelper.SaveTo(odInfos);
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
