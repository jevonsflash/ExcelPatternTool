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
using MahApps.Metro.Controls;
using Workshop.ViewModel;

namespace Workshop.View
{
    /// <summary>
    /// CreateCategoryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateCategoryWindow : MetroWindow
    {
        public CreateCategoryWindow()
        {
            InitializeComponent();
            
            var vm = this.DataContext as CreateCategoryViewModel;
            if (vm.CurrentEmployeeEntity.Id!=Guid.Empty)
            {
                this.Title = TitleBlock.Text = "修改类别";
                this.CreateButton.Content = "提交修改";
                this.ContinueCreateButton.Visibility = Visibility.Collapsed;
                this.RemoveButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.Title = TitleBlock.Text = "添加类别";
                this.CreateButton.Content = "添加";
                this.ContinueCreateButton.Visibility = Visibility.Visible;
                this.RemoveButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateCategory_OnClosed(object sender, EventArgs e)
        {
            ViewModelLocator.Cleanup<CreateCategoryViewModel>();
        }
    }
}
