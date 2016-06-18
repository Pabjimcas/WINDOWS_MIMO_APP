﻿namespace WINDOWS_MIMO_APP_2.ViewModels.Base
{
    using Autofac;
    using Services.NavigationService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class ViewModelLocator
    {
        private IContainer container;

        public ViewModelLocator()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<RecipeViewModel>();
            builder.RegisterType<RecipeListViewModel>();
            builder.RegisterType<TaskListViewModel>();

            this.container = builder.Build();
        }

        public MainViewModel MainVM
        {
            get { return this.container.Resolve<MainViewModel>(); }
        }

        public RecipeViewModel RecipeVM
        {
            get { return this.container.Resolve<RecipeViewModel>(); }
        }
        public RecipeListViewModel RecipeListVM
        {
            get { return this.container.Resolve<RecipeListViewModel>(); }
        }
        public TaskListViewModel TaskListVM
        {
            get { return this.container.Resolve<TaskListViewModel>(); }
        }

    }
}
