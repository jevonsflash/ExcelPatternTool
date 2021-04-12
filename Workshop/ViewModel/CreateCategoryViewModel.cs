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
            this.CurrentCategoryInfo = new CategoryInfo();
            this.SubmitCommand = new RelayCommand(SubmitAction);
            this.RemoveCommand = new RelayCommand(RemoveAction, CanSubmit);
            this.PropertyChanged += CreateProductPageViewModel_PropertyChanged;

        }

        private void CreateProductPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CurrentCategoryInfo))
            {
                this.SubmitCommand.RaiseCanExecuteChanged();
                this.RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private void RemoveAction()
        {
            basevm.RemoveCategory(this.CurrentCategoryInfo);
            this.CurrentCategoryInfo = new CategoryInfo();

        }

        private void SubmitAction()
        {
            basevm.CreateCategory(this.CurrentCategoryInfo);
            this.CurrentCategoryInfo = new CategoryInfo();

        }

        private bool CanSubmit()
        {
            return this.CurrentCategoryInfo != null && !string.IsNullOrEmpty(CurrentCategoryInfo.Id);
        }

        private CategoryInfo _currentCategoryInfo;

        public CategoryInfo CurrentCategoryInfo
        {
            get { return _currentCategoryInfo; }
            set
            {
                _currentCategoryInfo = value;
                RaisePropertyChanged(nameof(CurrentCategoryInfo));
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public CategoryPageViewModel basevm => SimpleIoc.Default.GetInstance<CategoryPageViewModel>();
    }
}
