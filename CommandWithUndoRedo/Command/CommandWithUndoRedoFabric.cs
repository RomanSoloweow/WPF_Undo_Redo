using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace WPFUndoRedo
{
   public static class CommandWithUndoRedo
    {
        public static CommandWithUndoRedo<TParameter, TResult> Create<TParameter, TResult>(string commandKey,
                Func<TParameter, TResult, TResult> execute,
                Func<TParameter, TResult, TResult> unExecute,
                string historyKey = "", Func<TParameter, bool> canExecute = null)
        {

            return new CommandWithUndoRedo<TParameter, TResult>(HistoryManager.GetHistoryContext(historyKey),
                commandKey, execute, unExecute, canExecute);
        }

        public static CommandWithUndoRedo<Unit, TResult> Create<TResult>(string commandKey,
        Func<TResult, TResult> execute,
        Func<TResult, TResult> unExecute,
        string historyKey = "", Func<bool> canExecute = null)
        {

            return new CommandWithUndoRedo<Unit, TResult>(HistoryManager.GetHistoryContext(historyKey),
                commandKey, (x,y) => { return execute(y); }, (x, y) => { return unExecute(y); }, (x) => { return canExecute(); });
        }

        public static CommandWithUndoRedo<TParameter, Unit> Create<TParameter>(string commandKey,
        Action<TParameter> execute,
        Action<TParameter> unExecute,
        string historyKey = "", Func<TParameter, bool> canExecute = null)
        {

            return new CommandWithUndoRedo<TParameter, Unit>(HistoryManager.GetHistoryContext(historyKey),
                commandKey, (x, y)=> { execute(x); return Unit.Default; }, (x, y) => { unExecute(x); return Unit.Default; }, canExecute);
        }

        public static CommandWithUndoRedo<Unit, Unit> Create(string commandKey,
        Action execute,
        Action unExecute,
        string historyKey = "", Func<bool> canExecute = null)
        {

            return new CommandWithUndoRedo<Unit, Unit>(HistoryManager.GetHistoryContext(historyKey),
                commandKey, (x,y) => { execute(); return Unit.Default; }, (x, y) => { unExecute(); return Unit.Default; }, (x)=> { return canExecute(); });
        }
    }
}
