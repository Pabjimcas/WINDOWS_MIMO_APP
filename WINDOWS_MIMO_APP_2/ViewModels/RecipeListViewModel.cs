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
    using System.Collections.ObjectModel;
    using Models;
    public class RecipeListViewModel : ViewModelBase
    {
        private IRecipeService recipeService;
        private string message;
        private INavigationService   navService;
        private DelegateCommand loadRecipeCommand;
        private ObservableCollection<RecipeList> recipes;

        public RecipeListViewModel(INavigationService navService,IRecipeService recipeService)
        {
            this.navService = navService;
            this.recipeService = recipeService;
            loadRecipeCommand = new DelegateCommand(LoadRecipe, null);
            Message = "Welcome to the recipe List page";
        }

        private async void LoadRecipe()
        {
            var result = await this.recipeService.GetRecipesAsync();

            if (result != null)
            {
                Recipes = new ObservableCollection<RecipeList>(result);
            }
        }
        public DelegateCommand LoadRecipeCommand
        {
            get { return loadRecipeCommand; }
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
