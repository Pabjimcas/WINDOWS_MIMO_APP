

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
    public class CaptureImageViewModel : ViewModelBase
    {
        private INavigationService navService;

        public CaptureImageViewModel(INavigationService navService)
        {
            this.navService = navService;
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
           
            this.navService.AppFrame = base.AppFrame;
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
