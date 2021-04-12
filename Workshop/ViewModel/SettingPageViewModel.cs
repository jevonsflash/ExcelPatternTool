using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Workshop.Model;
using Workshop.Service;

namespace Workshop.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        public SettingPageViewModel()
        {
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.PropertyChanged += SettingPageViewModel_PropertyChanged;
            SettingInfo = LocalDataService.ReadObjectLocal<SettingInfo>();
        }

        private bool _hasChanged;

        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                _hasChanged = value;
                RaisePropertyChanged(nameof(HasChanged));
            }
        }


        private void SettingInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseSettingChanged();
        }

        public void RaiseSettingChanged()
        {
            HasChanged = true;
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private void SettingPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingInfo) && SettingInfo != null)
            {
                SettingInfo.PropertyChanged += SettingInfo_PropertyChanged;

            }
        }



        private void SubmitAction()
        {
            LocalDataService.SaveObjectLocal(SettingInfo);
            HasChanged = false;


        }

        private bool CanSubmit()
        {
            return this.SettingInfo != null && HasChanged;
        }

        private SettingInfo _settingInfo;

        public SettingInfo SettingInfo
        {
            get { return _settingInfo; }
            set
            {
                _settingInfo = value;
                RaisePropertyChanged(nameof(SettingInfo));
            }
        }

        public RelayCommand SubmitCommand { get; set; }

    }
}
