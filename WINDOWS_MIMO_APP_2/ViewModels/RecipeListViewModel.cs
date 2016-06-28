

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Navigation;
    using System.Collections.ObjectModel;
    using Models;
    using Windows.UI.Xaml.Controls;
    using Base;
    using System;
    using Newtonsoft.Json;
    public class RecipeListViewModel : ViewModelBase
    {
        private IRecipeService recipeService;
        private string message;
        private INavigationService   navService;
        private DelegateCommand loadRecipeListCommand;
        private ObservableCollection<RecipeList> recipes;
        private bool estado = true;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; RaisePropertyChanged(); }
        }
       


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
                string json = JsonConvert.SerializeObject(recipeItemlist);
                this.navService.NavigateToRecipePage(json);
            }
            else
            {
                RecipeList item = new RecipeList();
                item.id = 1;
                string json = JsonConvert.SerializeObject(item);
                this.navService.NavigateToRecipePage(item);
            }
            

        }

        

        private async void LoadRecipeList()
        {
            var result = await this.recipeService.GetRecipesAsync();

            if (result != null)
            {
                Recipes = new ObservableCollection<RecipeList>(result);
                Estado = false;
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
