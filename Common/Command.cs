using System;
using System.Windows.Input;

namespace BierPongTurnier.Common
{
    public class Command : ICommand
    {
        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        public Command(Action execute) : this(execute, null)
        {
        }

        public Command(Action execute, Func<bool> canExecute)
        {
            this._execute = execute ?? throw new ArgumentNullException("execute");
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this._canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this._canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute();
        }

        public void Execute(object parameter)
        {
            this._execute();
        }
    }
}