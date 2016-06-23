

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
        private RecipeFavorite randomRecipe;
        public MainViewModel(INavigationService navService, IDbService dbService)
        {
            this.navService = navService;
            this.dbService = dbService;
            this.goToRecipeListPageCommand = new DelegateCommand(GoToRecipeListPageExecute);
            this.goToRecipePageCommand = new DelegateCommand(GoToRecipePageExecute);

            this.generateRandomRecipe();
            
        }

        public void generateRandomRecipe()
        {
            var favoritesList = this.dbService.getFavorites();
        
            Random rnd = new Random();
            int index = rnd.Next(0, favoritesList.Count);
            RandomRecipe = favoritesList.ElementAt(index);
        }

        public RecipeFavorite RandomRecipe
        {
            get
            {
                return this.randomRecipe;
            }
            set
            {
                this.randomRecipe = value;
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
            this.navService.NavigateToRecipePage(randomRecipe.id);
        }
        private void GoToRecipeListPageExecute()
        {
            this.navService.NavigateToRecipeListPage("RecipeList");
        }

    }
}
