using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelPatternTool.Model
{
    public class SettingInfo : ObservableObject
    {
        private string _addr;

        public string Addr
        {
            get { return _addr; }
            set
            {
                _addr = value;
                OnPropertyChanged(nameof(Addr));
            }
        }

        private string _port;

        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(nameof(Port));

            }
        }

    }
}
