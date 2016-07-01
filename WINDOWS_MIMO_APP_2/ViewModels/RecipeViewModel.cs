

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;
    using Windows.UI.Xaml.Navigation;
    using System.Windows.Input;
    using Models;
    using Services.Database;
    using System.Collections.ObjectModel;
    using Services.DialogService;
    using Windows.UI.Xaml;
    using Newtonsoft.Json;
    public class RecipeViewModel : ViewModelBase
    {
        private string message;
        private INavigationService   navService;
        private IRecipeService recipeService;
        private IDbService dbService;
        private DelegateCommand goToTaskListPageCommand;
        private DelegateCommand addToFavoritesCommand;
        private DelegateCommand removeFromFavoritesCommand;
        private Recipe recipe;
        private string photo;
        private List<MeasureIngredient> ingredientList;

        public List<MeasureIngredient> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; RaisePropertyChanged(); }
        }


        private Visibility favoriteButtom = Visibility.Visible;
        private Visibility nofavoriteButtom = Visibility.Collapsed;


        public Visibility FavoriteButtom
        {
            get { return favoriteButtom; }
            set { favoriteButtom = value; RaisePropertyChanged(); }
        }

        public Visibility NoFavoriteButtom
        {
            get { return nofavoriteButtom; }
            set { nofavoriteButtom = value; RaisePropertyChanged(); }
        }


        private string title;
        private DelegateCommand goToSplitTaskPageCommand;
        private IDialogService dialogService;
        private DelegateCommand goToIngredientListPageCommand;

        public RecipeViewModel(INavigationService navService, IRecipeService recipeService, IDbService dbService,IDialogService dialogService)
        {
            this.navService = navService;
            this.recipeService = recipeService;
            this.dbService = dbService;
            this.dialogService = dialogService;
            this.goToTaskListPageCommand = new DelegateCommand(GoToTaskListPageExecute);
            this.goToSplitTaskPageCommand = new DelegateCommand(GoToSplitTaskPageExecute);
            this.goToIngredientListPageCommand = new DelegateCommand(GoToIngredientListPageExecute);
            this.addToFavoritesCommand = new DelegateCommand(AddToFavoritesExecute);
            this.removeFromFavoritesCommand = new DelegateCommand(RemoveFromFavoritesExecute);
            Message = "Welcome to the recipe page";
        }

        private void GoToIngredientListPageExecute()
        {
            this.navService.NavigateToIngredientListPage(Recipe.measureIngredients);
        }

        private void AddToFavoritesExecute()
        {
                this.dbService.addRecipeFavorite(recipe);
                this.dialogService.ShowMessage("The Recipe has been saved as favorite ", "Save Favorite");
                FavoriteButtom = Visibility.Collapsed;
            NoFavoriteButtom = Visibility.Visible;
        }

        private void RemoveFromFavoritesExecute()
        {
            this.dbService.removeRecipeFavorite(recipe.id);
            this.dialogService.ShowMessage("The Recipe has been removed from favorite ", "Remove Favorite");
            FavoriteButtom = Visibility.Visible;
            NoFavoriteButtom = Visibility.Collapsed;
        }

        private async void LoadRecipe(RecipeList item)
        {
            if(Recipe == null)
            {
                bool existRecipe = this.dbService.recipeFavoriteExists(item.name);
                if (existRecipe)
                {
                    Recipe = this.dbService.getFavoriteRecipe(item.name);
                    FavoriteButtom = Visibility.Collapsed;
                    NoFavoriteButtom = Visibility.Visible;
                }
                else
                {
                    var result = await this.recipeService.GetRecipeAsync(item.id);

                    if (result != null)
                    {
                        Recipe = result;
                    }
                }
            }
           
        }
        public Recipe Recipe
        {
            get { return recipe; }
            set
            {
                recipe = value;
                Message = recipe.name;
                Title = recipe.author;
                Photo = recipe.photo;
                TaskList = recipe.tasks;
                IngredientList = recipe.measureIngredients;
                RaisePropertyChanged();
            }
        }

        public ICommand GoToTaskListPageCommand
        {
            get { return this.goToTaskListPageCommand; }
        }
        public ICommand GoToIngredientListPageCommand
        {
            get { return this.goToIngredientListPageCommand; }
        }
        public ICommand GoToSplitTaskPageCommand
        {
            get { return this.goToSplitTaskPageCommand; }
        }

        public ICommand AddToFavoritesCommand
        {
            get { return this.addToFavoritesCommand; }
        }

        public ICommand RemoveFromFavoritesCommand
        {
            get { return this.removeFromFavoritesCommand; }
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
        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                RaisePropertyChanged();
            }
        }
        public string Title
        {
            get {return title;}
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        public List<Task> TaskList { get; private set; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
            this.navService.AppFrame = base.AppFrame;

            var json = (String)e.Parameter;
            
             RecipeList recipeItem=JsonConvert.DeserializeObject<RecipeList>(json);
            
            LoadRecipe(recipeItem);
        }
        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        public override void GoBackExecute()
        {
            base.GoBackExecute();
        }
        private void GoToTaskListPageExecute()
        {
            this.navService.NavigateToTaskListPage(Recipe);
        }
        private void GoToSplitTaskPageExecute()
        {
            this.navService.NavigateToSplitTaskPage(Recipe);
        }
    }
}
