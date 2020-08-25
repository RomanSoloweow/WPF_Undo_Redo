using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace WPFUndoRedo
{
    public class HistoryContext
    {
        protected IHistory History;
        internal HistoryContext(IHistory history)
        {
            History = history;
            Undo = new Command<Unit>(unit => History.Undo(entry => ResolveCommand(entry.CommandKey).Undo(entry)));
            Redo = new Command<Unit>(unit => History.Undo(entry => ResolveCommand(entry.CommandKey).Redo(entry)));
        }

        internal void Snapshot(HistoryEntry entry) => History.Snapshot(entry);

        public Command<Unit> Undo { get; }
        public Command<Unit> Redo { get; }
        protected ICommandWithUndoRedo ResolveCommand(string commandKey) => CommandManager.ResolveCommand(commandKey);

    }
}
