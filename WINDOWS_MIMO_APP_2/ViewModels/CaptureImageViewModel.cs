

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;
    using Windows.UI.Xaml.Navigation;
    public class CaptureImageViewModel : ViewModelBase
    {
        private INavigationService navService;
        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { recipeName = value;
                RaisePropertyChanged();
            }
        }


        public CaptureImageViewModel(INavigationService navService)
        {
            this.navService = navService; 
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
            this.navService.AppFrame = base.AppFrame;
            RecipeName = e.Parameter as string;
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
