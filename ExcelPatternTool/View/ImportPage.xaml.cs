using CommunityToolkit.Mvvm.DependencyInjection;
using ExcelPatternTool.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelPatternTool.View
{
    /// <summary>
    /// ImportPage.xaml 的交互逻辑
    /// </summary>
    public partial class ImportPage : Page
    {
        public ImportPage()
        {
            InitializeComponent();
            Ioc.Default.GetRequiredService<ImportPageViewModel>().OnFinished+=ImportPage_OnFinished;
        }

        private void ImportPage_OnFinished(object sender, EventArgs e)
        {
            (App.Current.MainWindow as MainWindow).HamburgerMenuControl.SelectedIndex=0;
        }

        private void CurrentText_OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DataGrid_OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }

    }
}
