using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Exportable.Engines.Excel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Workshop.Helper;
using Workshop.Model;

namespace Workshop.ViewModel
{
    public class IndexPageViewModel : ViewModelBase
    {

        public IndexPageViewModel()
        {
            this.ContentList = new ObservableCollection<BatchInfo>();
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);
            this.ClearCommand = new RelayCommand(ClearAction, CanSubmit);
            this.SubmitCurrentCommand = new RelayCommand<string>(SubmitCurrentAction);
            this.PropertyChanged += IndexPageViewModel_PropertyChanged;
            this.ContentList.CollectionChanged += ContentList_CollectionChanged;
        }

        private void ClearAction()
        {
            this.ContentList.Clear();
        }

        private void ContentList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SubmitCommand.RaiseCanExecuteChanged();
        }

        private void SubmitCurrentAction(string obj)
        {
            this.CurrentContent = obj;
        }


        private bool CanSubmit()
        {
            //return !string.IsNullOrEmpty(this.CurrentContent);
            return this.ContentList.Count > 0;
            return true;
        }

        private void SubmitAction()
        {
            var productInfos = new List<ProductInfoExcel>();

            foreach (var batchInfo in ContentList)
            {
                productInfos.AddRange(batchInfo.ProductInfos);
            }

            if (productInfos.Count > 0)
            {


                SaveTo(productInfos);
            }

        }
        private void SaveTo(IList<ProductInfoExcel> src)
        {
            IExcelExportEngine engine = new ExcelExportEngine();
            engine.AddData(src);
            engine.SetFormat(ExcelVersion.XLS);
            MemoryStream memory = engine.Export();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = CommonHelper.DesktopPath;
            saveFileDialog.Filter = "所有文件(*.*)|*.*";
            saveFileDialog.FileName = "RootElements";
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;

            // Show save file dialog box
            bool? result = saveFileDialog.ShowDialog();
            //点了保存按钮进入
            if (result == true)
            {
                var aa = saveFileDialog.FileName;

                FileStream fs = new FileStream(aa, FileMode.Create);
                byte[] buff = memory.ToArray();
                fs.Write(buff, 0, buff.Length);
                fs.Close();
            }
        }

        private void IndexPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(CurrentContent))
            {
                var result = CreateProductInfos();
                if (result.IsSuccess)
                {
                    //success
                }
                else
                {
                    var msg = string.Empty;
                    switch (result.Type)
                    {
                        case ErrorType.批次号问题:
                            msg = "批次号出问题，请从新扫描";
                            break;
                        case ErrorType.编号问题:
                            msg = result.arg.ToString() + ",产品编号出问题，请从新扫描";

                            break;
                        case ErrorType.重复项:
                            msg = result.arg.ToString() + ",此批次号重复，添加失败";
                            break;
                        case ErrorType.解析失败:

                            msg = "扫描结果为空，请重新扫描";
                            break;

                    }
                    MessageBox.Show(msg);
                }

            }

        }

        private ProductResult CreateProductInfos()
        {
            if (string.IsNullOrEmpty(CurrentContent))
            {
                return new ProductResult(false);
            }
            var newlist = this.CurrentContent.Split(',');
            if (newlist.Length == 0)
            {
                return new ProductResult(false);
            }
            var productInfos = new List<ProductInfoExcel>();
            for (int i = 1; i < newlist.Length; i++)
            {
                var s = newlist[i];
                if (s.Length != 15)
                {
                    return new ProductResult(false, ErrorType.编号问题);
                }
                var currentTitle = string.Empty;

                if (i == 1)
                {
                    currentTitle = newlist[0];
                    if (currentTitle.Length != 14)
                    {
                        return new ProductResult(false, ErrorType.批次号问题);
                    }

                }
                var productInfo = new ProductInfoExcel()
                {

                    Title = currentTitle,
                    ProductNumber = s
                };

                productInfos.Add(productInfo);
            }

            var title = newlist[0];
            if (ContentList.Any(c => c.Title == title))
            {
                return new ProductResult(false, ErrorType.重复项) { arg = title };

            }
            ContentList.Add(new BatchInfo()
            {
                Title = title,
                ProductInfos = productInfos
            });
            return new ProductResult(true);
        }

        private string _currentContent;

        public string CurrentContent
        {
            get { return _currentContent; }
            set
            {
                _currentContent = value;
                RaisePropertyChanged(nameof(CurrentContent));
            }
        }

        private ObservableCollection<BatchInfo> _contentList;

        public ObservableCollection<BatchInfo> ContentList
        {
            get { return _contentList; }
            set
            {
                _contentList = value;
                RaisePropertyChanged(nameof(ContentList));
            }
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand<string> SubmitCurrentCommand { get; set; }

    }
}
