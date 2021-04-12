using GalaSoft.MvvmLight;
using Microsoft.Win32;
using SimpleExcelImport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Workshop.Model;
using Workshop.Service;
using Workshop.Model.Excel;
using Workshop.Model.Tables;
using Workshop.Helper;

namespace Workshop.ViewModel
{
    public class ImportPageViewModel : ViewModelBase
    {
        private string filePath;
        public IList<OrderTable> _currentOrderTables;

        public ImportPageViewModel()
        {
            this.ImportCommand = new RelayCommand(ImportAction, CanSubmit);
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);

            this.GetDataCommand = new RelayCommand(GetDataAction, CanSubmit);
            _currentOrderTables = new List<OrderTable>();
            this.OrderInfos=new ObservableCollection<OrderInfo>();
        }

        private void GetDataAction()
        {
            foreach (var item in this.OrderInfos)
            {
                var currentOrderTables = DataService.GetOrderListBySn(item.SerialNumber);
                if (currentOrderTables == null)
                {
                    MessageBox.Show("无作业单信息");
                    return;
                }
                _currentOrderTables = currentOrderTables;
                var currentOrderTable = _currentOrderTables.FirstOrDefault(c => c.productID == item.SerialNumber);
                if (currentOrderTable == null)
                {
                    MessageBox.Show(string.Format("无{0}序列号对应的作业单信息", item.SerialNumber));
                    return;
                }
                try
                {
                    var orderInfoTable = DataService.GetOrderInfo(currentOrderTable.job).FirstOrDefault();
                    if (orderInfoTable == null)
                    {
                        MessageBox.Show(string.Format("无{0}作业单号对应的作业单详细内容", currentOrderTable.job));
                        return;
                    }
                    item.OrderNumber = currentOrderTable.job;
                    item.ProductModelNumber = orderInfoTable.productTypeID;
                    item.ProductDetail = orderInfoTable.productTypeDesc;
                    item.Note = orderInfoTable.note;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(string.Format("获取{0}作业单号对应的作业单详细内容时发生未经处理的异常:{1}", currentOrderTable.job, ex.Message));
                    continue;

                }
            }

            //var aa = DataService.GetOrderListBySn("aa1820000774");
            //var bb = DataService.GetOrderInfo("C000300041478");
        }

        private void SubmitAction()
        {
            var odInfos = OrderInfos.ToList();



            if (odInfos.Count > 0)
            {


                DocHelper.SaveTo(odInfos);
            }

        }

        private void ImportAction()
        {
            var result = GetData();
            this.OrderInfos = new ObservableCollection<OrderInfo>(result);
        }

        private ObservableCollection<OrderInfo> _orderInfos;

        public ObservableCollection<OrderInfo> OrderInfos
        {
            get { return _orderInfos; }
            set
            {
                _orderInfos = value;
                RaisePropertyChanged(nameof(OrderInfos));
            }
        }
        private bool CanSubmit()
        {
            //return !string.IsNullOrEmpty(this.CurrentContent);
            //return this.ContentList.Count > 0;
            return true;
        }


        public IList<OrderInfo> GetData()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = CommonHelper.DesktopPath;
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xls";
            openFileDialog.FileName = "RootElements";
            openFileDialog.AddExtension = true;
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();
            ImportFromExcel import = new ImportFromExcel();
            List<OrderInfo> output = new List<OrderInfo>();
            if (result == true)
            {
                this.filePath = openFileDialog.FileName;
            }
            else
            {
                return output;

            }
            var data1 = new byte[0];
            try
            {
                data1 = File.ReadAllBytes(filePath);

            }
            catch (Exception e)
            {
                MessageBox.Show(filePath + " 此文件正被其他程序占用");
                return output;

            }
            try
            {
                if (filePath.EndsWith(".xlsx"))
                {
                    import.LoadXlsx(data1);
                }
                else if (filePath.EndsWith(".xls"))
                {
                    import.LoadXls(data1);

                }
                else
                {
                    MessageBox.Show(filePath + " 文件类型错误");
                    return output;

                }
                output = import.ExcelToList<OrderInfoExcel>(0, 1).Select(c => new OrderInfo()
                {
                    Id = c.Id,
                    CreateTime = c.CreateTime,
                    UserName = c.UserName,
                    ProcedureName = c.ProcedureName,
                    SerialNumber = c.SerialNumber,
                    OrderNumber = c.OrderNumber,
                    ProductModelNumber = c.ProductModelNumber,
                    ProductDetail = c.ProductDetail,
                    Note = c.Note
                }).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(filePath + " 格式错误");

            }
            return output;
        }
        public RelayCommand GetDataCommand { get; set; }

        public RelayCommand ImportCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
    }
}
