namespace WINDOWS_MIMO_APP_2.Services.NavigationService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Controls;
    public class NavigationService : INavigationService
    {
        public Frame AppFrame { private get; set; }
        public void GoBack()
        {
            if (AppFrame != null && AppFrame.CanGoBack)
                AppFrame.GoBack();
        }

    }
}
