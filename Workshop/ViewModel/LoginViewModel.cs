using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
using Workshop.Model;
using Workshop.Model.Apis;
using Workshop.Service;

namespace Workshop.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            UserName = "tony";
            LoginCommand = new RelayCommand<string>(LoginAction);
            InitData();
        }

        private void InitData()
        {
            IList<SeatList> data=null;
            InvokeHelper.InvokeOnUi("正在检查网络", () =>
            {
                data = WebDataService.GetSeatList();


                Thread.Sleep(2000);
            }).ContinueWith((t) =>
            {

                this.SeatList = new List<Model.Apis.SeatList>(data);
            }); 

        }


        private void LoginAction(string obj)
        {
            var seatId = string.Empty;
            if (CurrentSeat != null)
            {
                seatId = CurrentSeat.SeatId;
            }
            var result = WebDataService.Login(this.UserName, obj, seatId);
            if (string.IsNullOrEmpty(result.Session) || result.Errorno != 0)
            {
                MessageBox.Show("登录失败");
            }
            else
            {
                Messenger.Default.Send("", MessengerToken.CLOSEWINDOW);
                App.Session = result.Session;
            }
        }

        private SeatList _currentSeat;

        public SeatList CurrentSeat
        {
            get { return _currentSeat; }
            set { _currentSeat = value; }
        }


        private List<SeatList> _seatList;

        public List<SeatList> SeatList
        {
            get { return _seatList; }
            set
            {
                _seatList = value;
                RaisePropertyChanged(nameof(SeatList));
            }
        }



        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(nameof(UserName));
            }
        }

        public RelayCommand<string> LoginCommand { get; set; }



    }


}
