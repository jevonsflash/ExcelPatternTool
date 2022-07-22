using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using Workshop.Core.Domains;
using Workshop.Model;

namespace Workshop.ViewModel
{
    public class CreateCategoryViewModel : ObservableObject
    {
        public CreateCategoryViewModel()
        {
            this.CurrentEmployee = new Employee();
            this.SubmitCommand = new RelayCommand(SubmitAction);
            this.RemoveCommand = new RelayCommand(RemoveAction, CanSubmit);
            this.PropertyChanged += CreateProductPageViewModel_PropertyChanged;

        }

        private void CreateProductPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CurrentEmployee))
            {
                this.SubmitCommand.NotifyCanExecuteChanged();
                this.RemoveCommand.NotifyCanExecuteChanged();
            }
        }

        private void RemoveAction()
        {
            basevm.RemoveCategory(this.CurrentEmployee);
            this.CurrentEmployee = new Employee();

        }

        private void SubmitAction()
        {
            basevm.CreateCategory(this.CurrentEmployee);
            this.CurrentEmployee = new Employee();

        }

        private bool CanSubmit()
        {
            return this.CurrentEmployee != null && CurrentEmployee.Id!=Guid.Empty;
        }

        private Employee _currentEmployee;

        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                _currentEmployee = value;
                OnPropertyChanged(nameof(CurrentEmployee));
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public CategoryPageViewModel basevm => Ioc.Default.GetRequiredService<CategoryPageViewModel>();
    }
}
