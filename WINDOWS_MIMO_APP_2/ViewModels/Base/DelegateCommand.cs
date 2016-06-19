namespace WINDOWS_MIMO_APP_2.ViewModels.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    public class DelegateCommand : ICommand
    {
        private Action executeAction;
        private Func<bool> canExecuteAction;
        private DelegateCommand loadRecipes;
        private object p;

        public DelegateCommand(Action exec) : this(exec, null) { }
      
        public DelegateCommand(Action exec, Func<bool> canExec)
        {
            this.executeAction = exec;
            this.canExecuteAction = canExec;
        }
        public DelegateCommand(DelegateCommand loadRecipes, object p)
        {
            this.loadRecipes = loadRecipes;
            this.p = p;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteAction == null)
            {
                return true;
            }
            return canExecuteAction();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (executeAction != null)
            {
                this.executeAction();
            }
        }
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(null, new EventArgs());
            }
        }
    }
}
