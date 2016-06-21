

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
    public class PivotTaskViewModel : ViewModelBase
    {
        private string message;
        private INavigationService   navService;

        public PivotTaskViewModel(INavigationService navService)
        {
            this.navService = navService;
            this.TaskList = new ObservableCollection<TaskPivotModel>();
            Message = "Welcome to the split task page";
        }

        public ObservableCollection<TaskPivotModel> TaskList { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {

            this.IsDataLoaded = true;
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
        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            List<Task> result = (List<Task>)e.Parameter;

            foreach (Task task in result)
            {
                var taskPivotModel = new TaskPivotModel()
                {
                    Header = "Paso " + task.name
                };
                taskPivotModel.Task = task;
                TaskList.Add(taskPivotModel);
            }


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
