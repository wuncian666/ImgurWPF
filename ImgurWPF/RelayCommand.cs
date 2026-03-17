using System;
using System.Windows.Input;

namespace TodoList
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action.Invoke();
        }
    }

    internal class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<T> action;

        public RelayCommand(Action<T> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action.Invoke((T)parameter);
        }
    }
}