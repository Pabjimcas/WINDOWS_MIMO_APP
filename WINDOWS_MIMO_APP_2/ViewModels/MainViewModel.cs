﻿

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Base;
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Navigation;

    public class MainViewModel : ViewModelBase
    {
        private INavigationService navService;
        private DelegateCommand goToRecipePageCommand;
        
        public MainViewModel(INavigationService navService)
        {
            this.navService = navService;
            this.goToRecipePageCommand = new DelegateCommand(GoToRecipePageExecute);
        }

        public ICommand GoToRecipePageCommand
        {
            get { return this.goToRecipePageCommand; }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
        }

        private void GoToRecipePageExecute()
        {
            this.navService.NavigateToRecipePage("RecipePage");
        }

    }
}
