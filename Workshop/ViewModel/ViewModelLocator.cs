using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.DataBase;
using Workshop.Core.Validators;
using Workshop.Core;

namespace Workshop.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
           

        }

        public MainViewModel Main => Ioc.Default.GetRequiredService<MainViewModel>();
        public ImportPageViewModel ImportPage => Ioc.Default.GetRequiredService<ImportPageViewModel>();
        public CategoryPageViewModel CategoryPage => Ioc.Default.GetRequiredService<CategoryPageViewModel>();
        public SettingPageViewModel SettingPage => Ioc.Default.GetRequiredService<SettingPageViewModel>();


        public static void Cleanup<T>() where T : class
        {
      
        }
    }
}