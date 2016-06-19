﻿

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
    using System.Windows.Input;
    using Models;
    using Services.Database;
    public class RecipeViewModel : ViewModelBase
    {
        private string message;
        private INavigationService   navService;
        private IRecipeService recipeService;
        private IDbService dbService;
        private DelegateCommand goToTaskListPageCommand;
        private DelegateCommand addToFavoritesCommand;
        private DelegateCommand loadRecipeCommand;
        private Recipe recipe;

        private string title;

        public string Title
        {
            get {
                if (recipe != null)
                {
                    return recipe.name;
                }else
                {
                    return title;
                }
           }
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }


        public RecipeViewModel(INavigationService navService, IRecipeService recipeService, IDbService dbService)
        {
            this.navService = navService;
            this.recipeService = recipeService;
            this.dbService = dbService;
            this.goToTaskListPageCommand = new DelegateCommand(GoToTaskListPageExecute);
            loadRecipeCommand = new DelegateCommand(LoadRecipe, null);
            this.addToFavoritesCommand = new DelegateCommand(AddToFavoritesExecute);
            Message = "Welcome to the recipe page";
        }

        private void AddToFavoritesExecute()
        {
            this.dbService.addRecipeFavorite(recipe);
        }

        private async void LoadRecipe()
        {
            var result = await this.recipeService.GetRecipeAsync(1);

            if (result != null)
            {
                Recipe = result;
            }
        }
        public Recipe Recipe
        {
            get { return recipe; }
            set
            {
                recipe = value;
                Title = recipe.name;
                RaisePropertyChanged();
            }
        }

        public ICommand GoToTaskListPageCommand
        {
            get { return this.goToTaskListPageCommand; }
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
        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            Message = (string)e.Parameter;
            LoadRecipe();
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
            this.navService.NavigateToTaskListPage("TaskList");
        }
    }
}
