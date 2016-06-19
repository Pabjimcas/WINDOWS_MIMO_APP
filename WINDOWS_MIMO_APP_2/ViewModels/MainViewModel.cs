

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Base;
    using Models;
    using Services.Database;
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Navigation;

    public class MainViewModel : ViewModelBase
    {
        private INavigationService navService;
        private IDbService dbService;
        private DelegateCommand goToRecipePageCommand;
        private DelegateCommand goToRecipeListPageCommand;
        private ObservableCollection<RecipeFavorite> favoriteRecipes;
        public MainViewModel(INavigationService navService, IDbService dbService)
        {
            this.navService = navService;
            this.dbService = dbService;
            this.goToRecipeListPageCommand = new DelegateCommand(GoToRecipeListPageExecute);
            this.goToRecipePageCommand = new DelegateCommand(GoToRecipePageExecute);

            var favoritesList = this.dbService.getFavorites();
            FavoriteRecipes = new ObservableCollection<RecipeFavorite>(favoritesList);
        }

        public ObservableCollection<RecipeFavorite> FavoriteRecipes
        {
            get { return this.favoriteRecipes; }
            set
            {
                this.favoriteRecipes = value;
                RaisePropertyChanged();
            }
        }



        public ICommand GoToRecipePageCommand
        {
            get { return this.goToRecipePageCommand; }
        }
        public ICommand GoToRecipeListPageCommand
        {
            get { return this.goToRecipeListPageCommand; }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
        }

        private void GoToRecipePageExecute()
        {
            this.navService.NavigateToRecipePage("RecipePage");
        }
        private void GoToRecipeListPageExecute()
        {
            this.navService.NavigateToRecipeListPage("RecipeList");
        }

    }
}
