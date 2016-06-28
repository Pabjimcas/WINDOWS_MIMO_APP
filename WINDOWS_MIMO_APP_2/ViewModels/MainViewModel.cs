

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Base;
    using Models;
    using Newtonsoft.Json;
    using Services.Database;
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Navigation;

    public class MainViewModel : ViewModelBase
    {
        private INavigationService navService;
        private IDbService dbService;
        private DelegateCommand goToRecipePageCommand;
        private DelegateCommand goToRecipeListPageCommand;
        private DelegateCommand openRecipePictureCommand;
        private ObservableCollection<RecipeFavorite> favoriteRecipes;
        private RecipeFavorite randomRecipe;
        private string name;
        private string photo;
        private bool buttonEnabled;
        public MainViewModel(INavigationService navService, IDbService dbService)
        {
            this.navService = navService;
            this.dbService = dbService;
            this.goToRecipeListPageCommand = new DelegateCommand(GoToRecipeListPageExecute);
            this.goToRecipePageCommand = new DelegateCommand(GoToRecipePageExecute);
            this.openRecipePictureCommand = new DelegateCommand(OpenRecipePictureExecute);

            buttonEnabled = true;
            this.generateRandomRecipe();
            
        }

        public void generateRandomRecipe()
        {
            var favoritesList = this.dbService.getFavorites();
        
            if (favoritesList.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, favoritesList.Count);
                RandomRecipe = favoritesList.ElementAt(index);
                Name = randomRecipe.name;
                Photo = randomRecipe.photo;
                ButtonEnabled = true;
            }else
            {
                Name = "Sin favoritos";
                Photo = "/Assets/default.scale-100.jpg";
                ButtonEnabled = false;
            }
        }

        public bool ButtonEnabled
        {
            get
            {
                return this.buttonEnabled;
            }
            set
            {
                this.buttonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                RaisePropertyChanged();
            }
        }

        public string Photo
        {
            get
            {
                return this.photo;
            }
            set
            {
                this.photo = value;
                RaisePropertyChanged();
            }
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

        public ICommand OpenRecipePictureCommand
        {
            get { return this.openRecipePictureCommand; }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
        }

        private void OpenRecipePictureExecute()
        {
            this.navService.NavigateToImagesPage("Images");
        }

        private void GoToRecipePageExecute()
        {
            RecipeList item = new RecipeList();
            item.id = randomRecipe.id;
            item.type = "favorites";
            string json = JsonConvert.SerializeObject(item);
            this.navService.NavigateToRecipePage(json);
        }
        private void GoToRecipeListPageExecute()
        {
            this.navService.NavigateToRecipeListPage("RecipeList");
        }

    }
}
