using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Model;

namespace Workshop.ViewModel
{
    public class CreateCategoryViewModel : ObservableObject
    {
        public CreateCategoryViewModel()
        {
            this.CurrentEmployeeEntity = new EmployeeEntity();
            this.SubmitCommand = new RelayCommand(SubmitAction);
            this.RemoveCommand = new RelayCommand(RemoveAction, CanSubmit);
            this.PropertyChanged += CreateProductPageViewModel_PropertyChanged;

        }

        private void CreateProductPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CurrentEmployeeEntity))
            {
                this.SubmitCommand.NotifyCanExecuteChanged();
                this.RemoveCommand.NotifyCanExecuteChanged();
            }
        }

        private void RemoveAction()
        {
            basevm.RemoveCategory(this.CurrentEmployeeEntity);
            this.CurrentEmployeeEntity = new EmployeeEntity();

        }

        private void SubmitAction()
        {
            basevm.CreateCategory(this.CurrentEmployeeEntity);
            this.CurrentEmployeeEntity = new EmployeeEntity();

        }

        private bool CanSubmit()
        {
            return this.CurrentEmployeeEntity != null && CurrentEmployeeEntity.Id!=Guid.Empty;
        }

        private EmployeeEntity _currentEmployeeEntity;

        public EmployeeEntity CurrentEmployeeEntity
        {
            get { return _currentEmployeeEntity; }
            set
            {
                _currentEmployeeEntity = value;
                OnPropertyChanged(nameof(CurrentEmployeeEntity));
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public CategoryPageViewModel basevm => Ioc.Default.GetRequiredService<CategoryPageViewModel>();
    }
}
