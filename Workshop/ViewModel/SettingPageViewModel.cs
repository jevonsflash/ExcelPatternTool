using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Workshop.Core.Domains;
using Workshop.Infrastructure.Helper;
using Workshop.Model;
using Workshop.Core.Validators;
using Workshop.Helper;
using Workshop.Model.JsonEditor;

namespace Workshop.ViewModel
{
    public class SettingPageViewModel : ObservableObject
    {
        public SettingPageViewModel()
        {
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.RefreshCommand = new RelayCommand(RefreshAction);
            this.PropertyChanged += SettingPageViewModel_PropertyChanged;
            SettingInfo = LocalDataHelper.ReadObjectLocal<SettingInfo>();
            this.HasChanged = true;
            Init();

        }

        private async void RefreshAction()
        {
            await Init();
        }

        private async Task Init()
        {
            const string _dirPrefix = "Data";

            //const string local = "local_";
            const string _filePrefix = "_";
            var basePath = CommonHelper.AppBasePath;
            var dirPath = Path.Combine(basePath, _dirPrefix);
            if (DirFileHelper.IsExistDirectory(dirPath))
            {
                var fileName = string.Format("{1}{0}.json", nameof(ValidatorInfo), _filePrefix);
                var filePath = Path.Combine(dirPath, fileName);
                var schemaPropertyName = string.Format("{1}{0}.schema.json", nameof(ValidatorInfo), _filePrefix);
                var schemaPropertyPath = Path.Combine(dirPath, schemaPropertyName);
                var data = await JsonDocumentModel.LoadAsync(filePath, schemaPropertyPath);
                this.Document = data;

            }


        }

        private bool _hasChanged;

        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                _hasChanged = value;
                OnPropertyChanged(nameof(HasChanged));
            }
        }

        private JsonDocumentModel _document;

        public JsonDocumentModel Document
        {
            get { return _document; }
            set
            {
                _document = value;

                OnPropertyChanged(nameof(Document));

            }
        }

        private void SettingInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseSettingChanged();
        }

        public void RaiseSettingChanged()
        {
            HasChanged = true;
            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void SettingPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingInfo) && SettingInfo != null)
            {
                SettingInfo.PropertyChanged += SettingInfo_PropertyChanged;

            }
        }

        private void SaveDocument(JsonDocumentModel document, bool saveAs)
        {
            var task = InvokeHelper.InvokeOnUi(null, async () =>
            {
                await document.SaveAsync(saveAs);
            });
        }


        private void SubmitAction()
        {
            LocalDataHelper.SaveObjectLocal(SettingInfo);
            SaveDocument(this.Document,false);
            //HasChanged = false;


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
                OnPropertyChanged(nameof(SettingInfo));
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

    }
}
