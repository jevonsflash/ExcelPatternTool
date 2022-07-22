using CommunityToolkit.Mvvm.Messaging;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Workshop.Control
{
    /// <summary>
    /// ProgressWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressWindow : MetroWindow
    {
        public static ProgressWindow Instance;
        static ProgressWindow()
        {
        }

        public static void StaticShowDialog(string title)
        {
            Instance = new ProgressWindow();
            Instance.ShowDialog(title);
        }

        public static void StaticUnShowDialog()
        {
            if (Instance!=null)
            {
                Instance.Close();
                Instance = null;
            }
        }

        public double CurrentVal { get; private set; }
        public double TotalVal { get; private set; }

        public event EventHandler<ProgressOncanceledEventArgs> Oncanceled;

        public ProgressWindow()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<string,string>(this,MessengerToken.UPDATEPROGRESS, HandleMessage);
            WeakReferenceMessenger.Default.Register<string,string>(this, MessengerToken.CLOSEPROGRESS, HandleClose);
            this.Unloaded += (sender, e) => WeakReferenceMessenger.Default.UnregisterAll(this);

        }


        public void Show(string title)
        {
            this.TitleLabel.Text = title;
            base.Show();
        }

        public void ShowDialog(string title)
        {
            this.TitleLabel.Text = title;
            base.ShowDialog();
        }

        private void HandleClose(object recipient, string obj)
        {
            Debug.WriteLine("ProgressWindow close by" +obj);
            this.Close();
        }

        private void HandleMessage(object recipient, string obj)
        {
            this.MainProgress.IsIndeterminate = false;
            this.CancelButton.Visibility = Visibility.Visible;
            this.ProgressTextLayout.Visibility = Visibility.Visible;
            this.TitleLabel.Text = "正在处理..";
            var splitedstr = obj.Split('|');
            CurrentVal = double.Parse(splitedstr[0]);
            TotalVal = double.Parse(splitedstr[1]);
            if (CurrentVal < TotalVal)
            {


                this.MainProgress.Value = CurrentVal;
                this.MainProgress.Maximum = TotalVal;
                this.CurrentValueLabel.Content = CurrentVal;
                this.TotalValueLabel.Content = TotalVal;

            }
            else
            {
                this.Close();
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Oncanceled?.Invoke(this, new ProgressOncanceledEventArgs()
            {
                TotalVal = this.TotalVal,
                CurrentVal = this.CurrentVal
            });
            this.Close();
        }
    }
}
