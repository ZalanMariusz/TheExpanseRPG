using System;
using System.Windows.Input;

namespace TheExpanseRPG.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        public RelayCommand(Predicate<object> canExecute , Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }
        public bool CanExecute(object? parameter)
        {
            return _canExecute.Invoke(parameter!);
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter!);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
