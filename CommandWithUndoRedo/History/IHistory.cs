using System;

namespace WPFUndoRedo
{
    public interface IHistory
    {
        void Undo(Action<HistoryEntry> undo);
        void Redo(Action<HistoryEntry> redo);
        void Clear();

        void Snapshot(HistoryEntry historyElement);
    }

}
