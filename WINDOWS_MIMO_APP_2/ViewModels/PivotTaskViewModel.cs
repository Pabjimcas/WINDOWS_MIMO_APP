

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;
    using Windows.UI.Xaml.Navigation;
    using System.Collections.ObjectModel;
    using Models;
    using Windows.UI.Xaml.Input;
    public class PivotTaskViewModel : ViewModelBase
    {
        private string recipeName;
        private INavigationService   navService;

        public PivotTaskViewModel(INavigationService navService)
        {
            this.navService = navService;
            //this.TaskList = new ObservableCollection<TaskPivotModel>();
        }

        public ObservableCollection<Task> TaskList { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {

            this.IsDataLoaded = true;
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

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            var recipe = (Recipe)e.Parameter;
            List<Task> result = recipe.tasks;
            RecipeName = recipe.name;
            TaskList = new ObservableCollection<Task>(result);
            /*foreach (Task task in result)
            {
                var taskPivotModel = new TaskPivotModel()
                {
                    Header = "Paso " + task.name
                };
                taskPivotModel.Task = task;
                TaskList.Add(taskPivotModel);
            }*/


            //
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
