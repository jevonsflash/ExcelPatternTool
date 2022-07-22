using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

using Workshop.Common;
using Workshop.Core.Domains;
using Workshop.Helper;
using Workshop.Infrastructure.Helper;
using Workshop.Model;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.DataBase;
using Workshop.ViewModel;
using Workshop.Core.Validators;
using CommunityToolkit.Mvvm.Messaging;

namespace Workshop
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static List<Employee> GolobelCategorys;
        public static string Session;
        private bool _initialized;
        public App()
        {
            if (!_initialized)
            {
                _initialized = true;

                var connectionString = @"Data Source=mato.db";
                var contextOptions = new DbContextOptionsBuilder<WorkshopDbContext>()
                    .UseSqlite(connectionString)
                    .Options;


                Ioc.Default.ConfigureServices(
                    new ServiceCollection()

            .AddSingleton<MainViewModel>()
            .AddSingleton<IndexPageViewModel>()
            .AddSingleton<ImportPageViewModel>()
            .AddSingleton<CreateCategoryViewModel>()
            .AddSingleton<CategoryPageViewModel>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<SettingPageViewModel>()
            .AddSingleton<ToolsPageViewModel>()

            .AddSingleton<WorkshopDbContext>((c) => new WorkshopDbContext(contextOptions))
            .AddSingleton<Validator>((c) => new Validator())
                    .BuildServiceProvider());
                App.Current.Startup += Current_Startup;
                App.Current.Exit += Current_Exit;
            }
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
                WeakReferenceMessenger.Default.Send("", MessengerToken.CLOSEPROGRESS);

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
                WeakReferenceMessenger.Default.Send("", MessengerToken.CLOSEPROGRESS);

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
