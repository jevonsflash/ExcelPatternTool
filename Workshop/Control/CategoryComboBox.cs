using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Workshop.Core.Helper;
using Workshop.Model;

namespace Workshop.Control
{
    public  class CategoryComboBox:ComboBox
    {
        public CategoryComboBox()
        {
            ItemsSource = App.GolobelCategorys;
            Loaded += CategoryComboBox_Loaded;
            DisplayMemberPath = "Name";
            
        }

        private void CategoryComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //var ctl = (sender as ComboBox);
            //var source = ctl.ItemsSource as List<EmployeeDto>;
            //var dc = (ctl.DataContext as InfoBase).Category;
            //if (dc != null)
            //{
            //    var selectedOne = source.FirstOrDefault(c => c.Code == dc.Code);
            //    ctl.SelectedItem = selectedOne;

            //}
        }
    }
}
