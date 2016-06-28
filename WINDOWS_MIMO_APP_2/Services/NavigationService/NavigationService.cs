namespace WINDOWS_MIMO_APP_2.Services.NavigationService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Views;
    using Windows.UI.Xaml.Controls;
    public class NavigationService : INavigationService
    {
        public Frame AppFrame { private get; set; }
        public void GoBack()
        {
            if (AppFrame != null && AppFrame.CanGoBack)
                AppFrame.GoBack();
        }
        public void NavigateToRecipePage()
        {
            NavigateToRecipePage<Object>(null);
        }

        public void NavigateToRecipePage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(RecipePage), argument);
        }
        public void NavigateToRecipeListPage()
        {
            NavigateToRecipeListPage<Object>(null);
        }

        public void NavigateToRecipeListPage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(RecipeListPage), argument);
        }
        public void NavigateToIngredientListPage()
        {
            NavigateToIngredientListPage<Object>(null);
        }

        public void NavigateToIngredientListPage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(IngredientListPage), argument);
        }
        public void NavigateToTaskListPage()
        {
            NavigateToTaskListPage<Object>(null);
        }

        public void NavigateToTaskListPage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(TaskListPage), argument);
        }
        public void NavigateToTaskPage()
        {
            NavigateToTaskPage<Object>(null);
        }

        public void NavigateToTaskPage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(TaskPage), argument);
        }
        public void NavigateToSplitTaskPage()
        {
            NavigateToSplitTaskPage<Object>(null);
        }

        public void NavigateToSplitTaskPage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(SplitTaskPage), argument);
        }

        public void NavigateToImagesPage()
        {
            NavigateToImagesPage<Object>(null);
        }

        public void NavigateToImagesPage<T>(T argument)
        {
            if (AppFrame != null)
                AppFrame.Navigate(typeof(ImagesPage), argument);
        }
    }
}
