using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using ExcelPatternTool.Common;
using ExcelPatternTool.Control;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Model;
using ExcelPatternTool.View;

namespace ExcelPatternTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitEnvironment();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //InvokeHelper.InvokeOnUi("正在检查网络", () =>
            //{
            //    var SettingInfo = LocalDataHelper.ReadObjectLocal<SettingInfo>();
            //    if (SettingInfo == null)
            //    {
            //        SettingInfo = new SettingInfo()
            //        {
            //            Addr = "172.16.65.22"
            //        };
            //        LocalDataHelper.SaveObjectLocal(SettingInfo);

            //    }

            //    Thread.Sleep(2000);
            //});

        }

        private void InitEnvironment()
        {
            var currentVersion = 0.23;

            var currentAppInfo = new ApplicationInfo()
            {
                Version = currentVersion,
                RealeaseDate = DateTime.Now.Date
            };
            var appInfo = LocalDataHelper.ReadObjectLocal<ApplicationInfo>();
            if (appInfo == null)
            {
                LocalDataHelper.InitLocalPath();
                LocalDataHelper.SaveObjectLocal(currentAppInfo);
            }
            else
            {
                if (appInfo.Version < currentVersion)
                {
                    LocalDataHelper.InitLocalPath();
                    LocalDataHelper.SaveObjectLocal(currentAppInfo);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new Exception("测试！");
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            var control = (sender as Frame).NavigationService.RemoveBackEntry();

        }
    }
}
