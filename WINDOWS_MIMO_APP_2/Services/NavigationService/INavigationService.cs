namespace WINDOWS_MIMO_APP_2.Services.NavigationService
{
    using Windows.UI.Xaml.Controls;
    public interface INavigationService
    {
        Frame AppFrame { set; }
        void GoBack();
        void NavigateToRecipePage();
        void NavigateToRecipePage<T>(T argument);
        void NavigateToRecipeListPage();
        void NavigateToRecipeListPage<T>(T argument);
        void NavigateToTaskListPage();
        void NavigateToTaskListPage<T>(T argument);
       
    }
}
