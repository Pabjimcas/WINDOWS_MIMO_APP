namespace WINDOWS_MIMO_APP_2.ViewModels.Base
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    public class ViewModelBase : INotifyPropertyChanged
    {
        private DelegateCommand goBackCommand;
        private bool showSoftBackButton;
        private Frame appFrame;

        public ViewModelBase()
        {
            this.goBackCommand = new DelegateCommand(GoBackExecute, null);
            QueryForHardwareButton();
        }

        public bool ShowSoftBackButton => this.showSoftBackButton;
        public ICommand GoBackCommand => this.goBackCommand;

        public virtual void GoBackExecute() { }

        private void QueryForHardwareButton()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //this means we are executing on phone, so don't need to show soft back button.
                this.showSoftBackButton = false;
            }
            else
            {
                this.showSoftBackButton = true;
            }
        }

        public Frame AppFrame
        {
            get { return this.appFrame; }
            set { this.appFrame = value; }
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e) { }

        public virtual void OnNavigatedFrom(NavigationEventArgs e) { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
