using GalaSoft.MvvmLight.Ioc;
using Workshop.View;

namespace Workshop.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IndexPageViewModel>();
            SimpleIoc.Default.Register<ImportPageViewModel>(); SimpleIoc.Default.Register<CreateCategoryViewModel>();
            SimpleIoc.Default.Register<CategoryPageViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<SettingPageViewModel>();

        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public IndexPageViewModel IndexPage => SimpleIoc.Default.GetInstance<IndexPageViewModel>();
        public ImportPageViewModel ImportPage => SimpleIoc.Default.GetInstance<ImportPageViewModel>();
        public CreateCategoryViewModel CreateCategory => SimpleIoc.Default.GetInstance<CreateCategoryViewModel>();
        public CategoryPageViewModel CategoryPage => SimpleIoc.Default.GetInstance<CategoryPageViewModel>();
        public LoginViewModel Login => SimpleIoc.Default.GetInstance<LoginViewModel>();
        public SettingPageViewModel SettingPage => SimpleIoc.Default.GetInstance<SettingPageViewModel>();


        public static void Cleanup<T>() where T : class
        {
            SimpleIoc.Default.Unregister<T>();
            SimpleIoc.Default.Register<T>();
        }
    }
}