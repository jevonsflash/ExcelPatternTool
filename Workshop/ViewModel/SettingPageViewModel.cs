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
using Workshop.Model;
using Workshop.Core.Validators;
using Workshop.Core.Helper;
using Workshop.Model.JsonEditor;
using NJsonSchema;
using Workshop.Helper;
using Workshop.Core.Patterns;
using System.Windows;
using NJsonSchema.Generation;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Workshop.ViewModel
{
    public class SettingPageViewModel : ObservableObject
    {

        //const string local = "local_";
        public SettingPageViewModel()
        {


            this.SubmitCommand = new RelayCommand(SubmitAction, () => true);
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

            if (!LocalDataHelper.IsExist<PatternSchema>())
            {
                var settings = new JsonSchemaGeneratorSettings();
                settings.AllowReferencesWithProperties = false;
                settings.DefaultReferenceTypeNullHandling=ReferenceTypeNullHandling.NotNull;
                var generator = new JsonSchemaGenerator(settings);
                var schema = generator.Generate(typeof(Pattern));
                DirFileHelper.CreateFile(LocalDataHelper.GetPath<PatternSchema>(), schema.ToJson());
            }


            var schemaPropertyPath = LocalDataHelper.GetPath<PatternSchema>();
            JsonDocumentModel data = null;
            if (!LocalDataHelper.IsExist<Pattern>())
            {
                LocalDataHelper.SaveObjectLocal<Pattern>(new Pattern());
                var filePath = LocalDataHelper.GetPath<Pattern>();

                data = await JsonDocumentModel.LoadAsync(filePath, schemaPropertyPath);


            }
            else
            {
                var filePath = LocalDataHelper.GetPath<Pattern>();

                data = await JsonDocumentModel.LoadAsync(filePath, schemaPropertyPath);

            }

            this.Document = data;
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
            SaveDocument(this.Document, false);

            Ioc.Default.GetRequiredService<Validator>().ValidatorInfos=null;
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
