namespace WINDOWS_MIMO_APP_2.ViewModels.Base
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
       
        //public RecipeViewModel RecipeVM
        //{
         //   get { return this.container.Resolve<RecipeViewModel>(); }
        //}
    }
}
