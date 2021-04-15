using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Workshop.Model;

namespace Workshop.ViewModel
{
    public class CreateCategoryViewModel : ViewModelBase
    {
        public CreateCategoryViewModel()
        {
            this.CurrentEmployee = new EmployeeDto();
            this.SubmitCommand = new RelayCommand(SubmitAction);
            this.RemoveCommand = new RelayCommand(RemoveAction, CanSubmit);
            this.PropertyChanged += CreateProductPageViewModel_PropertyChanged;

        }

        private void CreateProductPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CurrentEmployee))
            {
                this.SubmitCommand.RaiseCanExecuteChanged();
                this.RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private void RemoveAction()
        {
            basevm.RemoveCategory(this.CurrentEmployee);
            this.CurrentEmployee = new EmployeeDto();

        }

        private void SubmitAction()
        {
            basevm.CreateCategory(this.CurrentEmployee);
            this.CurrentEmployee = new EmployeeDto();

        }

        private bool CanSubmit()
        {
            return this.CurrentEmployee != null && CurrentEmployee.Id!=Guid.Empty;
        }

        private EmployeeDto _currentEmployee;

        public EmployeeDto CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                _currentEmployee = value;
                RaisePropertyChanged(nameof(CurrentEmployee));
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public CategoryPageViewModel basevm => SimpleIoc.Default.GetInstance<CategoryPageViewModel>();
    }
}
