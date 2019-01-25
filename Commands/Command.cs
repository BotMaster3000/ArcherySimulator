using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArcherySimulator.Commands
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action action;

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter = null)
        {
            action.Invoke();
        }
    }
}
