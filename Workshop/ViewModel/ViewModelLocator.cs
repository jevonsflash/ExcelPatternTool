using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.DataBase;
using Workshop.Core.Validators;
using Workshop.Infrastructure.Core;

namespace Workshop.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
           

        }

        public MainViewModel Main => Ioc.Default.GetRequiredService<MainViewModel>();
        public IndexPageViewModel IndexPage => Ioc.Default.GetRequiredService<IndexPageViewModel>();
        public ImportPageViewModel ImportPage => Ioc.Default.GetRequiredService<ImportPageViewModel>();
        public CreateCategoryViewModel CreateCategory => Ioc.Default.GetRequiredService<CreateCategoryViewModel>();
        public CategoryPageViewModel CategoryPage => Ioc.Default.GetRequiredService<CategoryPageViewModel>();
        public LoginViewModel Login => Ioc.Default.GetRequiredService<LoginViewModel>();
        public SettingPageViewModel SettingPage => Ioc.Default.GetRequiredService<SettingPageViewModel>();
        public ToolsPageViewModel ToolsPage => Ioc.Default.GetRequiredService<ToolsPageViewModel>();


        public static void Cleanup<T>() where T : class
        {
      
        }
    }
}