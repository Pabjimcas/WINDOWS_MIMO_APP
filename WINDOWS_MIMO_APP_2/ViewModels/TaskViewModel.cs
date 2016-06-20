

namespace WINDOWS_MIMO_APP_2.ViewModels
{
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WINDOWS_MIMO_APP_2.ViewModels.Base;
    using Windows.UI.Xaml.Navigation;
    using Models;
    public class TaskViewModel : ViewModelBase
    {
        private string message;
        private INavigationService   navService;
        private string taskTitle;
        private string photo;
        private string description;
        private int? seconds;

        public TaskViewModel(INavigationService navService)
        {
            this.navService = navService;
            Message = "Welcome to the task page";
        }

        public string TaskTitle
        {
            get { return taskTitle; }
            set
            {
                taskTitle = value;
                RaisePropertyChanged();
            }
        }

        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged();
            }
        }

        public int? Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                RaisePropertyChanged();
            }
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

        private void LoadTask(Task task)
        {
            if (task != null)
            {
                TaskTitle = task.name;
                Photo = task.photo;
                Description = task.description;
                Seconds = task.seconds;
            }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navService.AppFrame = base.AppFrame;
            LoadTask((Task)e.Parameter);
            Message = (string)e.Parameter;
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
