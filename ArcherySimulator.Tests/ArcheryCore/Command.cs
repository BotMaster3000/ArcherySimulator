using System;

namespace ArcherySimulator.Commands
{
    public interface ICommand
    {
        event EventHandler? CanExecuteChanged;
        bool CanExecute(object? parameter);
        void Execute(object? parameter = null);
    }

    public class Command : ICommand
    {
        private readonly Action action;
        public event EventHandler? CanExecuteChanged;

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter = null) => action();
    }
}
