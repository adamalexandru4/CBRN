using System;
using System.Diagnostics;
using System.Windows.Input;

namespace CBRN_Project.Utility
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object>     execute;
        private readonly Predicate<object>  canExecute;

        public RelayCommand(Action<object> _execute, Predicate<object> _canExecute = null)
        {
            this.execute    = _execute ?? throw new ArgumentNullException("execute");
            this.canExecute = _canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add    => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter) => execute(parameter);

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }
    }
}
