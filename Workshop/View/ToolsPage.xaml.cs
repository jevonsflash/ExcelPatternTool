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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Workshop.ViewModel;

namespace Workshop.View
{
    /// <summary>
    /// ToolsPage.xaml 的交互逻辑
    /// </summary>
    public partial class ToolsPage : Page
    {
        public ToolsPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //var source = (new Uri("pack://application:,,,/View/PackupPage.xaml", UriKind.Absolute));

            //NavigationService.GetNavigationService(this).Navigate(source);


        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            var source = (new Uri("pack://application:,,,/View/ImportPage.xaml", UriKind.Absolute));

            NavigationService.GetNavigationService(this).Navigate(source);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Cleanup<ToolsPageViewModel>();
        }
    }
}
