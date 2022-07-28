using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Common
{
    public class MenuCommand
    {
        public string Title { get; set; }
        public RelayCommand Command { get; set; }
        public MenuCommand(string title, Action execute, Func<bool> canExecute)
        {
            this.Title=title;
            this.Command=new RelayCommand(execute, canExecute);
        }
    }
}
