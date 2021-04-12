using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model
{
    public class SettingInfo : ViewModelBase
    {
        private string _addr;

        public string Addr
        {
            get { return _addr; }
            set
            {
                _addr = value;
                RaisePropertyChanged(nameof(Addr));
            }
        }

        private string _port;

        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                RaisePropertyChanged(nameof(Port));

            }
        }

    }
}
