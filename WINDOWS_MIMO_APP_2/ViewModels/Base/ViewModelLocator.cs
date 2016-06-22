﻿namespace WINDOWS_MIMO_APP_2.ViewModels.Base
{
    using Autofac;
    using Services.Database;
    using Services.DialogService;
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
            builder.RegisterType<RecipeService>().As<IRecipeService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<DbService>().As<IDbService>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<RecipeViewModel>();
            builder.RegisterType<RecipeListViewModel>();
            builder.RegisterType<TaskListViewModel>();
            builder.RegisterType<TaskViewModel>();
            builder.RegisterType<PivotTaskViewModel>();

            this.container = builder.Build();
        }

        public T Resolve<T>()
        {
            return this.container.Resolve<T>();
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
        public TaskViewModel TaskVM
        {
            get { return this.container.Resolve<TaskViewModel>(); }
        }
        public PivotTaskViewModel SplitTaskVM
        {
            get { return this.container.Resolve<PivotTaskViewModel>(); }
        }

    }
}
