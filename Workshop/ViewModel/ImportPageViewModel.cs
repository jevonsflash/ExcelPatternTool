using GalaSoft.MvvmLight;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Workshop.Core.Domains;
using Workshop.Core.Entites;
using Workshop.Model;
using Workshop.Service;
using Workshop.Model.Excel;
using Workshop.Helper;
using Workshop.Infrastructure.Models;

namespace Workshop.ViewModel
{
    public class ImportPageViewModel : ViewModelBase
    {
        public ImportPageViewModel()
        {
            this.ImportCommand = new RelayCommand(ImportAction, CanSubmit);
            this.SubmitCommand = new RelayCommand(SubmitAction, CanSubmit);

            this.GetDataCommand = new RelayCommand(GetDataAction, CanSubmit);
            this.Employees = new ObservableCollection<dynamic>();
        }

        private void GetDataAction()
        {
            foreach (var item in this.Employees)
            {
                //var currentOrderTables = DataService.GetOrderListBySn(item.SerialNumber);
                //if (currentOrderTables == null)
                //{
                //    MessageBox.Show("无作业单信息");
                //    return;
                //}
                //if (false)
                //{
                //    MessageBox.Show(string.Format("无{0}序列号对应的作业单信息", item.SerialNumber));
                //    return;
                //}
                //try
                //{
                //    var orderInfoTable = DataService.GetEmployee(currentOrderTable.job).FirstOrDefault();
                //    if (orderInfoTable == null)
                //    {
                //        MessageBox.Show(string.Format("无{0}作业单号对应的作业单详细内容", currentOrderTable.job));
                //        return;
                //    }
                //    item.OrderNumber = currentOrderTable.job;
                //    item.ProductModelNumber = orderInfoTable.productTypeID;
                //    item.ProductDetail = orderInfoTable.productTypeDesc;
                //    item.Note = orderInfoTable.note;
                //}
                //catch (Exception ex)
                //{

                //    MessageBox.Show(string.Format("获取{0}作业单号对应的作业单详细内容时发生未经处理的异常:{1}", currentOrderTable.job, ex.Message));
                //    continue;

                //}
            }

            //var aa = DataService.GetOrderListBySn("aa1820000774");
            //var bb = DataService.GetEmployee("C000300041478");
        }

        private void SubmitAction()
        {
            var odInfos = Employees.ToList();



            if (odInfos.Count > 0)
            {


                
            }

        }

        private void ImportAction()
        {
            var result = DocHelper.ImportFromDelegator((importer) =>
            {

                var op1 = new ImportOption<EmpoyeeImportEntity>(0, 2);
                op1.SheetName = "全职";
                var r1 = importer.Process<EmpoyeeImportEntity>(op1);


                return new { Employees = r1 };

            });
            this.Employees = new ObservableCollection<dynamic>(result);
        }

        private ObservableCollection<dynamic> _employees;

        public ObservableCollection<dynamic> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                RaisePropertyChanged(nameof(Employees));
            }
        }
        private bool CanSubmit()
        {
            //return !string.IsNullOrEmpty(this.CurrentContent);
            //return this.ContentList.Count > 0;
            return true;
        }




        public RelayCommand GetDataCommand { get; set; }

        public RelayCommand ImportCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
    }
}
