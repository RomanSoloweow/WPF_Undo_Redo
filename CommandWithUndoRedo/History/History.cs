using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFUndoRedo
{
    public class History : IHistory, IDisposable
    {
        public History()
        {

        }

        public Stack<HistoryEntry> StackRedo { get; set; } = new Stack<HistoryEntry>();

        public Stack<HistoryEntry> StackUndo { get; set; } = new Stack<HistoryEntry>();

        public void Undo(Action<HistoryEntry> undo)
        {
            if (StackUndo.Count > 0)
            {
                HistoryEntry last = StackUndo.Pop();
                undo.Invoke(last);
                StackRedo.Push(last);
            }
        }
        public void Clear()
        {
            StackRedo.Clear();
            StackUndo.Clear();
        }
        public void Redo(Action<HistoryEntry> redo)
        {
            if (StackRedo.Count > 0)
            {
                HistoryEntry last = StackRedo.Pop();
                redo.Invoke(last);
                StackUndo.Push(last);
            }
        }

        public void Snapshot(HistoryEntry entry)
        {
            StackRedo.Clear();
            StackUndo.Push(entry);
        }
        public void Dispose()
        {
            StackRedo.Clear();
            StackUndo.Clear();
        }

    }
}
