

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;
    using Windows.UI.Xaml.Navigation;
    using System.Collections.ObjectModel;
    using Models;
    using Windows.UI.Xaml.Controls;
    public class RecipeListViewModel : ViewModelBase
    {
        private IRecipeService recipeService;
        private string message;
        private INavigationService   navService;
        private DelegateCommand loadRecipeListCommand;
        private ObservableCollection<RecipeList> recipes;
        

        public RecipeListViewModel(INavigationService navService,IRecipeService recipeService)
        {
            this.navService = navService;
            this.recipeService = recipeService;
            loadRecipeListCommand = new DelegateCommand(LoadRecipeList, null);
            Message = "Welcome to the recipe List page";
        }
        public void ListItemClicked(object sender, object parameter)
        {
            var arg = parameter as ItemClickEventArgs;
            var recipeItemlist = arg.ClickedItem as RecipeList;
            if (recipeItemlist != null)
            {
                var st =recipeItemlist.id ;
                this.navService.NavigateToRecipePage(st);
            }else
            {
                this.navService.NavigateToRecipePage(1);
            }
            
        }

        

        private async void LoadRecipeList()
        {
            var result = await this.recipeService.GetRecipesAsync();

            if (result != null)
            {
                Recipes = new ObservableCollection<RecipeList>(result);
            }
        }
        public DelegateCommand LoadRecipeListCommand
        {
            get { return loadRecipeListCommand; }
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<RecipeList> Recipes
        {
            get { return recipes; }
            set
            {
                recipes = value;
                RaisePropertyChanged();
            }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            Message = (string)e.Parameter;
            LoadRecipeList();
        }
        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        public override void GoBackExecute()
        {
            base.GoBackExecute();
        }
    }
}
