using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Workshop.Common;
using Workshop.Control;
using Workshop.Helper;

namespace Workshop.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {
            UserName = "tony";
            LoginCommand = new RelayCommand<string>(LoginAction);
            InitData();
        }

        private void InitData()
        {
            //IList<SeatList> data=null;
            InvokeHelper.InvokeOnUi("正在检查网络", () =>
            {
                //data = WebDataService.GetSeatList();


                Thread.Sleep(2000);
            }).ContinueWith((t) =>
            {

                //this.SeatList = new List<Model.Apis.SeatList>(data);
            }); 

        }


        private void LoginAction(string obj)
        {
            var seatId = string.Empty;
           
            //var result = WebDataService.Login(this.UserName, obj, seatId);
            //if (string.IsNullOrEmpty(result.Session) || result.Errorno != 0)
            //{
            //    MessageBox.Show("登录失败");
            //}
            //else
            //{
            //    WeakReferenceMessenger.Default.Send("", MessengerToken.CLOSEWINDOW);
            //    App.Session = result.Session;
            //}
        }

        



        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public RelayCommand<string> LoginCommand { get; set; }



    }


}
