using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Workshop.Common;
using Workshop.ViewModel;

namespace Workshop.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            Loaded += LoginWindow_Loaded;
            Messenger.Default.Register<String>(this, MessengerToken.CLOSEWINDOW, HandleMessage);

        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(App.Session))
            {
                var result = MessageBox.Show("已经登录，是否注销？", "已登录", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Cancel)
                {
                    this.Close();
                }
                else
                {
                    App.Session = string.Empty;
                }
            }
        }

        private void HandleMessage(string obj)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as LoginViewModel;
            var pwd = this.MainPasswordBox.Password;
            if (string.IsNullOrEmpty(pwd))
            {
                return;
            }
            vm.LoginCommand.Execute(pwd);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(bool)this.IsEmployeeCheckBox.IsChecked)
            {
                var vm = this.DataContext as LoginViewModel;
                vm.CurrentSeat = null;
            }
        }
    }
}
