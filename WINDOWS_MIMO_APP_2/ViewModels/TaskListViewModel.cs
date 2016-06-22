

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
    using System.Collections.ObjectModel;
    using Models;
    using Windows.UI.Xaml.Controls;
    public class TaskListViewModel : ViewModelBase
    {
        private string recipeName;
        private INavigationService   navService;
        private DelegateCommand goToTaskPageCommand;
        private DelegateCommand loadTaskListCommand;
        private bool estado = true;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; RaisePropertyChanged(); }
        }


        public TaskListViewModel(INavigationService navService)
        {
            this.navService = navService;
            this.goToTaskPageCommand = new DelegateCommand(GoToTaskPageExecute);
        }
        public ICommand GoToTaskPageCommand
        {
            get { return this.goToTaskPageCommand; }
        }

        
        public string RecipeName
        {
            get { return recipeName; }
            set
            {
                recipeName = value;
                RaisePropertyChanged();
            }
        }

        public void ListItemClicked(object sender, object parameter)
        {
            var arg = parameter as ItemClickEventArgs;
            var taskItemList = arg.ClickedItem as Task;
            if (taskItemList != null)
            {
                this.navService.NavigateToTaskPage(taskItemList);
            }

        }

        public ObservableCollection<Task> TaskList { get; private set; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            if (e.Parameter != null)
            {
                var recipe = (Recipe)e.Parameter;
                List<Task> result= recipe.tasks;
                TaskList = new ObservableCollection<Task>(result);
                RecipeName = recipe.name;
                Estado = false;
            }
            
            //Message = (string)e.Parameter;
        }
        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        public override void GoBackExecute()
        {
            base.GoBackExecute();
        }
        private void GoToTaskPageExecute()
        {
            this.navService.NavigateToTaskPage("TASK");
        }
    }
}
