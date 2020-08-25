using SimpleStateMachineNodeEditor.Helpers.Extensions;
using System;
using System.Windows.Input;

namespace WPFUndoRedo
{
    public class CommandWithUndoRedo<TParameter, TResult> : ICommandWithUndoRedo, ICommand
    {    
        public delegate void ExecuteHandler(TParameter parameter);
        internal CommandWithUndoRedo(HistoryContext historyContext,
                string commandKey,
                Func<TParameter, TResult, TResult> execute,
                Func<TParameter, TResult, TResult> unExecute,
                Func<TParameter, bool> canExecute)
        {
            History = historyContext;
            _execute = execute;
            _unExecute = unExecute;
            _canExecute = canExecute;
            CommandKey = commandKey;

            CommandManager.RegistryCommand(CommandKey, this);
        }

        protected Func<TParameter, TResult, TResult> _execute;
        protected Func<TParameter, TResult, TResult> _unExecute;
        protected Func<TParameter, bool> _canExecute;
        public HistoryContext History { get; }
        protected TParameter Parameter { get; set; }
        protected TResult Result { get; set; }

        public string CommandKey { get; protected set; }


        public event ExecuteHandler OnExecute;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter.Cast<TParameter>());
        }

        public void Execute(object parameter = default)
        {
            Parameter = parameter.Cast<TParameter>();
            Result = _execute.Invoke(Parameter, Result);
            History.Snapshot(ToSnapShot());

            Result = default;
            OnExecute?.Invoke(Parameter);
            Parameter = default;         
        }

        protected HistoryEntry ToSnapShot()
        {
            return new HistoryEntry(CommandKey, Parameter, Result);
        }

        void ICommandWithUndoRedo.Undo(HistoryEntry element)
        {
            element.Result = _unExecute(element.Parameter.Cast<TParameter>(), element.Result.Cast<TResult>());
        }

        void ICommandWithUndoRedo.Redo(HistoryEntry element)
        {
            element.Result = _execute(element.Parameter.Cast<TParameter>(), element.Result.Cast<TResult>());
        }
    }
}
