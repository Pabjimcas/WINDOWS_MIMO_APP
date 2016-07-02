

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
    using System.Diagnostics;
    using Windows.Media.Capture;
    using Windows.Storage.Streams;
    using Windows.Media.MediaProperties;
    using Windows.Devices.Sensors;
    using Windows.Storage.FileProperties;
    using Services.TileService;
    public class RecipeViewModel : ViewModelBase
    {
        private string message;
        private INavigationService   navService;
        private IRecipeService recipeService;
        private ITileService tileService;
        private IDbService dbService;
        private DelegateCommand goToTaskListPageCommand;
        private DelegateCommand addToFavoritesCommand;
        private DelegateCommand captureImageCommand;

        private Recipe recipe;
        private string photo;
        private List<MeasureIngredient> ingredientList;
        private string title;
        private DelegateCommand goToSplitTaskPageCommand;
        private IDialogService dialogService;
        private DelegateCommand goToIngredientListPageCommand;
        private Visibility favoriteButtom = Visibility.Visible;

        public List<MeasureIngredient> IngredientList
        {
            get { return ingredientList; }
            set { ingredientList = value; RaisePropertyChanged(); }
        }


        

        public Visibility FavoriteButtom
        {
            get { return favoriteButtom; }
            set { favoriteButtom = value; RaisePropertyChanged(); }
        }


      

        public RecipeViewModel(INavigationService navService, IRecipeService recipeService, IDbService dbService,IDialogService dialogService,ITileService tileService)
        {
            this.navService = navService;
            this.recipeService = recipeService;
            this.dbService = dbService;
            this.dialogService = dialogService;
            this.tileService = tileService;
            this.goToTaskListPageCommand = new DelegateCommand(GoToTaskListPageExecute);
            this.goToSplitTaskPageCommand = new DelegateCommand(GoToSplitTaskPageExecute);
            this.goToIngredientListPageCommand = new DelegateCommand(GoToIngredientListPageExecute);
            this.addToFavoritesCommand = new DelegateCommand(AddToFavoritesExecute);
            this.captureImageCommand = new DelegateCommand(CaptureImageExecute);
            Message = "Welcome to the recipe page";
        }

        private void CaptureImageExecute()
        {
            this.navService.NavigateToCaptureImagePage(Recipe.name);
        }

        private void GoToIngredientListPageExecute()
        {
            this.navService.NavigateToIngredientListPage(Recipe.measureIngredients);
        }

        private void AddToFavoritesExecute()
        {
                this.dbService.addRecipeFavorite(recipe);
                this.dialogService.ShowMessage("The Recipe has been saved as favorite ", "Save Favorite");
                tileService.CreateRecipeTile(recipe);
                FavoriteButtom = Visibility.Collapsed;
        }

        private async void LoadRecipe(RecipeList item)
        {
                bool existRecipe = this.dbService.recipeFavoriteExists(item.name);
                if (existRecipe)
                {
                    Recipe = this.dbService.getFavoriteRecipe(item.name);
                    FavoriteButtom = Visibility.Collapsed;
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

        public ICommand CaptureImageCommand
        {
            get { return this.captureImageCommand; }
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
