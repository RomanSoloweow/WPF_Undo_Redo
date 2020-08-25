using System;
using System.Collections.Generic;
using System.Text;

namespace WPFUndoRedo
{
    public interface ICommandWithUndoRedo
    {
        void Undo(HistoryEntry element);

        void Redo(HistoryEntry element);
    }
}
