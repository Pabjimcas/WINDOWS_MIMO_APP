

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
        private Recipe recipe;
        private string photo;
        private List<MeasureIngredient> ingredientList;

        public List<MeasureIngredient> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; RaisePropertyChanged(); }
        }


        private Visibility _advancedFormat = Visibility.Visible;

        public Visibility AdvancedFormat
        {
            get { return _advancedFormat; }
            set { _advancedFormat = value; RaisePropertyChanged(); }
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
            AdvancedFormat = Visibility.Collapsed;
        }



        private async void LoadRecipe(RecipeList item)
        {
            bool existRecipe = this.dbService.recipeFavoriteExists(item.name);


            if(existRecipe)
            {
                Recipe = this.dbService.getFavoriteRecipe(item.name);
                AdvancedFormat = Visibility.Collapsed;
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
