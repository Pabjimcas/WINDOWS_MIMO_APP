namespace WINDOWS_MIMO_APP_2.Services.NavigationService
{
    using Windows.UI.Xaml.Controls;
    internal interface INavigationService
    {
        Frame AppFrame { set; }
        void GoBack();
    }
}
