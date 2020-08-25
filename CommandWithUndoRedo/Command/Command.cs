using SimpleStateMachineNodeEditor.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WPFUndoRedo
{
    public class Command<Tparameter>: ICommand
    {
        public event EventHandler CanExecuteChanged;
        protected Action<Tparameter> Action;
        protected Func<Tparameter, bool> CanExecute;

        public Command(Action<Tparameter> action, Func<Tparameter, bool> canExecute=null)
        {
            Action = action;
            CanExecute = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute?.Invoke(parameter.Cast<Tparameter>())??true;
        }

        public void Execute(object parameter= default)
        {
            Action.Invoke(parameter.Cast<Tparameter>());
        }
    }
}
