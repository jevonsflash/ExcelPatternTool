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
using Workshop.Core.Domains;
using Workshop.Model;
using Workshop.ViewModel;

namespace Workshop.View
{
    /// <summary>
    /// ProcedurePage.xaml 的交互逻辑
    /// </summary>
    public partial class CategoryPage : Page
    {
        public CategoryPage()
        {
            InitializeComponent();
        }
        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as FrameworkElement;
            var vm = this.DataContext as CategoryPageViewModel;
            vm.EditCommand.Execute(item.DataContext as Employee);
        }

        private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as FrameworkElement;
            var vm = this.DataContext as CategoryPageViewModel;
            vm.RemoveCommand.Execute(item.DataContext as Employee);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Cleanup<CategoryPageViewModel>();

        }
        private void DataGrid_OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (string.IsNullOrEmpty(((PropertyDescriptor)e.PropertyDescriptor).DisplayName))
            {
                e.Cancel = true;
            }
            e.Column.Header = ((PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }
    }
}
