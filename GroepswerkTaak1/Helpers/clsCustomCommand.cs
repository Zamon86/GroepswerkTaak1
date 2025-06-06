using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Common
{
    public class clsCustomCommand : ICommand
    {
        Action<object> _TargetExecuteMethod;
        Predicate<object> _TargetCanExecuteMethod;
        public event EventHandler? CanExecuteChanged = delegate { };

        public clsCustomCommand(Action<object> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;

        }
        public clsCustomCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            _TargetExecuteMethod= executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }
        public bool CanExecute(object? parameter)
        {
            bool b = _TargetCanExecuteMethod == null ? true : _TargetCanExecuteMethod(parameter);
            return b;
        }
       
        public void Execute(object? parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((object)parameter);
            }
        }

        public void RaiseCanExecuteChanged() { 
        CanExecuteChanged(this,EventArgs.Empty);
        
        }
    }
}
