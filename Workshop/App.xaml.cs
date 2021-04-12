using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using Workshop.Common;
using Workshop.Helper;
using Workshop.Model;
using Workshop.Model.Apis;
using Workshop.Service;

namespace Workshop
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static List<CategoryInfo> GolobelCategorys;
        public static string Session;
        public App()
        {
            App.Current.Startup += Current_Startup;
            App.Current.Exit += Current_Exit;

        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            LogHelper.ExitThread();

        }

        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Messenger.Default.Send("", MessengerToken.CLOSEPROGRESS);

                LogHelper.LogError("UI线程全局异常" + e.Exception);
                MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogError("不可恢复的UI线程全局异常" + ex);
                MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "不可恢复的UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Messenger.Default.Send("", MessengerToken.CLOSEPROGRESS);

                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    LogHelper.LogError("非UI线程全局异常" + exception);
                    MessageBox.Show("An unhandled exception just occurred: " + exception.Message, "非UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("不可恢复的非UI线程全局异常" + ex);
                MessageBox.Show("An unhandled exception just occurred: " + ex.Message, "不可恢复的非UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void Current_Startup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LogHelper.LogFlag = true;

        }
    }
}
