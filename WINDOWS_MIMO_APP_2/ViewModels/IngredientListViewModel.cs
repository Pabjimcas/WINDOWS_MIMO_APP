

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
    public class IngredientListViewModel : ViewModelBase
    {
        private string message;
        private INavigationService   navService;
        private ObservableCollection<MeasureIngredient> ingredients;



        public IngredientListViewModel(INavigationService navService, IRecipeService recipeService)
        {
            this.navService = navService;
            Message = "Ingredientes Necesarios";
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

        public ObservableCollection<MeasureIngredient> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                RaisePropertyChanged();
            }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            if (e.Parameter != null)
            {
                List<MeasureIngredient> result = e.Parameter as List<MeasureIngredient>; 
                Ingredients = new ObservableCollection<MeasureIngredient>(result);
            }
            
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
