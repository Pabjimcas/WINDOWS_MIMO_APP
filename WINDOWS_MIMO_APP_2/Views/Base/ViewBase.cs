namespace WINDOWS_MIMO_APP2.Views.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;

    public class ViewBase : Page
    {
        private ViewModelBase vmBase;
        public ViewBase() { }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this.DataContext != null)
            {
                this.vmBase = (ViewModelBase)this.DataContext;
                this.vmBase.AppFrame = this.Frame;
                this.vmBase.OnNavigatedTo(e);
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (this.vmBase != null)
            {
                this.vmBase.OnNavigatedFrom(e);
            }
        }

    }
}
