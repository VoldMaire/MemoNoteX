namespace MemoNote.ViewModel
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;

        private readonly Predicate<object> canExecute;

        public DelegateCommand(Action<object> _execute, Predicate<object> _canExecute = null)
        {
            if (_execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            execute = _execute;
            canExecute = _canExecute;
        }

        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
