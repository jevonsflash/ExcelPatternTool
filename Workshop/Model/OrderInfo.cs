using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Workshop.Model
{
    //ID 作业单 序列号 产品型号    产品描述 创建时间

    public class OrderInfo: ObservableObject
    {
        [DisplayName("我是认真的")]
        public string UserName { get; set; }
        public string ProcedureName { get; set; }
        public string SerialNumber { get; set; }

        private string _orderNumber;
        public string OrderNumber
        {
            get { return _orderNumber; }
            set
            {
                _orderNumber = value;
                OnPropertyChanged(nameof(OrderNumber));
            }
        }


        private string _productModelNumber;
        public string ProductModelNumber
        {
            get { return _productModelNumber; }
            set
            {
                _productModelNumber = value;
                OnPropertyChanged(nameof(ProductModelNumber));
            }
        }

        private string _productDetail;
        public string ProductDetail
        {
            get { return _productDetail; }
            set
            {
                _productDetail = value;
                OnPropertyChanged(nameof(ProductDetail));
            }
        }



        private string _note;
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
    }
}
